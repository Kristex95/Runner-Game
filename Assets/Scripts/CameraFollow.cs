using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(0, target.position.y, target.position.z) + offset;
        transform.position = desiredPosition;
    }
}
