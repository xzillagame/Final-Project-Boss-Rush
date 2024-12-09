using UnityEngine;
using UnityEngine.Events;

namespace Xavian
{
    public class BasicAttackCooldown : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnCooldownEnded;

        [SerializeField] private float cooldownTime = 1.5f;
        private float currentTime = 0f;


        public void StartCooldown()
        {
            currentTime = 0;
            enabled = true;
        }



        private void Update()
        {
            currentTime += Time.deltaTime;

            if(currentTime >= cooldownTime ) 
            {
                OnCooldownEnded.Invoke();
                enabled = false;
            }

        }
    }
}