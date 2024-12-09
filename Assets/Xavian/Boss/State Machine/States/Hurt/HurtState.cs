using UnityEngine;

namespace Xavian
{
    public class HurtState : BaseState
    {
        [SerializeField] private string AnimClipName = "Get Hit";
        private int HurtAnimHash;

        private void OnEnable()
        {
            HurtAnimHash = Animator.StringToHash(AnimClipName);
        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            stateMachine.CanChangeState = false;

            stateMachine.bossData.BossAnimEvents.OnEndHurt.AddListener(EndHurt);
            stateMachine.bossData.BossAnimator.Play(HurtAnimHash,-1,0.08f);

        }


        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.BossAnimEvents.OnEndHurt.RemoveListener(EndHurt);
        }


        private void EndHurt()
        {
            stateMachine.CanChangeState = true;
            stateMachine.TransitionState(stateMachine.idleState);
        }

    }
}