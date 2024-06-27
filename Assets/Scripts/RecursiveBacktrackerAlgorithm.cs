using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class RecursiveBacktrackerAlgorithm {

    public Map GenerateMap (Vector2Int size) {
        var map = new Map(size);
        Cell cell = map.GetRandomCell();
        cell.inMaze = true;
        MoveNext(cell, map);
        return map;
    }


    private void MoveNext (Cell cell, Map map) {
        int x = cell.Position.x;
        int y = cell.Position.y;

        var upperPosition = new Vector2Int(x, y - 1);
        var lowerPosition = new Vector2Int(x, y + 1);
        var leftPosition = new Vector2Int(x - 1, y);
        var rightPosition = new Vector2Int(x + 1, y);

        var sides = new List<Cell.Side>();

        while (true) {
            if (map.CheckBounds(upperPosition) && !map[upperPosition].inMaze)
                sides.Add(Cell.Side.Top);
            if (map.CheckBounds(lowerPosition) && !map[lowerPosition].inMaze)
                sides.Add(Cell.Side.Bottom);
            if (map.CheckBounds(leftPosition) && !map[leftPosition].inMaze)
                sides.Add(Cell.Side.Left);
            if (map.CheckBounds(rightPosition) && !map[rightPosition].inMaze)
                sides.Add(Cell.Side.Right);

            if (sides.Count == 0)
                return;

            Cell.Side side = sides[Random.Range(0, sides.Count)];

            switch (side) {
                case Cell.Side.Top:
                    Cell upperCell = map[upperPosition];
                    cell.ConnectUpperCell(upperCell);
                    MoveNext(upperCell, map);
                    break;
                case Cell.Side.Bottom:
                    Cell lowerCell = map[lowerPosition];
                    cell.ConnectLowerCell(lowerCell);
                    MoveNext(lowerCell, map);
                    break;
                case Cell.Side.Left:
                    Cell leftCell = map[leftPosition];
                    cell.ConnectLeftCell(leftCell);
                    MoveNext(leftCell, map);
                    break;
                case Cell.Side.Right:
                    Cell rightCell = map[rightPosition];
                    cell.ConnectRightCell(rightCell);
                    MoveNext(rightCell, map);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            sides.Clear();
        }
    }

}