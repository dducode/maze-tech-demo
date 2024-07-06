using UnityEngine;



public class InputHandler : MonoBehaviour {

    [SerializeField]
    private CameraController cameraController;


    private void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetMouseButton(1))
            cameraController.TransformCamera();

        cameraController.Zoom();
    }

}