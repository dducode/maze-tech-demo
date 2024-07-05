using UnityEditor;
using UnityEditor.Formats.Fbx.Exporter;
using UnityEngine;



[CustomEditor(typeof(Maze))]
public class MazeEditor : Editor {

    private SerializedProperty m_sizeProperty;
    private SerializedProperty m_seedProperty;
    private SerializedProperty m_isOptimizedProperty;

    private Maze m_maze;
    private MeshFilter m_meshFilter;
    private Map m_map;


    private void OnEnable () {
        m_maze = (Maze)target;
        m_meshFilter = m_maze.GetComponent<MeshFilter>();
        m_map = new Map(m_maze.Size);

        m_sizeProperty = serializedObject.FindProperty("size");
        m_seedProperty = serializedObject.FindProperty("seed");
        m_isOptimizedProperty = serializedObject.FindProperty("isOptimized");
    }


    public override void OnInspectorGUI () {
        base.OnInspectorGUI();
        serializedObject.Update();

        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(m_sizeProperty);
        Vector2Int size = m_sizeProperty.vector2IntValue;
        size.x = Mathf.Max(size.x, 4);
        size.y = Mathf.Max(size.y, 4);
        m_sizeProperty.vector2IntValue = size;

        EditorGUILayout.PropertyField(m_seedProperty);
        EditorGUILayout.PropertyField(m_isOptimizedProperty);

        if (EditorGUI.EndChangeCheck()) {
            m_map = new Map(size);
            SceneView.RepaintAll();
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.Space(15);
        if (GUILayout.Button("Create New Maze"))
            m_maze.CreateNewMaze();

        using (new EditorGUILayout.HorizontalScope()) {
            if (m_meshFilter.sharedMesh != null) {
                if (GUILayout.Button("Export Mesh"))
                    ExportMesh(m_meshFilter.sharedMesh);
                if (GUILayout.Button("Export FBX"))
                    ExportFBX(m_meshFilter.sharedMesh);
            }
        }
    }


    private void ExportMesh (Mesh mesh) {
        if (!AssetDatabase.IsValidFolder("Assets/GeneratedAssets"))
            AssetDatabase.CreateFolder("Assets", "GeneratedAssets");

        AssetDatabase.CreateAsset(
            mesh, AssetDatabase.GenerateUniqueAssetPath($"Assets/GeneratedAssets/{mesh.name}.mesh")
        );
        EditorUtility.FocusProjectWindow();
        EditorGUIUtility.PingObject(mesh);
        Selection.SetActiveObjectWithContext(mesh, mesh);
    }


    private void ExportFBX (Mesh mesh) {
        if (!AssetDatabase.IsValidFolder("Assets/GeneratedAssets"))
            AssetDatabase.CreateFolder("Assets", "GeneratedAssets");

        var fbx = new GameObject(mesh.name);
        fbx.AddComponent<MeshFilter>().sharedMesh = mesh;
        fbx.AddComponent<MeshRenderer>();
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"Assets/GeneratedAssets/{mesh.name}.fbx");
        string exportAssetPath = ModelExporter.ExportObject(
            System.IO.Path.Combine(System.IO.Path.GetFullPath(assetPath)), fbx
        );
        AssetDatabase.Refresh();
        DestroyImmediate(fbx);

        if (!string.IsNullOrEmpty(exportAssetPath)) {
            string relativePath = System.IO.Path.GetRelativePath(Application.dataPath, exportAssetPath);
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(relativePath);
            Selection.SetActiveObjectWithContext(asset, asset);
            EditorUtility.FocusProjectWindow();
        }
        else {
            Debug.LogError("Error while exporting FBX asset");
        }
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
        Vector3 topLeft = m_maze.transform.TransformPoint(center + new Vector3(-0.5f, 0, 0.5f));
        Vector3 topRight = m_maze.transform.TransformPoint(center + new Vector3(0.5f, 0, 0.5f));
        Vector3 bottomRight = m_maze.transform.TransformPoint(center + new Vector3(0.5f, 0, -0.5f));
        Vector3 bottomLeft = m_maze.transform.TransformPoint(center + new Vector3(-0.5f, 0, -0.5f));
        Handles.DrawPolyLine(topLeft, topRight, bottomRight, bottomLeft, topLeft);
    }

}