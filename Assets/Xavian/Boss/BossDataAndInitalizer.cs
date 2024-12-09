using System;
using UnityEngine;

namespace Xavian
{
    public class BossDataAndInitalizer : MonoBehaviour
    {
        [field:SerializeField] public LocalGameManager XavianGameManager;

        public Transform BossTransform { get; private set; }

        [field: SerializeField] public Animator BossAnimator { get; private set; }
        [field: SerializeField] public BossAnimationEvents BossAnimEvents { get; private set; }

        [field:SerializeField] public Transform ProjectileShootingLocation { get; private set; }

        [SerializeField] private BossStateMachine stateMachine;

        [field: SerializeField] public BoxCollider MeleeRangeCollider { get; private set; }
        [field: SerializeField] public BoxCollider MeleeDamageCollider { get; private set; }

        [SerializeField] private BossStunHandler stunHandler;

        [field:SerializeField] public Transform BossCenterPoint {get; private set; }

        private void OnEnable()
        {
            stunHandler.OnBossStunThreshholdReached += BossStunned;
            BossTransform = transform;
        }


        private void OnDisable()
        {
            stunHandler.OnBossStunThreshholdReached -= BossStunned;
        }

        private void BossStunned()
        {
            stateMachine.TransitionState(stateMachine.hurtState);
        }

        private void Start()
        {
            stateMachine.IntilaizeStateMachine(this);
        }




    }
}