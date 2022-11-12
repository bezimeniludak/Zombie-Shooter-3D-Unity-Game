using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 200f;
    [SerializeField] private Transform _playerBody;

    private float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameState != GameManager.GameState.running)
            return;

        float mouseInputX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseInputY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseInputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseInputX); 
    }
}
