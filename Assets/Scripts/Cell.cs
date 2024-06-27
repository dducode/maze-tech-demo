using UnityEngine;



public class Cell {

    public bool inMaze;
    public Vector2Int Position { get; set; }
    private Cell UpperCell { get; set; }
    private Cell LowerCell { get; set; }
    private Cell RightCell { get; set; }
    private Cell LeftCell { get; set; }


    public void ConnectUpperCell (Cell upperCell) {
        UpperCell = upperCell;
        upperCell.LowerCell = this;
        upperCell.inMaze = true;
    }


    public void ConnectLowerCell (Cell lowerCell) {
        LowerCell = lowerCell;
        lowerCell.UpperCell = this;
        lowerCell.inMaze = true;
    }


    public void ConnectRightCell (Cell rightCell) {
        RightCell = rightCell;
        rightCell.LeftCell = this;
        rightCell.inMaze = true;
    }


    public void ConnectLeftCell (Cell leftCell) {
        LeftCell = leftCell;
        leftCell.RightCell = this;
        leftCell.inMaze = true;
    }



    public enum Side {

        Top,
        Bottom,
        Left,
        Right

    }

}