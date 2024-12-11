using UnityEngine;
using UnityEngine.Events;

namespace Xavian
{
    public class ReflectTracker : MonoBehaviour
    {
        public UnityAction OnProjectileEnterReflectArea;


        private void OnTriggerEnter(Collider other)
        {
            OnProjectileEnterReflectArea?.Invoke();
        }
    }
}