using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform playerTransform;

    void Start()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        transform.position = new Vector3(playerTransform.position.x,playerTransform.position.y, transform.position.z);
    }
}
