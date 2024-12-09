using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Xavian
{
    public class AttackIndiactor : MonoBehaviour
    {

        public UnityEvent OnIndicatorFinished;

        [SerializeField] BoltAttack bolt;


        [SerializeField, Range(0,10f), Tooltip("Time in Seconds")] public float timeToFilled = 10f;
        
        [SerializeField] private RectTransform fillCircleScale;
        [SerializeField] private float scaleValueToReach = 5f;
        private Vector3 targetSize = Vector3.zero;

        private float currentTime = 0;

        private Vector3 orginalScale = Vector3.zero;

        private BoltAttack currentBolt = null;

        
        private Queue<AttackIndiactor> poolReference = new Queue<AttackIndiactor>();

        public void InitialSetupOfIndicator(Queue<AttackIndiactor> pool)
        {
            poolReference = pool;
        }

        public void StartIndicator(float timeForAttack = -1)
        {
            Vector3 resetedScale = fillCircleScale.localScale;
            resetedScale.x = 0;
            resetedScale.y = 0;
            fillCircleScale.localScale = resetedScale;

            if (timeForAttack > 0)
            {
                timeToFilled = timeForAttack;
            }

            currentTime = 0f;

            targetSize.z = 1f;
            targetSize.x = scaleValueToReach;
            targetSize.y = scaleValueToReach;

            orginalScale.z = 1;


            enabled = true;

        }
        
        public void DisableIndicator()
        {
            poolReference.Enqueue(this);
            gameObject.SetActive(false);
        }

        public void SpawnObject()
        {
            currentBolt = Instantiate(bolt);
            currentBolt.InitalizeBolt(transform.position);
            currentBolt.OnBoltFinished.AddListener(DisableIndicator);
        }


        private void Update()
        {

            fillCircleScale.localScale = Vector3.Lerp(orginalScale, targetSize, currentTime / timeToFilled);
            currentTime += Time.deltaTime;

            if(fillCircleScale.localScale.x >= scaleValueToReach)
            {
                OnIndicatorFinished.Invoke();
                enabled = false;
            }

        }

        private void OnDisable()
        {
            currentBolt.OnBoltFinished.RemoveListener(DisableIndicator);
            OnIndicatorFinished.RemoveAllListeners();
        }



    }
}