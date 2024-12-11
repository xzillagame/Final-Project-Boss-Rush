using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Xavian
{
    public class LastFireballShootState : BaseState
    {
        [SerializeField] private FireballProjectile basicFireballProjectile;

        [SerializeField, Range(0.01f, 1f)] private float animCrossValue = 0.5f;
        [SerializeField, Range(0.1f, 1f)] private float AnimAnticpationSpeedRate = 0.5f;
        [SerializeField, Range(0.1f, 1.5f)] private float reverseAnimSpeed = 0.25f;

        private float currentAnimAnticipationSpeed = 1f;
        [SerializeField] private int timesToShootProjectile = 1;
        private int currentShootProjectileCounter = 0;

        [SerializeField] private int numberOfReflectableFireballs = 1;

        private HashSet<int> indexToShootReflectableFireballs = new HashSet<int>();

        [SerializeField] private string AnimClipName = "Fireball Shoot";
        private int FireballShootAnimHash;

        [SerializeField, Range(0, 5f)] private float turnRate = 0.75f;
        private bool canTurn = false;

        public event UnityAction<FireballProjectile> OnSpawnedFireball;

        private void OnEnable()
        {
            FireballShootAnimHash = Animator.StringToHash(AnimClipName);
        }

        public override void EnterState(BossStateMachine bossStateMachine)
        {
            base.EnterState(bossStateMachine);

            canTurn = false;

            stateMachine.bossData.BossAnimEvents.OnStartFireballAnticipation.AddListener(BeginFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballAnticipation.AddListener(FinishFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballShotAttack.AddListener(FireballShotAttackFinished);
            stateMachine.bossData.BossAnimEvents.OnFireballLaunched.AddListener(ShootFireball);


            List<int> fireballShootIndex = new List<int>(new int[timesToShootProjectile]);
            for (int i = 0; i < timesToShootProjectile; i++)
            {
                fireballShootIndex[i] = i;
            }

            for (int i = 0; i < numberOfReflectableFireballs; ++i)
            {
                int possibleIndexForReflect = fireballShootIndex.RandomPopFromList<int>();
                indexToShootReflectableFireballs.Add(possibleIndexForReflect);
            }

            currentAnimAnticipationSpeed = AnimAnticpationSpeedRate;
            stateMachine.bossData.BossAnimator.CrossFade(FireballShootAnimHash, animCrossValue);
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (canTurn)
            {
                Vector3 directionTo = (stateMachine.bossData.XavianGameManager.Player.transform.position
                                        - stateMachine.bossData.BossTransform.position);

                directionTo.y = 0f;
                directionTo.Normalize();

                Quaternion lookRotation = Quaternion.LookRotation(directionTo);

                stateMachine.bossData.BossTransform.rotation = Quaternion.Lerp(stateMachine.bossData.BossTransform.rotation, lookRotation,
                                                                    turnRate * Time.deltaTime);
            }


        }

        public override void ExitState()
        {
            base.ExitState();
            stateMachine.bossData.BossAnimator.speed = 1f;

            stateMachine.bossData.BossAnimator.SetFloat("FireballShotReverseValue", 1f);

            currentShootProjectileCounter = 0;
            currentAnimAnticipationSpeed = 1f;

            canTurn = false;

            indexToShootReflectableFireballs.Clear();


            stateMachine.bossData.BossAnimEvents.OnStartFireballAnticipation.RemoveListener(BeginFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballAnticipation.RemoveListener(FinishFireballAnticipation);
            stateMachine.bossData.BossAnimEvents.OnEndFireballShotAttack.RemoveListener(FireballShotAttackFinished);
            stateMachine.bossData.BossAnimEvents.OnFireballLaunched.RemoveListener(ShootFireball);
        }

        private void BeginFireballAnticipation()
        {
            stateMachine.bossData.BossAnimator.speed = currentAnimAnticipationSpeed;
            canTurn = true;
        }

        private void FinishFireballAnticipation()
        {
            canTurn = false;

            if (currentShootProjectileCounter != 0)
            {
                stateMachine.bossData.BossAnimator.SetFloat("FireballShotReverseValue", 1f);
            }

            stateMachine.bossData.BossAnimator.speed = 1f;
        }

        private void FireballShotAttackFinished()
        {
            stateMachine.TransitionState(stateMachine.idleState);
            OnStateCompleted?.Invoke();
        }

        private void ShootFireball()
        {
            canTurn = true;

            Transform spawnLocation = stateMachine.bossData.ProjectileShootingLocation;

            FireballProjectile newProjecitle = Instantiate<FireballProjectile>(basicFireballProjectile, spawnLocation.position, spawnLocation.rotation);
            OnSpawnedFireball?.Invoke(newProjecitle);
            newProjecitle.SetFinalAttackEntites(stateMachine.bossData.BossCenterPoint, stateMachine.bossData.XavianGameManager.Player.transform);
            newProjecitle.transform.forward = stateMachine.bossData.transform.forward;

            if (indexToShootReflectableFireballs.Count > 0
                && indexToShootReflectableFireballs.Contains(currentShootProjectileCounter))
            {
                newProjecitle.EnableReflect();
            }


            currentShootProjectileCounter++;

            if (currentShootProjectileCounter < timesToShootProjectile)
            {

                float currentTime = stateMachine.bossData.BossAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                stateMachine.bossData.BossAnimator.Play(0, -1, currentTime - 0.04f);

                currentAnimAnticipationSpeed = 1f;
                stateMachine.bossData.BossAnimator.SetFloat("FireballShotReverseValue", -reverseAnimSpeed);
            }



        }




    }
}