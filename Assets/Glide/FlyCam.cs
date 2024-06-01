using System;
using UnityEngine;

public class FlyCam : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    public float rotSpeed = 1.0f;

    private void Start()
    {   
        transform.position = target.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation, Time.deltaTime * rotSpeed);
    }
}
