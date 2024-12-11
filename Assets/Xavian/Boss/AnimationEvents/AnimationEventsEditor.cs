using UnityEditor;

namespace Xavian
{
    [CustomEditor(typeof(BossAnimationEvents))]
    public class AnimationEventsEditor : Editor
    {

        BossAnimationEvents bossAnimationEvents;

        #region Basic Attack Events
        bool basicAttackDropdown = false;

        SerializedProperty startBasicAttack;
        SerializedProperty startBasicAttackAnticipation;
        SerializedProperty endBasicAttackAnticipation;
        SerializedProperty endBasicAttack;
        SerializedProperty startAttemptDamage;
        SerializedProperty endAttemptDamage;
        #endregion

        #region Fireball Shot Attack Events
        bool fireballAttackDropdown = false;

        SerializedProperty startFireballShot;
        SerializedProperty endFireballShot;
        SerializedProperty startFireballShotAnticipation;
        SerializedProperty endFireballShotAnticipation;
        SerializedProperty fireballShotLaunched;
        #endregion

        #region Hurt Events
        bool hurtDropdown = false;

        SerializedProperty startHurtEvent;
        SerializedProperty endHurtEvent;

        #endregion

        #region Roar Events
        bool roarDropdown = false;

        SerializedProperty startRoarEvent;
        SerializedProperty beginAttackRoarEvent;
        SerializedProperty endRoarEvent;

        #endregion

        #region Tail Spin Events

        bool tailSpinDropdown = false;

        SerializedProperty startTailSpinEvent;
        SerializedProperty tailSpinRotateEndEvent;
        SerializedProperty returnFromTailSwipeEvent;
        SerializedProperty endTailSpinEvent;

        #endregion

        #region Death Events

        bool deathDropdown = false;

        SerializedProperty startDeathEvent;
        SerializedProperty beforeDeathPeakEvent;
        SerializedProperty deathPeakEvent;
        SerializedProperty endDeathEvent;

        #endregion

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();


            BasicAttackPropertiesFoldoutAndUpdate();
            FireballPropertiesFoldoutAndUpdate();
            HurtPropertiesFoldoutAndUpdate();
            RoarPropertiesFoldoutAndUpdate();
            DeathPropertiesFoldoutAndUpdate();
            TailSpinPropertiesFoldoutAndUpdate();

            serializedObject.ApplyModifiedProperties();

        }


        private void OnEnable()
        {
            bossAnimationEvents = (BossAnimationEvents)target;

            BasicAttackPropertiesSetup();
            FireballShotPropertiesSetup();
            HurtAttackPropertiesSetup();
            RoarPropertiesSetup();
            DeathPropertiesSetup();
            TailSpinPropertiesSetup();
        }

        private void FireballShotPropertiesSetup()
        {
            startFireballShot = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartFireballShotAttack));
            endFireballShot = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndFireballShotAttack));
            startFireballShotAnticipation = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartFireballAnticipation));
            endFireballShotAnticipation = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndFireballAnticipation));
            fireballShotLaunched = serializedObject.FindProperty(nameof(bossAnimationEvents.OnFireballLaunched));
        }
        private void FireballPropertiesFoldoutAndUpdate()
        {
            fireballAttackDropdown = EditorGUILayout.Foldout(fireballAttackDropdown, "Fireball Shot Events", true);
            if (fireballAttackDropdown)
            {
                EditorGUILayout.PropertyField(startFireballShot);
                EditorGUILayout.PropertyField(startFireballShotAnticipation);
                EditorGUILayout.PropertyField(endFireballShotAnticipation);
                EditorGUILayout.PropertyField(endFireballShot);
                EditorGUILayout.PropertyField(fireballShotLaunched);
            }
        }

        private void BasicAttackPropertiesSetup()
        {
            startBasicAttack = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartBasicAttack));
            startBasicAttackAnticipation = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartBasicAttackAnticipation));
            endBasicAttackAnticipation = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndBasicAttackAnticipation));
            endBasicAttack = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndBasicAttack));
            startAttemptDamage = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartAttemptDamageBasicAttack));
            endAttemptDamage = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndAttemptDamageBasicAttack));
        }
        private void BasicAttackPropertiesFoldoutAndUpdate()
        {
            basicAttackDropdown = EditorGUILayout.Foldout(basicAttackDropdown, "Basic Attack Events", true);

            if (basicAttackDropdown)
            {
                EditorGUILayout.PropertyField(startBasicAttack);
                EditorGUILayout.PropertyField(startBasicAttackAnticipation);
                EditorGUILayout.PropertyField(endBasicAttackAnticipation);
                EditorGUILayout.PropertyField(endBasicAttack);
                EditorGUILayout.PropertyField(startAttemptDamage);
                EditorGUILayout.PropertyField(endAttemptDamage);

            }
        }

        private void HurtAttackPropertiesSetup()
        {
            startHurtEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartHurt));
            endHurtEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndHurt));
        }
        private void HurtPropertiesFoldoutAndUpdate()
        {
            hurtDropdown = EditorGUILayout.Foldout(hurtDropdown, "Hurt Events", true);
            if (hurtDropdown)
            {
                EditorGUILayout.PropertyField(startHurtEvent);
                EditorGUILayout.PropertyField(endHurtEvent);
            }
        }

        private void RoarPropertiesSetup()
        {
            startRoarEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartRoar));
            beginAttackRoarEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnBeginRoarAttack));
            endRoarEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndRoar));
        }
        private void RoarPropertiesFoldoutAndUpdate()
        {
            roarDropdown = EditorGUILayout.Foldout(roarDropdown, "Roar Events", true);
            if (roarDropdown)
            {
                EditorGUILayout.PropertyField(startRoarEvent);
                EditorGUILayout.PropertyField(beginAttackRoarEvent);
                EditorGUILayout.PropertyField(endRoarEvent);
            }
        }

        private void DeathPropertiesSetup()
        {
            startDeathEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartDeath));
            beforeDeathPeakEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnBeforeDeathPeak));
            deathPeakEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnDeathPeak));
            endDeathEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndDeath));
        }
        private void DeathPropertiesFoldoutAndUpdate()
        {
            deathDropdown = EditorGUILayout.Foldout(deathDropdown, "Death Events", true);
            if (deathDropdown)
            {
                EditorGUILayout.PropertyField(startDeathEvent);
                EditorGUILayout.PropertyField(beforeDeathPeakEvent);
                EditorGUILayout.PropertyField(deathPeakEvent);
                EditorGUILayout.PropertyField(endDeathEvent);
            }
        }

        private void TailSpinPropertiesSetup()
        {
            startTailSpinEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnStartTailSpin));
            tailSpinRotateEndEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnTailSpinRotateEnd));
            returnFromTailSwipeEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnReturnFromTailSwipe));
            endTailSpinEvent = serializedObject.FindProperty(nameof(bossAnimationEvents.OnEndTailSpin));
        }
        private void TailSpinPropertiesFoldoutAndUpdate()
        {
            tailSpinDropdown = EditorGUILayout.Foldout(tailSpinDropdown, "Tail Spin Events", true);

            if(tailSpinDropdown) 
            {
                EditorGUILayout.PropertyField(startTailSpinEvent);
                EditorGUILayout.PropertyField(tailSpinRotateEndEvent);
                EditorGUILayout.PropertyField(returnFromTailSwipeEvent);
                EditorGUILayout.PropertyField(endTailSpinEvent);
            }

        }


    }
}