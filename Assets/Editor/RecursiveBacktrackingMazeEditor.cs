using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(RecursiveBacktrackingMaze))]
public class RecursiveBacktrackingMazeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Update serialized object for custom field handling
        serializedObject.Update();


        // Draw "Maze Settings" fields
        EditorGUILayout.LabelField("Maze Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("height"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("seed"));

        SerializedProperty startIdx = serializedObject.FindProperty("startIdx");
        startIdx.intValue = EditorGUILayout.Popup("Entry Position", startIdx.intValue, new string[] { "North", "East", "South", "West" });
        
        SerializedProperty exitIdx = serializedObject.FindProperty("exitIdx");
        exitIdx.intValue = EditorGUILayout.Popup("Exit Position", exitIdx.intValue, new string[] { "North", "East", "South", "West" });

        
        // Add buttons between Maze Settings and Tilemap settings
        EditorGUILayout.Space(); // Add some spacing for better visual separation
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Maze"))
        {
            ((RecursiveBacktrackingMaze)target).GenerateMaze();
        }
        if (GUILayout.Button("Clear Maze"))
        {
            ((RecursiveBacktrackingMaze)target).ClearMaze();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(); // Add spacing below the buttons

        // Draw "Tilemap, Tiles and Objects" fields
        EditorGUILayout.LabelField("Tilemaps", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("mazeTilemap"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wallTilemap"));
        //EditorGUILayout.PropertyField(serializedObject.FindProperty("openedTilemap"));
        
        EditorGUILayout.Space();
        
        EditorGUILayout.LabelField("Tiles", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pathTile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("wallRuleTile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("chestTile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("doorTile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("openedChestTile"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("openedDoorTile"));

        // Apply changes to serialized properties
        serializedObject.ApplyModifiedProperties();
    }
}