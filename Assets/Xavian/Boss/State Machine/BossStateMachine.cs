using UnityEngine;

namespace Xavian
{
    public class BossStateMachine : MonoBehaviour
    {
        [HideInInspector]public BossLogic bossData;


        public IdleState idleState;
        public BasicAttackState basicAttackState;
        public GroundedFireballState groundedFireballState;

        private BaseState currentState;

        #region State Change Overrides

        //Overrides current state with desidered function to set state

        public void PerformBasicAttack()
        {
            if(currentState != basicAttackState)
            {
                TransitionState(basicAttackState);
            }
        }

        #endregion

        [ContextMenu("Test Fireball")]
        private void TestGroundedFireball()
        {
            TransitionState(groundedFireballState);
        }


        public void IntilaizeStateMachine(BossLogic boss)
        {
            bossData = boss;

            currentState = idleState;
            currentState.EnterState(this);
        }

        public void TransitionState(BaseState nextState)
        {
            if(nextState != null && currentState != nextState)
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






    }
}