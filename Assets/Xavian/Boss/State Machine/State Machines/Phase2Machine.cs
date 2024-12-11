using System;
using UnityEngine;

namespace Xavian
{
    public class Phase2Machine : BossStateMachine
    {
        [SerializeField] private BossStateMachine nextPhase;

        public override void IntilaizeStateMachine(BossDataAndInitalizer boss)
        {
            base.IntilaizeStateMachine(boss);

            initialState.OnStateCompleted += RestoreHealth;

            boss.MeleeRangeSensor.OnEnter.AddListener(PerformMeleeAttack);
            boss.BossDamageable.OnDeath.AddListener(NextPhase);

        }

        private void RestoreHealth()
        {
            bossData.BossDamageable.HealToFull();
            initialState.OnStateCompleted -= RestoreHealth;
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