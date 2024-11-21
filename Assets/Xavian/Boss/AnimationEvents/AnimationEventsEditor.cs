using System;
using UnityEditor;
using UnityEngine;

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


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();


            BasicAttackPropertiesFoldoutAndUpdate();
            FireballPropertiesFoldoutAndUpdate();


            serializedObject.ApplyModifiedProperties();

        }


        private void OnEnable()
        {
            bossAnimationEvents = (BossAnimationEvents)target;

            BasicAttackPropertiesSetup();
            FireballShotPropertiesSetup();
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

    }
}