﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    public float speed = 3f;
    public float zoomSpeed = 10f;

    public List<Limits> limits;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
        newPos.x += Time.deltaTime * speed * Input.GetAxis("Horizontal");
        newPos.x = Mathf.Clamp(newPos.x, limits[0].minLimit, limits[0].maxLimit);

        var fieldOfView = cam.fieldOfView + (Time.deltaTime * zoomSpeed * Input.mouseScrollDelta.y);
        fieldOfView = Mathf.Clamp(fieldOfView, limits[1].minLimit, limits[1].maxLimit);
        cam.fieldOfView = fieldOfView;

        newPos.z += Time.deltaTime * speed * Input.GetAxis("Vertical");
        newPos.z = Mathf.Clamp(newPos.z, limits[2].minLimit, limits[2].maxLimit);
        transform.position = newPos;
    }
}

[System.Serializable]
public struct Limits 
{
    public float minLimit;
    public float maxLimit;
}