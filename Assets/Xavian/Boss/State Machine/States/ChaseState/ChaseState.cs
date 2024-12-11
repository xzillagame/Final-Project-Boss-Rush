using UnityEngine;

namespace Xavian
{
    public class ChaseState : BaseState
    {
        [SerializeField,Range(0.01f,1f)] private float animCrossValue = 0.25f;
        [SerializeField] private float MaxChaseTime = 3f;
        private float currentChaseTime = 0f;


        [SerializeField] private string AnimClipName = "Run";
        private int ChaseAnimHash;


        private void OnEnable()
        {
            ChaseAnimHash = Animator.StringToHash(AnimClipName);
        }


        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);
            stateMachine.bossData.MeleeRangeCollider.enabled = true;
            stateMachine.bossData.BossAnimator.CrossFade(ChaseAnimHash, animCrossValue);
            stateMachine.bossData.BossNavAgent.enabled = true;
        }

        public override void UpdateState()
        {
            base.UpdateState();

            stateMachine.bossData.BossNavAgent.SetDestination(stateMachine.bossData.XavianGameManager.Player.transform.position);

            currentChaseTime += Time.deltaTime;

            if(currentChaseTime > MaxChaseTime)
            {
                stateMachine.TransitionState(stateMachine.groundedFireballState);
            }


        }

        public override void ExitState()
        {
            base.ExitState();

            stateMachine.bossData.BossNavAgent.enabled = false;


            stateMachine.bossData.MeleeRangeCollider.enabled = false;
            currentChaseTime = 0f;
        }


    }
}