using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    /*[SerializeField] float _mouseSensitivity = 100f;
    [SerializeField] Transform _playerBody;

    float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        float mouseX = Input.GetAxis("Horizontal") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Vertical") * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        _playerBody.Rotate(Vector3.up * mouseX);
    }*/
}
