using UnityEngine;

namespace Xavian
{
    public class BasicAttackState : BaseState
    {
        [SerializeField, Range(0.1f, 1f)] float AnimAnticpationSpeedRate = 0.25f;

        [SerializeField, Range(0.01f, 1f)] private float animCrossValue = 0.25f;

        [SerializeField] private string AnimClipName = "Basic Attack";
        private int BasicAttackAnimHash;

        [SerializeField,Range(0,5f)] private float turnRate = 0.75f;

        [SerializeField] private int TimesToBiteBeforeTailSwipe = 2;
        private int currentCount = 0;

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

        private void EnableDamageArea() => stateMachine.bossData.MeleeDamageCollider.enabled = true;
        private void DisableDamageArea() => stateMachine.bossData.MeleeDamageCollider.enabled = false;
        private void StartAnticipation() => stateMachine.bossData.BossAnimator.speed = AnimAnticpationSpeedRate;
        private void EndAnticipation() => stateMachine.bossData.BossAnimator.speed = 1f;
        private void BasicAttackFinished()
        {
            currentCount += 1;

            if (currentCount >= TimesToBiteBeforeTailSwipe)
            {
                currentCount = 0;
                stateMachine.TransitionState(stateMachine.tailSpinState);
            }
            else
            {
                stateMachine.TransitionState(stateMachine.idleState);
            }

        }





    }
}