using UnityEngine;



public class MazeGridDrawer : MonoBehaviour {

    [SerializeField]
    private Maze maze;

    [SerializeField]
    private UserInterface userInterface;


    private void LateUpdate () {
        DrawGrid();
    }


    private void DrawGrid () {
        Vector2Int size = userInterface.InputSize;
        var map = new Map(size);

        for (var x = 0; x < size.x; x++)
            for (var y = 0; y < size.y; y++)
                DrawCell(map.MapToMazePosition(new Vector2Int(x, y)));
    }


    private void DrawCell (Vector3 center) {
        Vector3 topLeft = maze.transform.TransformPoint(center + new Vector3(-0.5f, 0, 0.5f));
        Vector3 topRight = maze.transform.TransformPoint(center + new Vector3(0.5f, 0, 0.5f));
        Vector3 bottomRight = maze.transform.TransformPoint(center + new Vector3(0.5f, 0, -0.5f));
        Vector3 bottomLeft = maze.transform.TransformPoint(center + new Vector3(-0.5f, 0, -0.5f));

        Debug.DrawLine(topLeft, topRight, Color.green);
        Debug.DrawLine(topRight, bottomRight, Color.green);
        Debug.DrawLine(bottomRight, bottomLeft, Color.green);
        Debug.DrawLine(bottomLeft, topLeft, Color.green);
    }

}