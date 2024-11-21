using UnityEngine;

namespace Xavian
{
    public class BossLogic : MonoBehaviour
    {
        [field: SerializeField] public Animator BossAnimator { get; private set; }
        [field: SerializeField] public BossAnimationEvents BossAnimEvents { get; private set; }

        [field:SerializeField] public Transform ProjectileShootingLocation { get; private set; }

        [SerializeField] private BossStateMachine stateMachine;

        public BoxCollider meleeRangeCollider;
        public BoxCollider damageCollider;


        private void Start()
        {
            stateMachine.IntilaizeStateMachine(this);
        }



    }
}