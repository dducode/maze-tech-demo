using System;
using UnityEngine;
using Random = System.Random;



public class Map {

    public Vector2Int Size { get; }

    public Cell this [int x, int y] {
        get {
            if (x < 0 || Size.x <= x) {
                throw new ArgumentOutOfRangeException(nameof(x), x,
                    "Argument was outside of bounds of the map"
                );
            }

            if (y < 0 || Size.y <= y) {
                throw new ArgumentOutOfRangeException(nameof(y), y,
                    "Argument was outside of bounds of the map"
                );
            }

            return cells[x, y];
        }
    }

    public Cell this [Vector2Int position] => this[position.x, position.y];

    private readonly Cell[,] cells;
    private readonly Matrix4x4 m_mapToMazeMatrix;


    public Map (Vector2Int size) {
        Size = size;
        cells = new Cell[size.x, size.y];
        m_mapToMazeMatrix = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 0, -1, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(-Size.x * 0.5f + 0.5f, 0, Size.y * 0.5f - 0.5f, 0)
        );

        for (var x = 0; x < size.x; x++) {
            for (var y = 0; y < size.y; y++) {
                cells[x, y] = new Cell {
                    Position = new Vector2Int(x, y)
                };
            }
        }
    }


    public Cell GetRandomCell (Random random) {
        return this[random.Next(0, Size.x), random.Next(0, Size.y)];
    }


    public bool CheckBounds (Vector2Int position) {
        return position.x >= 0 && position.x < Size.x && position.y >= 0 && position.y < Size.y;
    }


    public Vector3 MapToMazePosition (Vector2Int position) {
        return m_mapToMazeMatrix * new Vector4(position.x, position.y, 0, 1);
    }

}