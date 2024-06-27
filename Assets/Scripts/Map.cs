using System;
using UnityEngine;
using Random = UnityEngine.Random;



public class Map {

    public Vector2Int Size { get; }

    public Cell this [Vector2Int position] {
        get {
            if (position.x < 0 || Size.x <= position.x) {
                throw new ArgumentOutOfRangeException(nameof(position.x), position.x,
                    "Argument was outside of bounds of the map"
                );
            }

            if (position.y < 0 || Size.y <= position.y) {
                throw new ArgumentOutOfRangeException(nameof(position.y), position.y,
                    "Argument was outside of bounds of the map"
                );
            }

            return cells[position.x, position.y];
        }
    }

    private readonly Cell[,] cells;


    public Map (Vector2Int size) {
        Size = size;
        cells = new Cell[size.x, size.y];

        for (var x = 0; x < size.x; x++) {
            for (var y = 0; y < size.y; y++) {
                cells[x, y] = new Cell {
                    Position = new Vector2Int(x, y)
                };
            }
        }
    }


    public Cell GetRandomCell () {
        return this[new Vector2Int(Random.Range(0, Size.x), Random.Range(0, Size.y))];
    }


    public bool CheckBounds (Vector2Int position) {
        return position.x >= 0 && position.x < Size.x && position.y >= 0 && position.y < Size.y;
    }

}