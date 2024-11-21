using UnityEngine;

namespace Xavian
{
    public class IdleState : BaseState
    {
        [SerializeField] private string AnimClipName = "Idle";
        private int IdleAnimHash;

        private void OnEnable()
        {
            IdleAnimHash = Animator.StringToHash(AnimClipName);
        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);
            stateMachine.bossData.BossAnimator.Play(IdleAnimHash);
            stateMachine.bossData.meleeRangeCollider.enabled = true;
        }

        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.meleeRangeCollider.enabled = false;
        }

    }
}