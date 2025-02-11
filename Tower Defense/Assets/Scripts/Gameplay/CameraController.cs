using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 _newPos;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _newPos.x += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _newPos.x -= movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _newPos.z += movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _newPos.z -= movementSpeed * Time.deltaTime;
        }
        transform.position = _newPos;
        _newPos.z = Mathf.Clamp(transform.position.z, 500, 700);
        _newPos.x = Mathf.Clamp(transform.position.x, 0, 200);
        
    }
}
