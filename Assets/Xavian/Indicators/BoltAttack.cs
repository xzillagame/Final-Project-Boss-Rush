using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

namespace Xavian
{
    public class BoltAttack : MonoBehaviour
    {
        public UnityEvent OnBoltFinished;

        [SerializeField] private CapsuleCollider capsule;
        [SerializeField] private VisualEffect boltEffect;

        [SerializeField] private float damagerLifetime = 0.25f;


        public void InitalizeBolt(Vector3 spawnPosition)
        {
            Vector3 newPosition = spawnPosition;

            float halfHeight = capsule.height / 2f;

            halfHeight *= capsule.gameObject.transform.localScale.y;

            newPosition.y += (halfHeight + transform.position.y);

            transform.position = newPosition;

            boltEffect.Play();

        }


        private void Start()
        {
            StartCoroutine(TimetoDisableDamager());
        }


        IEnumerator TimetoDisableDamager()
        {
            yield return new WaitForSeconds(damagerLifetime);
            capsule.enabled = false;
            OnBoltFinished.Invoke();
        }


    }
}