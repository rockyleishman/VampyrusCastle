using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LocalWave))]
public class LocalWaveEditorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //begin drawer
        EditorGUI.BeginProperty(position, label, property);

        //label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        //remove identation
        int defaultIndentLevel = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        //rects
        Rect spawnLocationRect = new Rect(position.x, position.y, 30, position.height);
        Rect spacingTimeRect = new Rect(position.x + 35, position.y, position.width, position.height);
        Rect enemiesRect = new Rect(position.x + 35, position.y, position.width, position.height);

        //fields
        EditorGUI.PropertyField(spawnLocationRect, property.FindPropertyRelative("SpawnLocation"), GUIContent.none);
        EditorGUI.PropertyField(spacingTimeRect, property.FindPropertyRelative("SpacingTime"), GUIContent.none);
        EditorGUI.PropertyField(enemiesRect, property.FindPropertyRelative("Enemies"), GUIContent.none);

        //restore indentation
        EditorGUI.indentLevel = defaultIndentLevel;

        //end drawer
        EditorGUI.EndProperty();
    }
}
