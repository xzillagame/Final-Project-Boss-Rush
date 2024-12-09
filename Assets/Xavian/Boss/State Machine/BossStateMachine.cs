using UnityEngine;

namespace Xavian
{
    public class BossStateMachine : MonoBehaviour
    {
        [HideInInspector] public BossDataAndInitalizer bossData;


        public IdleState idleState;
        public BasicAttackState basicAttackState;
        public GroundedFireballState groundedFireballState;
        public ChaseState chaseState;
        public HurtState hurtState;
        public LightingAttackState lightingAttackState;

        public bool CanChangeState = true;


        private BaseState currentState;


        [ContextMenu("Lighting Attack")]
        private void ForceLighting()
        {
            TransitionState(lightingAttackState);
        }

        public void IntilaizeStateMachine(BossDataAndInitalizer boss)
        {
            bossData = boss;

            currentState = idleState;
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