using UnityEngine;



public class InputHandler : MonoBehaviour {

    [SerializeField]
    private CameraController cameraController;


    private void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetMouseButton(1))
            TransformCamera();

        if (Input.GetMouseButton(2)) {
            cameraController.MoveCamera(new Vector3(
                -Input.GetAxis("Mouse X") * Time.deltaTime,
                -Input.GetAxis("Mouse Y") * Time.deltaTime)
            );
        }

        cameraController.Zoom(Input.mouseScrollDelta.y);
    }


    private void TransformCamera () {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime;
        float y = Input.GetAxis("Forward") * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * Time.deltaTime;
        cameraController.MoveCamera(new Vector3(x, y, z));
        cameraController.RotateCamera(new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")));
    }

}