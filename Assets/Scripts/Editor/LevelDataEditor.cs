using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    SerializedProperty saveChanges;

    private void OnEnable()
    {
        //init serialized properties
        saveChanges = serializedObject.FindProperty("DoNotDeleteThisBool");

        //reference target as LevelData data
        LevelData data = (LevelData)target;
        data.DoNotDeleteThisBool = false;
    }

    public override void OnInspectorGUI()
    {
        //draw base components of inspector
        base.OnInspectorGUI();

        //update serialized properties
        serializedObject.Update();

        //reference target as LevelData data
        LevelData data = (LevelData)target;

        //limit array sizes if user tries to break limit
        if (data.SpawnLocations.Length > LevelData.MaxSpawnLocations)
        {
            Vector2[] newArray = new Vector2[LevelData.MaxSpawnLocations];
            System.Array.Copy(data.SpawnLocations, newArray, LevelData.MaxSpawnLocations);
            data.SpawnLocations = newArray;
        }
        for (int i = 0; i < LevelData.MaxWaveCount; i++)
        {
            for (int j = 0; j < LevelData.MaxSpawnLocations; j++)
            {
                if (data.EnemyCounts[i, j] > LevelData.MaxEnemiesPerSpawnLocation)
                {
                    data.EnemyCounts[i, j] = LevelData.MaxEnemiesPerSpawnLocation;
                }
            }
        }

        //save changes button
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("AUTOSAVE CHANGES", EditorStyles.boldLabel, GUILayout.Width(180));
        EditorGUILayout.PropertyField(saveChanges, GUIContent.none, GUILayout.Width(60));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("Changes made below will only save if this is checked.", EditorStyles.miniLabel);

        //each wave
        for (int i = 0; i < data.WaveCount; i++)
        {
            //space between waves
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();

            //Header & Time Limit fields
            EditorGUILayout.BeginHorizontal();
            if (i < data.WaveCount - 1)
            {
                EditorGUILayout.LabelField("Wave " + (i + 1), EditorStyles.boldLabel, GUILayout.Width(120));
                EditorGUILayout.LabelField("Time Limit (s):", GUILayout.Width(120));
                data.WaveTimeLimits[i] = EditorGUILayout.FloatField(data.WaveTimeLimits[i]);
            }
            else
            {
                EditorGUILayout.LabelField("Wave " + (i + 1) + " (Final)", EditorStyles.boldLabel, GUILayout.Width(120));
                EditorGUILayout.LabelField("No Time Limit");
            }
            EditorGUILayout.EndHorizontal();

            //Enemy Count & Spacing Time fields for each spawn location
            for (int j = 0; j < data.SpawnLocations.Length; j++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Location " + j + " (" + data.SpawnLocations[j].x + ", " + data.SpawnLocations[j].y + ")", GUILayout.Width(120));
                EditorGUILayout.LabelField("Enemy Count:", GUILayout.Width(120));
                data.EnemyCounts[i, j] = EditorGUILayout.IntField(data.EnemyCounts[i, j], GUILayout.Width(60));
                EditorGUILayout.LabelField("Spacing (s):", GUILayout.Width(120));
                data.SpacingTimes[i, j] = EditorGUILayout.FloatField(data.SpacingTimes[i, j], GUILayout.Width(60));
                EditorGUILayout.EndHorizontal();

                //Enemy Type fields for each enemy
                for (int k = 0; k < data.EnemyCounts[i, j]; k++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(120));
                    EditorGUILayout.LabelField("Enemy " + (k + 1) + ":", EditorStyles.miniLabel, GUILayout.Width(120));
                    data.Enemies[i, j, k] = (EnemyType)EditorGUILayout.EnumPopup(data.Enemies[i, j, k]);
                    EditorGUILayout.EndHorizontal();
                }
            }            
        }

        serializedObject.ApplyModifiedProperties();
    }
}
