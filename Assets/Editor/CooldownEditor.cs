using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(Utility.Cooldown))]
public class CooldownEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("time"), GUIContent.none);
        EditorGUI.EndProperty();
    }
}
