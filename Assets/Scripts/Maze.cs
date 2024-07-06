using UnityEngine;



public class Maze : MonoBehaviour {

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField, HideInInspector]
    private Vector2Int size = new(10, 10);

    [SerializeField, HideInInspector]
    private int seed;

    [SerializeField, HideInInspector]
    private bool isOptimized;

    public Vector2Int Size => size;

    public int Seed => seed;

    public bool IsOptimized => isOptimized;

    private readonly MapGenerator m_mapGenerator = new();
    private readonly MazeGenerator m_mazeGenerator = new();


    public void CreateNewMaze () {
        meshFilter.mesh = m_mazeGenerator.GenerateMesh(m_mapGenerator.GenerateMap(Size, Seed), IsOptimized);
    }


    public void CreateNewMaze (Vector2Int size, int seed, bool isOptimized) {
        meshFilter.mesh = m_mazeGenerator.GenerateMesh(m_mapGenerator.GenerateMap(size, seed), isOptimized);
    }

}