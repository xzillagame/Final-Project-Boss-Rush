using UnityEngine;

namespace Xavian
{
    public class Phase3Machine : BossStateMachine
    {

        [SerializeField] public LastFireballShootState LastFireballShootState;

        [SerializeField] private int maxNumberOfFireballReflects = 6;
        private int currentReflectCount = 0;


        public FireballReflectState FireballReflectState;

        public override void IntilaizeStateMachine(BossDataAndInitalizer boss)
        {
            base.IntilaizeStateMachine(boss);

            LastFireballShootState.OnStateCompleted += EnableCollider;

            bossData.BossDamageable.OnDeath.AddListener(EnterDeathState);

            bossData.XavianGameManager.BeginFinalPhase();

            boss.BossDamageable.HealToFull();

            StaticInputManager.input.Player.Move.Disable();
            StaticInputManager.input.Player.Dodge.Disable();
        }

        public void MoveToReflectState()
        {
            currentReflectCount++;

            if(currentReflectCount < maxNumberOfFireballReflects)
            {
                TransitionState(FireballReflectState);
            }
            else
            {
                TransitionState(FireballReflectState);
                bossData.BossFireballReflectCollider.enabled = false;
            }


        }


        private void EnableCollider()
        {
            bossData.BossFireballReflectCollider.enabled = true;
        }

    }
}