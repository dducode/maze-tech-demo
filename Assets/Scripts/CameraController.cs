using UnityEngine;



public class CameraController : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float sensitive = 5;

    private float m_xRotation;
    private float m_yRotation;


    private void Start () {
        m_xRotation = transform.eulerAngles.x;
        m_yRotation = transform.eulerAngles.y;
    }


    public void Zoom (float zoom) {
        transform.position += transform.forward * zoom;
    }


    public void MoveCamera (Vector3 v) {
        v *= speed;
        transform.position += transform.right * v.x + transform.up * v.y + transform.forward * v.z;
    }


    public void RotateCamera (Vector2 rotation) {
        rotation *= sensitive;
        m_xRotation -= rotation.x;
        m_yRotation += rotation.y;
        transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0);
    }

}