using UnityEditor;
using UnityEngine;



public static class ExtendedEditorPrefs {

    public static Vector2Int GetVector2Int (string key, Vector2Int defaultValue) {
        return new Vector2Int(
            EditorPrefs.GetInt($"{key}-x", defaultValue.x),
            EditorPrefs.GetInt($"{key}-y", defaultValue.y)
        );
    }


    public static void SetVector2Int (string key, Vector2Int value) {
        EditorPrefs.SetInt($"{key}-x", value.x);
        EditorPrefs.SetInt($"{key}-y", value.y);
    }

}