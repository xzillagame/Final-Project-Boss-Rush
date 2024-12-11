using DG.Tweening;
using System;
using UnityEngine;

namespace Xavian
{
    public class TailSpinState : BaseState
    {
        [SerializeField] private string AnimClipName = "Tail Attack";
        private int TailSpinAnimHash;

        private float currentSpeed;

        [SerializeField] private float duration = 1f;
        [SerializeField] private float tailAttackSize = 3f;
        private Vector3 orginalColliderSize = Vector3.zero;

        private void OnEnable()
        {
            TailSpinAnimHash = Animator.StringToHash(AnimClipName);
        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            stateMachine.CanChangeState = false;

            stateMachine.bossData.BossAnimator.Play(TailSpinAnimHash);

            stateMachine.bossData.MeleeRangeCollider.enabled = false;

            stateMachine.bossData.BossAnimEvents.OnTailSpinRotateEnd.AddListener(EndRotation);
            stateMachine.bossData.BossAnimEvents.OnReturnFromTailSwipe.AddListener(DisableDamageCollider);
            stateMachine.bossData.BossAnimEvents.OnEndTailSpin.AddListener(FinishState);
        }
        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.BossAnimEvents.OnTailSpinRotateEnd.RemoveListener(EndRotation);
        }

        private void DisableDamageCollider()
        {
            stateMachine.bossData.MeleeDamageCollider.enabled = false;
        }


        private void FinishState()
        {
            stateMachine.CanChangeState = true;
            
            stateMachine.TransitionState(stateMachine.idleState);
            stateMachine.bossData.MeleeDamageCollider.enabled = false;
            stateMachine.bossData.MeleeDamageCollider.size = orginalColliderSize;
        }

        private void EndRotation()
        {

            stateMachine.bossData.MeleeDamageCollider.enabled = true;
            orginalColliderSize = stateMachine.bossData.MeleeDamageCollider.size;
            stateMachine.bossData.MeleeDamageCollider.size = orginalColliderSize * tailAttackSize;

            currentSpeed = stateMachine.bossData.BossAnimator.speed;
            stateMachine.bossData.BossAnimator.speed = 0f;

            float rotationWith360 = stateMachine.bossData.BossTransform.rotation.eulerAngles.y;
            rotationWith360 += 360f;

            stateMachine.bossData.BossTransform.DORotate(new Vector3(0, rotationWith360, 0), duration, RotateMode.FastBeyond360)
                .SetId(0)
                .OnComplete(ReturnToOrginalSpeed);

        }

        private void ReturnToOrginalSpeed()
        {
            stateMachine.bossData.BossAnimator.speed = currentSpeed;
        }



    }
}