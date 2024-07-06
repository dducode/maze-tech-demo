using UnityEngine;



public class MazeGridDrawer : MonoBehaviour {

    [SerializeField]
    private Maze maze;

    [SerializeField]
    private UserInterface userInterface;

    [SerializeField]
    private Material gridMaterial;


    private void OnPostRender () {
        DrawGrid();
    }


    private void DrawGrid () {
        Vector2Int size = userInterface.InputSize;
        var map = new Map(size);

        GL.PushMatrix();
        gridMaterial.SetPass(0);
        GL.Begin(GL.LINES);

        for (var x = 0; x < size.x; x++)
            for (var y = 0; y < size.y; y++)
                DrawCell(map.MapToMazePosition(new Vector2Int(x, y)));

        GL.End();
        GL.PopMatrix();
    }


    private void DrawCell (Vector3 center) {
        Vector3 topLeft = maze.transform.TransformPoint(center + new Vector3(-0.5f, 0, 0.5f));
        Vector3 topRight = maze.transform.TransformPoint(center + new Vector3(0.5f, 0, 0.5f));
        Vector3 bottomRight = maze.transform.TransformPoint(center + new Vector3(0.5f, 0, -0.5f));
        Vector3 bottomLeft = maze.transform.TransformPoint(center + new Vector3(-0.5f, 0, -0.5f));

        GL.Vertex(topLeft);
        GL.Vertex(topRight);

        GL.Vertex(topRight);
        GL.Vertex(bottomRight);

        GL.Vertex(bottomRight);
        GL.Vertex(bottomLeft);

        GL.Vertex(bottomLeft);
        GL.Vertex(topLeft);
    }

}