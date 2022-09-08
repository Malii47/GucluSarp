using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -5);
    }
}