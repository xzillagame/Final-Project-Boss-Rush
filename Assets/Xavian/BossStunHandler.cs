using UnityEngine;
using UnityEngine.Events;

namespace Xavian
{
    public class BossStunHandler : MonoBehaviour
    {
        public event UnityAction OnBossStunThreshholdReached;

        [Header("Stun Values")]
        [SerializeField, Range(0f,3f)] private float maxStunValue = 1f;
        [SerializeField,Range(0f,0.5f)] private float stunBuildupRate = 0.25f;
        [SerializeField, Range(0f,1f)] private float stunDecayRate = 0.05f;
        private float currentStunValue = 0f;

        [Space(10f)]
        [Header("Stun Timer Values")]
        [SerializeField] private float timeBeforeDecayResumes = 2f;
        private float currentTimeBeforeDecayResumes = 0f;

        [SerializeField] private float maxStunCooldownTime = 6f;
        private float currentStunCooldownTime = 0f;

        private bool HasStunCooldown = false;

        public void RecievedHit(Damage damage)
        {
            if (HasStunCooldown) return;

            currentStunValue += stunBuildupRate;
            currentStunValue = Mathf.Clamp(currentStunValue, 0f, maxStunValue);

            if (currentStunValue >= maxStunValue)
            {
                OnBossStunThreshholdReached?.Invoke();
                currentStunValue = 0f;
                currentTimeBeforeDecayResumes = 0f;
                HasStunCooldown = true;
            }


            currentTimeBeforeDecayResumes = 2f;
        }



        private void Update()
        {
            currentTimeBeforeDecayResumes -= Time.deltaTime;
            currentTimeBeforeDecayResumes = Mathf.Clamp(currentTimeBeforeDecayResumes, 0, timeBeforeDecayResumes);

            if(currentTimeBeforeDecayResumes == 0f)
            {
                currentStunValue -= stunDecayRate * Time.deltaTime;
                currentStunValue = Mathf.Clamp(currentStunValue, 0, maxStunValue);
            }

            if(HasStunCooldown)
            {
                currentStunCooldownTime += Time.deltaTime;

                if(currentStunCooldownTime >= maxStunCooldownTime)
                {
                    HasStunCooldown = false;
                    currentStunCooldownTime = 0f;
                }

            }
            


        }


    }
}