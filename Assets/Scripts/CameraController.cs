using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 8f;
    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position,desiredPosition, smoothSpeed* Time.deltaTime);
        transform.position = smoothPosition;
    }
}
