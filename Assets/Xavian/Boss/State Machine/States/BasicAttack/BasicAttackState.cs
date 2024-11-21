using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xavian
{
    public class BasicAttackState : BaseState
    {
        [SerializeField, Range(0.1f, 1f)] float AnimAnticpationSpeedRate = 0.25f;

        [SerializeField] private string AnimClipName = "Basic Attack";
        private int BasicAttackAnimHash;

        private void OnEnable()
        {
            BasicAttackAnimHash = Animator.StringToHash(AnimClipName);
        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            stateMachine.bossData.BossAnimEvents.OnStartBasicAttackAnticipation.AddListener(StartAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndBasicAttackAnticipation.AddListener(EndAnticipation);
            stateMachine.bossData.BossAnimEvents.OnStartAttemptDamageBasicAttack.AddListener(EnableDamageArea);
            stateMachine.bossData.BossAnimEvents.OnEndAttemptDamageBasicAttack.AddListener(DisableDamageArea);
            stateMachine.bossData.BossAnimEvents.OnEndBasicAttack.AddListener(BasicAttackFinished);

            stateMachine.bossData.BossAnimator.Play(BasicAttackAnimHash);
        }

        public override void ExitState()
        {
            base.ExitState();
            DisableDamageArea();
            stateMachine.bossData.BossAnimator.speed = 1f;

            stateMachine.bossData.BossAnimEvents.OnStartBasicAttackAnticipation.RemoveListener(StartAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndBasicAttackAnticipation.RemoveListener(EndAnticipation);
            stateMachine.bossData.BossAnimEvents.OnStartAttemptDamageBasicAttack.RemoveListener(EnableDamageArea);
            stateMachine.bossData.BossAnimEvents.OnEndAttemptDamageBasicAttack.RemoveListener(DisableDamageArea);
            stateMachine.bossData.BossAnimEvents.OnEndBasicAttack.RemoveListener(BasicAttackFinished);


        }


        //Called from Animation Events

        public void EnableDamageArea() => stateMachine.bossData.damageCollider.enabled = true;
        public void DisableDamageArea() => stateMachine.bossData.damageCollider.enabled = false;
        public void StartAnticipation() => stateMachine.bossData.BossAnimator.speed = AnimAnticpationSpeedRate;
        public void EndAnticipation() => stateMachine.bossData.BossAnimator.speed = 1f;

        public void BasicAttackFinished() => stateMachine.TransitionState(stateMachine.idleState);





    }
}