using UnityEngine;



public class Maze : MonoBehaviour {

    [SerializeField]
    private MeshFilter meshFilter;

    private readonly MapGenerator m_mapGenerator = new();
    private readonly MazeGenerator m_mazeGenerator = new();


    public void CreateNewMaze (Vector2Int mazeSize, int seed) {
        meshFilter.mesh = m_mazeGenerator.GenerateMesh(m_mapGenerator.GenerateMap(mazeSize, seed));
    }

}