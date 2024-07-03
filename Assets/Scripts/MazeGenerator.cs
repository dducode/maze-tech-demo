using UnityEngine;



public class MazeGenerator {

    public Mesh GenerateMesh (Map map, bool isOptimized) {
        int wallsCount = isOptimized ? OptimizedCountWalls(map) : CountWalls(map);
        const int exteriorWallVertices = 12;
        const int exteriorWallIndexes = 18;
        var meshBuilder = new MeshBuilder(wallsCount, 1, exteriorWallVertices * 4, exteriorWallIndexes * 4);

        GenerateFloor(meshBuilder, map);
        GenerateWalls(meshBuilder, map, isOptimized);

        return meshBuilder.BuildMesh();
    }


    private int OptimizedCountWalls (Map map) {
        var wallsCount = 0;

        // count vertical walls
        for (var x = 0; x < map.Size.x - 1; x++) {
            for (var y = 0; y < map.Size.y; y++) {
                if (!map[x, y].IsConnectWithRightCell()) {
                    while (y < map.Size.y && !map[x, y].IsConnectWithRightCell())
                        y++;
                    wallsCount++;
                }
            }
        }

        // count horizontal walls
        for (var y = 0; y < map.Size.y - 1; y++) {
            for (var x = 0; x < map.Size.x; x++) {
                if (!map[x, y].IsConnectWithLowerCell()) {
                    while (x < map.Size.x && !map[x, y].IsConnectWithLowerCell())
                        x++;
                    wallsCount++;
                }
            }
        }

        return wallsCount;
    }


    private int CountWalls (Map map) {
        var wallsCount = 0;

        for (var y = 0; y < map.Size.y; y++) {
            for (var x = 0; x < map.Size.x; x++) {
                Cell cell = map[x, y];
                if (y < map.Size.y - 1 && !cell.IsConnectWithLowerCell())
                    wallsCount++;
                if (x < map.Size.x - 1 && !cell.IsConnectWithRightCell())
                    wallsCount++;
            }
        }

        return wallsCount;
    }


    private void GenerateFloor (MeshBuilder meshBuilder, Map map) {
        meshBuilder.BeginFormSubMesh();
        meshBuilder.AddPlane(Vector3.zero, Quaternion.Euler(90, 0, 0), map.Size);
        meshBuilder.EndFormSubMesh();
    }


    private void GenerateWalls (MeshBuilder meshBuilder, Map map, bool isOptimized) {
        meshBuilder.BeginFormSubMesh();

        GenerateExteriorWalls(meshBuilder, map.Size);

        if (isOptimized)
            OptimizedGenerateInteriorWalls(meshBuilder, map);
        else
            GenerateInteriorWalls(meshBuilder, map);

        meshBuilder.EndFormSubMesh();
    }


    private void GenerateExteriorWalls (MeshBuilder meshBuilder, Vector2Int mapSize) {
        const float wallThickness = 0.2f;

        float x = mapSize.x * 0.5f;
        float y = mapSize.y * 0.5f;

        GenerateFrontWall();
        GenerateBackWall();
        GenerateLeftWall();
        GenerateRightWall();
        return;

        void GenerateFrontWall () {
            // front side
            meshBuilder.AddPlane(
                new Vector3(0, 0.5f, y + wallThickness),
                Quaternion.Euler(0, 180, 0),
                new Vector2((x + wallThickness) * 2, 1)
            );

            // back side
            meshBuilder.AddPlane(
                new Vector3(0, 0.5f, y),
                Quaternion.identity,
                new Vector2(x * 2, 1)
            );

            int i = meshBuilder.VerticesCount;

            // top side
            meshBuilder.AddVertices(
                new Vector3(-(x + wallThickness), 1, y + wallThickness),
                new Vector3(x + wallThickness, 1, y + wallThickness),
                new Vector3(-x, 1, y),
                new Vector3(x, 1, y)
            );

            meshBuilder.AddIndexes(i, i + 1, i + 2, i + 2, i + 1, i + 3);
        }

        void GenerateBackWall () {
            // front side
            meshBuilder.AddPlane(
                new Vector3(0, 0.5f, -(y + wallThickness)),
                Quaternion.identity,
                new Vector2((x + wallThickness) * 2, 1)
            );

            // back side
            meshBuilder.AddPlane(
                new Vector3(0, 0.5f, -y),
                Quaternion.Euler(0, 180, 0),
                new Vector2(x * 2, 1)
            );

            int i = meshBuilder.VerticesCount;

            // top side
            meshBuilder.AddVertices(
                new Vector3(x + wallThickness, 1, -(y + wallThickness)),
                new Vector3(-(x + wallThickness), 1, -(y + wallThickness)),
                new Vector3(x, 1, -y),
                new Vector3(-x, 1, -y)
            );

            meshBuilder.AddIndexes(i, i + 1, i + 2, i + 2, i + 1, i + 3);
        }

        void GenerateLeftWall () {
            // front side
            meshBuilder.AddPlane(
                new Vector3(-(x + wallThickness), 0.5f, 0),
                Quaternion.Euler(0, 90, 0),
                new Vector2((y + wallThickness) * 2, 1)
            );

            // back side
            meshBuilder.AddPlane(
                new Vector3(-x, 0.5f, 0),
                Quaternion.Euler(0, -90, 0),
                new Vector2(y * 2, 1)
            );

            int i = meshBuilder.VerticesCount;

            // top side
            meshBuilder.AddVertices(
                new Vector3(-(x + wallThickness), 1, -(y + wallThickness)),
                new Vector3(-(x + wallThickness), 1, y + wallThickness),
                new Vector3(-x, 1, -y),
                new Vector3(-x, 1, y)
            );

            meshBuilder.AddIndexes(i, i + 1, i + 2, i + 2, i + 1, i + 3);
        }

        void GenerateRightWall () {
            // front side
            meshBuilder.AddPlane(
                new Vector3(x + wallThickness, 0.5f, 0),
                Quaternion.Euler(0, -90, 0),
                new Vector2((y + wallThickness) * 2, 1)
            );

            // back side
            meshBuilder.AddPlane(
                new Vector3(x, 0.5f, 0),
                Quaternion.Euler(0, 90, 0),
                new Vector2(y * 2, 1)
            );

            int i = meshBuilder.VerticesCount;

            // top side
            meshBuilder.AddVertices(
                new Vector3(x + wallThickness, 1, y + wallThickness),
                new Vector3(x + wallThickness, 1, -(y + wallThickness)),
                new Vector3(x, 1, y),
                new Vector3(x, 1, -y)
            );

            meshBuilder.AddIndexes(i, i + 1, i + 2, i + 2, i + 1, i + 3);
        }
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


    private void OptimizedGenerateInteriorWalls (MeshBuilder meshBuilder, Map map) {
        GenerateVerticalWalls(meshBuilder, map);
        GenerateHorizontalWalls(meshBuilder, map);
    }


    private void GenerateVerticalWalls (MeshBuilder meshBuilder, Map map) {
        const float wallThickness = 0.2f;

        for (var x = 0; x < map.Size.x - 1; x++) {
            for (var y = 0; y < map.Size.y; y++) {
                if (!map[x, y].IsConnectWithRightCell()) {
                    Vector2Int startPosition = map[x, y].Position;
                    var wallLength = 0;

                    while (y < map.Size.y && !map[x, y].IsConnectWithRightCell()) {
                        wallLength++;
                        y++;
                    }

                    Vector2Int endPosition = map[x, y - 1].Position;
                    Vector3 wallCenter = Vector3.Lerp(
                        map.MapToMazePosition(startPosition), map.MapToMazePosition(endPosition), 0.5f
                    );
                    GenerateVerticalWall(meshBuilder, wallCenter, wallThickness, wallLength);
                }
            }
        }
    }


    private void GenerateHorizontalWalls (MeshBuilder meshBuilder, Map map) {
        const float wallThickness = 0.2f;

        for (var y = 0; y < map.Size.y - 1; y++) {
            for (var x = 0; x < map.Size.x; x++) {
                if (!map[x, y].IsConnectWithLowerCell()) {
                    Vector2Int startPosition = map[x, y].Position;
                    var wallLength = 0;

                    while (x < map.Size.x && !map[x, y].IsConnectWithLowerCell()) {
                        wallLength++;
                        x++;
                    }

                    Vector2Int endPosition = map[x - 1, y].Position;
                    Vector3 wallCenter = Vector3.Lerp(
                        map.MapToMazePosition(startPosition), map.MapToMazePosition(endPosition), 0.5f
                    );
                    GenerateHorizontalWall(meshBuilder, wallCenter, wallThickness, wallLength);
                }
            }
        }
    }


    private void GenerateVerticalWall (MeshBuilder meshBuilder, Vector3 position, float wallThickness, int length) {
        var wallPosition = new Vector3(position.x + 0.5f, 0.5f, position.z);
        var wallSize = new Vector3(wallThickness, 1, length);
        meshBuilder.AddCube(wallPosition, wallSize);
    }


    private void GenerateHorizontalWall (MeshBuilder meshBuilder, Vector3 position, float wallThickness, int length) {
        var wallPosition = new Vector3(position.x, 0.5f, position.z - 0.5f);
        var wallSize = new Vector3(length + wallThickness, 1, wallThickness);
        meshBuilder.AddCube(wallPosition, wallSize);
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