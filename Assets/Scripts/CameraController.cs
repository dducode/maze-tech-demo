﻿using UnityEngine;



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


    public void TransformCamera () {
        MoveCamera();
        RotateCamera();
    }


    public void Zoom () {
        transform.position += transform.forward * Input.mouseScrollDelta.y;
    }


    private void MoveCamera () {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float y = Input.GetAxis("Forward") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position += transform.right * x + transform.up * y + transform.forward * z;
    }


    private void RotateCamera () {
        m_xRotation -= Input.GetAxis("Mouse Y") * sensitive;
        m_yRotation += Input.GetAxis("Mouse X") * sensitive;
        transform.rotation = Quaternion.Euler(m_xRotation, m_yRotation, 0);
    }

}