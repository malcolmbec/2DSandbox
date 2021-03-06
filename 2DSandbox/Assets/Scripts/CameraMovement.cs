﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float minHeight = 0f;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);

    void Update()
    {
        // follow the player with offset
        float new_x = player.position.x + offset.x;
        float new_y = player.position.y + offset.y;
        // set minimum height for the camera
        if (new_y < minHeight)
        {
            new_y = minHeight;
        }
        transform.position = new Vector3(new_x, new_y, offset.z);
    }
}
