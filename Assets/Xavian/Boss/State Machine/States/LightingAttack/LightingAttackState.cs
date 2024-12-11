using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Xavian
{
    public class LightingAttackState : BaseState
    {
        private Queue<AttackIndiactor> lightingAttackQueue = new Queue<AttackIndiactor>();

        [SerializeField, Range(0.01f, 1f)] private float animCrossValue = 0.5f;

        [SerializeField] private string AnimClipName = "Scream";
        [SerializeField] private string AfterRoarAnimClipName = "Idle";
        private int RoarAnimHash;
        private int AfterRoarAnimHash;

        [SerializeField] private int totalLightingStrikesInPool = 50;
        [SerializeField] public int lightingStrikeCount = 5;

        [SerializeField] private AttackIndiactor lightingAttack;

        //Needed to know to when the last lighting strike finished
        private AttackIndiactor lastCachedReference;

        [SerializeField] private float lightingBoltDelayTime = 0.1f;
        private WaitForSeconds lightingBoltWaitForSeconds;

        private void OnEnable()
        {
            RoarAnimHash = Animator.StringToHash(AnimClipName);
            AfterRoarAnimHash = Animator.StringToHash(AfterRoarAnimClipName);

            for (int i = 0; i < totalLightingStrikesInPool; i++) 
            {
                AttackIndiactor newLightingStrike = Instantiate(lightingAttack);
                lightingAttack.gameObject.SetActive(false);
                newLightingStrike.InitialSetupOfIndicator(lightingAttackQueue);

                lightingAttackQueue.Enqueue(newLightingStrike);

            }

        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            lightingBoltWaitForSeconds = new WaitForSeconds(lightingBoltDelayTime);

            bossStateMachine.bossData.BossAnimator.CrossFade(RoarAnimHash, animCrossValue, -1);
            bossStateMachine.bossData.BossAnimEvents.OnBeginRoarAttack.AddListener(RunSpawnLightingRoutine);
            bossStateMachine.bossData.BossAnimEvents.OnEndRoar.AddListener(EndRoarAnimation);

            bossStateMachine.CanChangeState = false;

        }

        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.BossAnimEvents.OnBeginRoarAttack.RemoveListener(RunSpawnLightingRoutine);
            stateMachine.bossData.BossAnimEvents.OnEndRoar.RemoveListener(EndRoarAnimation);

            lastCachedReference = null;
        }

        private void EndRoarAnimation()
        {
            stateMachine.bossData.BossAnimator.CrossFade(AfterRoarAnimHash, animCrossValue, -1);
        }


        private void RunSpawnLightingRoutine()
        {
            StartCoroutine(SpawnLightingStrikes());
        }


        private IEnumerator SpawnLightingStrikes()
        {

            for(int i = 0; i < lightingStrikeCount; i++) 
            {

                AttackIndiactor newAttack = lightingAttackQueue.Dequeue();
                newAttack.transform.position = stateMachine.bossData.XavianGameManager.GetRandomPointInArenaBounds();
                newAttack.gameObject.SetActive(true);
                newAttack.StartIndicator();
                newAttack.OnIndicatorFinished.AddListener(newAttack.DisableIndicator);

                if(i == lightingStrikeCount - 1)
                {
                    newAttack.OnIndicatorFinished.AddListener(FinishAttack);
                    lastCachedReference = newAttack;
                }

                yield return lightingBoltWaitForSeconds;

            }

        }

        private void FinishAttack()
        {
            stateMachine.CanChangeState = true;
            stateMachine.TransitionState(stateMachine.idleState);
            OnStateCompleted?.Invoke();
        }


    }
}