using System;
using UnityEngine;
using UnityEngine.AI;

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

        [field: SerializeField] public Sensor MeleeRangeSensor { get; private set; }
        [field: SerializeField] public BoxCollider MeleeRangeCollider { get; private set; }
        [field: SerializeField] public BoxCollider MeleeDamageCollider { get; private set; }

        [SerializeField] private BossStunHandler stunHandler;
        [field: SerializeField] public Damageable BossDamageable { get; private set; }

        [field:SerializeField] public Transform BossCenterPoint {get; private set; }

        [field: SerializeField] public NavMeshAgent BossNavAgent { get; private set; }

        [field: SerializeField] public BoxCollider BossFireballReflectCollider { get; private set; }

        private void Start()
        {
            stunHandler.OnBossStunThreshholdReached += BossStunned;
            stateMachine.IntilaizeStateMachine(this);
            BossTransform = transform;
        }

        private void OnDisable()
        {
            stunHandler.OnBossStunThreshholdReached -= BossStunned;
        }


        public void GoToNextBossPhase(BossStateMachine nextStateMachine)
        {
            //stateMachine.gameObject.SetActive(false);
            stateMachine.gameObject.SetActive(false);
            stateMachine = nextStateMachine;
            stateMachine.gameObject.SetActive(true);
            stateMachine.IntilaizeStateMachine(this);
        }


        private void BossStunned()
        {
            stateMachine.TransitionState(stateMachine.hurtState);
        }

    }
}