using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xavian
{
    public class BasicAttackState : BaseState
    {
        [SerializeField, Range(0.1f, 1f)] float AnimAnticpationSpeedRate = 0.25f;

        [SerializeField, Range(0.01f, 1f)] private float animCrossValue = 0.25f;

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

            stateMachine.bossData.BossAnimator.CrossFade(BasicAttackAnimHash, animCrossValue);
        }

        [SerializeField,Range(0,5f)] private float turnRate = 0.75f;

        public override void UpdateState()
        {
            base.UpdateState();


            Vector3 directionTo = (stateMachine.bossData.XavianGameManager.Player.transform.position
                                                    - stateMachine.bossData.BossTransform.position);

            directionTo.y = 0f;
            directionTo.Normalize();

            Quaternion lookRotation = Quaternion.LookRotation(directionTo);

            stateMachine.bossData.BossTransform.rotation = Quaternion.Lerp(stateMachine.bossData.BossTransform.rotation, lookRotation,
                                                                turnRate * Time.deltaTime);
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

        public void EnableDamageArea() => stateMachine.bossData.MeleeDamageCollider.enabled = true;
        public void DisableDamageArea() => stateMachine.bossData.MeleeDamageCollider.enabled = false;
        public void StartAnticipation() => stateMachine.bossData.BossAnimator.speed = AnimAnticpationSpeedRate;
        public void EndAnticipation() => stateMachine.bossData.BossAnimator.speed = 1f;

        public void BasicAttackFinished() => stateMachine.TransitionState(stateMachine.idleState);





    }
}