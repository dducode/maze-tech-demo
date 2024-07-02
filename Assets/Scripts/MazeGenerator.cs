using UnityEngine;



public class MazeGenerator {

    public Mesh GenerateMesh (Map map) {
        var wallsCount = 4;

        for (var y = 0; y < map.Size.y; y++) {
            for (var x = 0; x < map.Size.x; x++) {
                Cell cell = map[x, y];
                if (!cell.IsConnectWithLowerCell())
                    wallsCount++;
                if (!cell.IsConnectWithRightCell())
                    wallsCount++;
            }
        }

        var meshBuilder = new MeshBuilder(wallsCount, 1);

        GenerateFloor(meshBuilder, map);
        GenerateWalls(meshBuilder, map);

        return meshBuilder.BuildMesh();
    }


    private void GenerateFloor (MeshBuilder meshBuilder, Map map) {
        meshBuilder.BeginFormSubMesh();
        meshBuilder.AddPlane(Vector3.zero, Quaternion.Euler(90, 0, 0), map.Size);
        meshBuilder.EndFormSubMesh();
    }


    private void GenerateWalls (MeshBuilder meshBuilder, Map map) {
        meshBuilder.BeginFormSubMesh();

        GenerateExteriorWalls(meshBuilder, map.Size);
        GenerateInteriorWalls(meshBuilder, map);

        meshBuilder.EndFormSubMesh();
    }


    private void GenerateExteriorWalls (MeshBuilder meshBuilder, Vector2Int mapSize) {
        float x = mapSize.x * 0.5f;
        float y = mapSize.y * 0.5f;
        int prevCount = meshBuilder.VerticesCount;

        const float wallThickness = 0.2f;

        // front wall
        meshBuilder.AddCube(
            new Vector3(0, 0.5f, y),
            new Vector3(mapSize.x + wallThickness, 1, wallThickness)
        );

        // back wall
        meshBuilder.AddCube(
            new Vector3(0, 0.5f, -y),
            new Vector3(mapSize.x + wallThickness, 1, wallThickness)
        );

        // left wall
        meshBuilder.AddCube(
            new Vector3(-x, 0.5f, 0),
            new Vector3(wallThickness, 1, mapSize.y)
        );

        // right wall
        meshBuilder.AddCube(
            new Vector3(x, 0.5f, 0),
            new Vector3(wallThickness, 1, mapSize.y)
        );

        for (int i = prevCount; i < meshBuilder.VerticesCount; i += 4)
            meshBuilder.AddIndexes(i, i + 1, i + 2, i + 2, i + 1, i + 3);
    }


    private void GenerateInteriorWalls (MeshBuilder meshBuilder, Map map) {
        const float wallThickness = 0.2f;

        for (var x = 0; x < map.Size.x; x++) {
            for (var y = 0; y < map.Size.y; y++) {
                Cell cell = map[x, y];
                if (y < map.Size.y - 1 && !cell.IsConnectWithLowerCell())
                    GenerateBottomWall(meshBuilder, map.MapToMazePosition(cell.Position), wallThickness);
                if (x < map.Size.x - 1 && !cell.IsConnectWithRightCell())
                    GenerateRightWall(meshBuilder, map.MapToMazePosition(cell.Position), wallThickness);
            }
        }
    }


    private void GenerateBottomWall (MeshBuilder meshBuilder, Vector3 cellPosition, float wallThickness) {
        var wallPosition = new Vector3(cellPosition.x, 0.5f, cellPosition.z - 0.5f);
        var wallSize = new Vector3(1 + wallThickness, 1, wallThickness);
        meshBuilder.AddCube(wallPosition, wallSize);
    }


    private void GenerateRightWall (MeshBuilder meshBuilder, Vector3 cellPosition, float wallThickness) {
        var wallPosition = new Vector3(cellPosition.x + 0.5f, 0.5f, cellPosition.z);
        var wallSize = new Vector3(wallThickness, 1, 1);
        meshBuilder.AddCube(wallPosition, wallSize);
    }

}