using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelData data = (LevelData)target;

        EditorGUILayout.LabelField("Wave Settings", EditorStyles.boldLabel);


    }
}
