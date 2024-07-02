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

    private readonly MapGenerator m_mapGenerator = new();
    private readonly MazeGenerator m_mazeGenerator = new();


    public void CreateNewMaze () {
        meshFilter.mesh = m_mazeGenerator.GenerateMesh(m_mapGenerator.GenerateMap(Size, seed), isOptimized);
    }

}