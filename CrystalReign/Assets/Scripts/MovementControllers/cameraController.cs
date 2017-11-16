﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public bool inverted = false;
    public float minAngle = -45;
    public float maxAngle = 45;
    Vector3 currentRotation;
    float rotate = 5;
    float firstY;
    bool isC = false;

    float distance;
    public LayerMask layerMask;

    Transform camera = null;


    void Start()
    {

        if (transform.childCount != 1)
        {
            throw new System.Exception("Missing camera");
        }
        camera = transform.GetChild(0);
        currentRotation = transform.rotation.eulerAngles;
        firstY = currentRotation.x;
        distance = Vector3.Distance(transform.position, camera.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            isC = !isC;
        if (!isC)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (camera == null) return;
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = (inverted ? 1 : -1) * Input.GetAxis("Mouse Y");
            if (mouseY * rotate + currentRotation.x > firstY + maxAngle || mouseY * rotate + currentRotation.x < firstY + minAngle)
            {
                mouseY = 0;
            }
            Vector3 rotateValue = new Vector3(mouseY, mouseX, 0);
            currentRotation += rotateValue * rotate;
            Quaternion rotation = Quaternion.Euler(currentRotation);
            transform.rotation = rotation;

            RaycastHit hit;
            float currentDistance = distance;
            if (Physics.Raycast(transform.position, camera.position - transform.position, out hit, distance, layerMask))
            {
                currentDistance = Vector3.Distance(transform.position, hit.point);
            }
            Vector3 camLocPos = camera.localPosition;
            camLocPos.z = -currentDistance;
            camera.localPosition = camLocPos;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
