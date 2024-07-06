using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class UserInterface : UIBehaviour {

    [SerializeField]
    private Maze maze;

    [SerializeField]
    private TMP_InputField xSize;

    [SerializeField]
    private Slider xSlider;

    [SerializeField]
    private TMP_InputField ySize;

    [SerializeField]
    private Slider ySlider;

    [SerializeField]
    private TMP_InputField seed;

    [SerializeField]
    private Toggle isOptimized;

    public Vector2Int InputSize => new(int.Parse(xSize.text), int.Parse(ySize.text));


    protected override void Awake () {
        xSize.text = $"{maze.Size.x:N0}";
        xSlider.value = maze.Size.x;

        xSize.onEndEdit.AddListener(x => {
            float value = Math.Clamp(int.Parse(x), xSlider.minValue, xSlider.maxValue);
            xSize.text = $"{value:N0}";
            xSlider.value = value;
        });
        xSlider.onValueChanged.AddListener(x => xSize.text = $"{x:N0}");

        ySize.text = $"{maze.Size.y:N0}";
        ySlider.value = maze.Size.y;

        ySize.onEndEdit.AddListener(y => {
            float value = Math.Clamp(int.Parse(y), ySlider.minValue, ySlider.maxValue);
            ySize.text = $"{value:N0}";
            ySlider.value = value;
        });
        ySlider.onValueChanged.AddListener(y => ySize.text = $"{y:N0}");

        seed.text = $"{maze.Seed:N0}";
        isOptimized.isOn = maze.IsOptimized;
    }


    public void CreateNewMaze () {
        maze.CreateNewMaze(InputSize, int.Parse(seed.text), isOptimized.isOn);
    }

}