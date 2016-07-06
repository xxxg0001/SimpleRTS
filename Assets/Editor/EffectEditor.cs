
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;





[CustomEditor(typeof(EffectContainer))]
public class EffectEditor : Editor {

    
    SerializedProperty effectsProperty;
    void OnEnable()
    {
        effectsProperty = serializedObject.FindProperty("effectConfig");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //var model = target as EffectContainer;
        int count = effectsProperty.arraySize;
        count = EditorGUILayout.IntField("Count", count);
        if (count != effectsProperty.arraySize)
        {
            while (count > effectsProperty.arraySize)
            {
                effectsProperty.InsertArrayElementAtIndex(effectsProperty.arraySize);
            }
            while (count < effectsProperty.arraySize)
            {
                effectsProperty.DeleteArrayElementAtIndex(effectsProperty.arraySize - 1);
            }
        }
        EditorGUI.indentLevel++;
        for (int i = 0; i < effectsProperty.arraySize; i++)
        {
            SerializedProperty effectsPropertyRef = effectsProperty.GetArrayElementAtIndex(i);
            SerializedProperty effectTypeProperty = effectsPropertyRef.FindPropertyRelative("effectType");
            SerializedProperty paramProperty = effectsPropertyRef.FindPropertyRelative("param");
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(effectTypeProperty);            
            EffectType effectType = (EffectType)effectTypeProperty.enumValueIndex;
            if (effectType != EffectType.None)
            {
                var name = System.Enum.GetName(typeof(EffectType), effectType);
                EditorGUILayout.PropertyField(paramProperty, new GUIContent(name));
            }
        }
        EditorGUI.indentLevel --;

        serializedObject.ApplyModifiedProperties();
        


        //base.OnInspectorGUI();

    }
    
}
