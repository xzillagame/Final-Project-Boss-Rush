using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xavian
{
    public class FireballProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Damager damager;
        [SerializeField] private Collider projectileCollider;

        [SerializeField] private float hitCooldown = 0.25f;

        [SerializeField] private LayerMask layerThatReflectsProjectile;
        [SerializeField] private LayerMask layerOfEntities;
        [SerializeField] private LayerMask layerOfWallBounce;

        [SerializeField, Range(0,3)] private int numberOfWallBounces = 0;


        [SerializeField] private Color orbColor;
        [SerializeField] private Color flareColor;
        [SerializeField] private Color trailColor;

        [SerializeField] private ParticleSystem orbEffectParticleSystem;
        [SerializeField] private ParticleSystem flareEffectParticleSystem;
        [SerializeField] private TrailRenderer addetiveColorTrail;

        [SerializeField] private float lifeTime = 5f;
        private float currentLifeTime = 0;

        private Rigidbody rigidBody;
        private WaitForSeconds reflectionWaittime;
        private bool isRefelectable = false;

        private Vector3 direction;

        private Transform lastEntityOrgin;



        public void SetEntity(Transform transform)
        {
            lastEntityOrgin = transform;
        }

        public void EnableReflect()
        {
            ParticleSystem.MainModule editableMainModule;

            editableMainModule = orbEffectParticleSystem.main;
            editableMainModule.startColor = orbColor;

            editableMainModule = flareEffectParticleSystem.main;
            editableMainModule.startColor = flareColor;



            var gradient = addetiveColorTrail.colorGradient;
            var keys = gradient.colorKeys;

            keys[0].color = trailColor;

            gradient.colorKeys = keys;
            addetiveColorTrail.colorGradient = gradient;

            isRefelectable = true;


        }


        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.velocity = transform.forward * speed;
            reflectionWaittime = new WaitForSeconds(hitCooldown);
            direction = rigidBody.velocity.normalized;
        }

        private void Update()
        {
            currentLifeTime += Time.deltaTime;
            if(currentLifeTime > lifeTime)
            {
                DestroyProjectile();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            int otherobjectLayerMask = 1 << other.gameObject.layer;

            if (isRefelectable == true && (layerThatReflectsProjectile & otherobjectLayerMask) != 0)
            {

                Vector3 directionTo = (lastEntityOrgin.position - transform.position).normalized;
                direction.y = 0;


                rigidBody.velocity = (directionTo * speed) * 1.5f;


                LayerMask storeDamageLayer = projectileCollider.includeLayers & layerThatReflectsProjectile;
                LayerMask storeEntityLayer = projectileCollider.includeLayers & layerOfEntities;


                storeDamageLayer ^= layerThatReflectsProjectile;
                storeEntityLayer ^= layerOfEntities;

                projectileCollider.includeLayers = storeDamageLayer | storeEntityLayer;

                StartCoroutine(ReflectionWaitTimeCoRoutine());

            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            int otherObjectLayerMask = 1 << collision.gameObject.layer;

            if( (otherObjectLayerMask & layerOfWallBounce) != 0 && numberOfWallBounces != 0 )
            {

                numberOfWallBounces--;

                Vector3 reflectVel = Vector3.Reflect(direction, collision.contacts[0].normal);

                rigidBody.velocity = reflectVel * speed;
                direction = rigidBody.velocity.normalized;
            }
            else if ( numberOfWallBounces == 0 )
            {
                DestroyProjectile();
            }
        }


        private IEnumerator ReflectionWaitTimeCoRoutine()
        {
            isRefelectable = false;
            yield return reflectionWaittime;
            isRefelectable = true;
        }


        public void DestroyProjectile()
        {
            Destroy(gameObject);
        }

    }
}