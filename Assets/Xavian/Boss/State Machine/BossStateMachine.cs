using UnityEngine;

namespace Xavian
{
    public class BossStateMachine : MonoBehaviour
    {
        [HideInInspector] public BossDataAndInitalizer bossData;

        public IdleState idleState;
        public BasicAttackState basicAttackState;
        public ChaseState chaseState;
        public DeathState deathState;
        public HurtState hurtState;

        public BaseState initialState;

        public GroundedFireballState groundedFireballState;
        public LightingAttackState lightingAttackState;
        public TailSpinState tailSpinState;


        public BaseState[] specialAttacks;

        public bool CanChangeState = true;


        protected BaseState currentState;

        public virtual void IntilaizeStateMachine(BossDataAndInitalizer boss)
        {
            bossData = boss;

            currentState = initialState;
            currentState.EnterState(this);

        }

        public void TransitionState(BaseState nextState)
        {
            if(nextState != null && CanChangeState == true)
            {
                currentState.ExitState();
                currentState = nextState;
                currentState.EnterState(this);

            }
        }

        public void OverrideState(BaseState nextState)
        {
            if(nextState != null)
            {
                currentState.ExitState();
                currentState = nextState;
                currentState.EnterState(this);
            }
        }

        public void EnterDeathState()
        {
            CanChangeState = false;
            OverrideState(deathState);
        }

        public BaseState GetRandomSpecialAttack()
        {
            return specialAttacks.GetRandomFromArray();
        }

        private void Update()
        {
            currentState.UpdateState();
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateState();
        }


    }
}