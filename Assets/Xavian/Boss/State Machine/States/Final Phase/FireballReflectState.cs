using UnityEngine;

namespace Xavian
{
    public class FireballReflectState : BaseState
    {

        [SerializeField] private string animClipName = "Tail Attack";
        private int reflectAnimHash;

        [SerializeField] private float animSpeed;


        private void OnEnable()
        {
            reflectAnimHash = Animator.StringToHash(animClipName);
        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            stateMachine.bossData.BossAnimator.Play(reflectAnimHash);
            stateMachine.bossData.BossAnimator.speed = animSpeed;

            stateMachine.bossData.BossAnimEvents.OnEndTailSpin.AddListener(FinishState);

        }

        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.BossAnimEvents.OnEndTailSpin.RemoveListener(FinishState);
        }


        private void FinishState()
        {
            stateMachine.TransitionState(stateMachine.idleState);
            stateMachine.bossData.BossAnimator.speed = 1f;
        }



    }
}