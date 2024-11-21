using System;
using System.Collections;
using UnityEngine;

namespace Xavian
{
    public class GroundedFireballState : BaseState
    {
        [SerializeField] private Projectile basicFireballProjectile;


        [SerializeField, Range(0.1f , 1f)] private float AnimAnticpationSpeedRate = 0.5f;

        [SerializeField] private string AnimClipName = "Fireball Shoot";
        private int FireballShootAnimHash;

        private void OnEnable()
        {
            FireballShootAnimHash = Animator.StringToHash(AnimClipName);
        }


        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            stateMachine.bossData.BossAnimEvents.OnStartFireballAnticipation.AddListener(BeginFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballAnticipation.AddListener(FinishFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballShotAttack.AddListener(FireballShotAttackFinished);
            stateMachine.bossData.BossAnimEvents.OnFireballLaunched.AddListener(ShootFireball);


            stateMachine.bossData.BossAnimator.Play(FireballShootAnimHash);
        }


        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.BossAnimator.speed = 1f;

            stateMachine.bossData.BossAnimEvents.OnStartFireballAnticipation.RemoveListener(BeginFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballAnticipation.RemoveListener(FinishFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballShotAttack.RemoveListener(FireballShotAttackFinished);
            stateMachine.bossData.BossAnimEvents.OnFireballLaunched.RemoveListener(ShootFireball);
        }

        private void BeginFireballAnticipation() => stateMachine.bossData.BossAnimator.speed = AnimAnticpationSpeedRate;

        private void FinishFireballAnticipation() => stateMachine.bossData.BossAnimator.speed = 1f;

        private void FireballShotAttackFinished() => stateMachine.TransitionState(stateMachine.idleState);
        private void ShootFireball()
        {
            Transform spawnLocation = stateMachine.bossData.ProjectileShootingLocation;

            Projectile newProjecitle = Instantiate<Projectile>(basicFireballProjectile, spawnLocation.position, spawnLocation.rotation);
            newProjecitle.transform.forward = stateMachine.bossData.transform.forward;
        }




    }
}