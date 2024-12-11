using UnityEngine;

namespace Xavian
{
    public class Phase1Machine : BossStateMachine
    {

        [SerializeField] BossStateMachine nextPhase;

        public override void IntilaizeStateMachine(BossDataAndInitalizer boss)
        {
            base.IntilaizeStateMachine(boss);

            boss.MeleeRangeSensor.OnEnter.AddListener(PerformMeleeAttack);
            boss.BossDamageable.OnDeath.AddListener(NextPhase);

        }

        private void NextPhase()
        {
            OverrideState(idleState);
            bossData.GoToNextBossPhase(nextPhase);

            bossData.MeleeRangeSensor.OnEnter.RemoveListener(PerformMeleeAttack);
            bossData.BossDamageable.OnDeath.RemoveListener(NextPhase);

        }

        private void PerformMeleeAttack()
        {
            TransitionState(basicAttackState);
        }




    }
}