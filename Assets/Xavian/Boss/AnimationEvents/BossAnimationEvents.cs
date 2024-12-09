﻿using UnityEngine;
using UnityEngine.Events;

namespace Xavian
{
    public class BossAnimationEvents : MonoBehaviour
    {
        #region Basic Attack Event Recievers and Invokers

        [HideInInspector] public UnityEvent OnStartBasicAttack;
        [HideInInspector] public UnityEvent OnEndBasicAttack;
        [HideInInspector] public UnityEvent OnStartBasicAttackAnticipation;
        [HideInInspector] public UnityEvent OnEndBasicAttackAnticipation;
        [HideInInspector] public UnityEvent OnStartAttemptDamageBasicAttack;
        [HideInInspector] public UnityEvent OnEndAttemptDamageBasicAttack;

        private void StartBasicAttack() => OnStartBasicAttack.Invoke();
        private void EndBasicAttack() => OnEndBasicAttack.Invoke();
        private void StartBasicAttackAnticipation() => OnStartBasicAttackAnticipation.Invoke();
        private void EndBasicAttackAnticipation() => OnEndBasicAttackAnticipation.Invoke();
        private void StartAttemptDamageBasicAttack() => OnStartAttemptDamageBasicAttack.Invoke();
        private void EndAttempDamageBasicAttack() => OnEndAttemptDamageBasicAttack.Invoke();

        #endregion

        #region Fireball Attack Event Recievers and Invokers

        [HideInInspector] public UnityEvent OnStartFireballShotAttack;
        [HideInInspector] public UnityEvent OnEndFireballShotAttack;
        [HideInInspector] public UnityEvent OnStartFireballAnticipation;
        [HideInInspector] public UnityEvent OnEndFireballAnticipation;
        [HideInInspector] public UnityEvent OnFireballLaunched;

        private void StartFireballShotAttack() => OnStartFireballShotAttack.Invoke();
        private void EndFireballShotAttack() => OnEndFireballShotAttack.Invoke();
        private void StartFireballAnticipation() => OnStartFireballAnticipation.Invoke();
        private void EndFireballAnticipation() => OnEndFireballAnticipation.Invoke();
        private void FireballLaunched() => OnFireballLaunched.Invoke();


        #endregion

        #region Hurt Event Recievers and Invokers

        [HideInInspector] public UnityEvent OnStartHurt;
        [HideInInspector] public UnityEvent OnEndHurt;

        private void StartHurt() => OnStartHurt.Invoke();
        private void EndHurt() => OnEndHurt.Invoke();

        #endregion

        #region Roar Event Recievers and Invokers

        [HideInInspector] public UnityEvent OnStartRoar;
        [HideInInspector] public UnityEvent OnBeginRoarAttack;
        [HideInInspector] public UnityEvent OnEndRoar;

        private void StartRoar() => OnStartRoar.Invoke();
        private void BeginRoarAttack() => OnBeginRoarAttack.Invoke();
        private void EndRoar() => OnEndRoar.Invoke();

        #endregion


    }






}