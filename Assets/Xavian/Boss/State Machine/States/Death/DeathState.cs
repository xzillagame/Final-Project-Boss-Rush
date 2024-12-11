using UnityEngine;

namespace Xavian
{
    public class DeathState : BaseState
    {

        [SerializeField] private string AnimClipName = "Die";
        private int DeathAnimHash;


        private void OnEnable()
        {
            DeathAnimHash = Animator.StringToHash(AnimClipName);
        }


        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            stateMachine.bossData.BossAnimator.speed = 1f;
            stateMachine.bossData.BossAnimator.Play(DeathAnimHash);
        }


    }
}