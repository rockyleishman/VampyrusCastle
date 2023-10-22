using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Wave))]
public class WaveEditorDrawer : PropertyDrawer
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
        Rect timeLimitRect = new Rect(position.x, position.y, 30, position.height);
        Rect localWaveRect = new Rect(position.x + 35, position.y, position.width, position.height);

        //fields
        EditorGUI.PropertyField(timeLimitRect, property.FindPropertyRelative("WaveTimeLimit"), GUIContent.none);
        EditorGUI.PropertyField(localWaveRect, property.FindPropertyRelative("LocalWaves"), GUIContent.none);

        //restore indentation
        EditorGUI.indentLevel = defaultIndentLevel;

        //end drawer
        EditorGUI.EndProperty();
    }
}
