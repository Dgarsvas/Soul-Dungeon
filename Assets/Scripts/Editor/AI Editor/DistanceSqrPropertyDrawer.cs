using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(SqrDistance))]
public class DistanceSqrPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var actualDistanceRect = new Rect(position.x, position.y, 60, position.height);
        var sqrDistanceRect = new Rect(position.x + 60, position.y, 60, position.height);

        EditorGUI.PropertyField(actualDistanceRect, property.FindPropertyRelative("actualDistance"), GUIContent.none);
        property.FindPropertyRelative("sqrDistance").floatValue = Mathf.Pow(property.FindPropertyRelative("actualDistance").floatValue, 2);

        GUI.enabled = false;
        EditorGUI.PropertyField(sqrDistanceRect, property.FindPropertyRelative("sqrDistance"), GUIContent.none);
        GUI.enabled = true;


        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}