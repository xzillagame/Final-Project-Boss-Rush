using UnityEngine;

namespace Xavian
{
    public class IdleState : BaseState
    {
        [SerializeField] private float MaxIdleTime = 2f;
        private float currentIdleTime = 0f;

        [SerializeField] private bool CanExitIdleThroughTime = true;

        [SerializeField, Range(0.01f, 1f)] private float animCrossValue = 0.25f;

        [SerializeField] private string AnimClipName = "Idle";
        private int IdleAnimHash;

        private void OnEnable()
        {
            IdleAnimHash = Animator.StringToHash(AnimClipName);
        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);
            stateMachine.bossData.BossAnimator.CrossFade(IdleAnimHash, animCrossValue);
            stateMachine.bossData.MeleeRangeCollider.enabled = true;
        }

        public override void UpdateState()
        {

            if (!CanExitIdleThroughTime) return;

            base.UpdateState();

            currentIdleTime += Time.deltaTime;

            if (currentIdleTime > MaxIdleTime)
            {
                stateMachine.TransitionState(stateMachine.chaseState);
            }

        }


        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.MeleeRangeCollider.enabled = false;
            currentIdleTime = 0f;
        }

    }
}