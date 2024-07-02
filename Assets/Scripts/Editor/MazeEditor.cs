using System;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(Maze))]
public class MazeEditor : Editor {

    private const string MazeSizeKey = "maze-size";
    private const string SeedKey = "seed";

    private Maze m_maze;
    private Map m_map;
    private Vector2Int m_mazeSize;
    private int m_seed;


    private void OnEnable () {
        m_maze = (Maze)target;
        m_mazeSize = ExtendedEditorPrefs.GetVector2Int(MazeSizeKey, new Vector2Int(10, 10));
        m_map = new Map(m_mazeSize);
        m_seed = EditorPrefs.GetInt(SeedKey);
    }


    public override void OnInspectorGUI () {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        m_seed = EditorGUILayout.IntField("Seed", m_seed);
        m_mazeSize = EditorGUILayout.Vector2IntField("Maze Size", m_mazeSize);
        m_mazeSize.x = Math.Max(m_mazeSize.x, 4);
        m_mazeSize.y = Math.Max(m_mazeSize.y, 4);

        if (EditorGUI.EndChangeCheck()) {
            EditorPrefs.SetInt(SeedKey, m_seed);
            ExtendedEditorPrefs.SetVector2Int(MazeSizeKey, m_mazeSize);
            m_map = new Map(m_mazeSize);
            SceneView.RepaintAll();
        }

        EditorGUILayout.Space(15);
        if (GUILayout.Button("Create New Maze"))
            m_maze.CreateNewMaze(m_mazeSize, m_seed);
    }


    private void OnSceneGUI () {
        DrawMap(m_map);
    }


    private void DrawMap (Map map) {
        Color color = Handles.color;
        Handles.color = Color.green;

        for (var x = 0; x < map.Size.x; x++)
            for (var y = 0; y < map.Size.y; y++)
                DrawCell(map.MapToMazePosition(new Vector2Int(x, y)));

        Handles.color = color;
    }


    private void DrawCell (Vector3 center) {
        Handles.DrawPolyLine(
            MazeToWorldPosition(center + new Vector3(-0.5f, 0, 0.5f)),
            MazeToWorldPosition(center + new Vector3(0.5f, 0, 0.5f)),
            MazeToWorldPosition(center + new Vector3(0.5f, 0, -0.5f)),
            MazeToWorldPosition(center + new Vector3(-0.5f, 0, -0.5f)),
            MazeToWorldPosition(center + new Vector3(-0.5f, 0, 0.5f))
        );
    }


    private Vector3 MazeToWorldPosition (Vector3 position) {
        return m_maze.transform.localToWorldMatrix * new Vector4(position.x, position.y, position.z, 1);
    }

}