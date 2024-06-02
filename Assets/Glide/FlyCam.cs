using System;
using UnityEngine;

public class FlyCam : MonoBehaviour
{
    Transform _target;

    public Transform target
    {
        get { return _target; }
        set
        {
            _target = value;
            transform.position = _target.position;
        }
    }

    public float speed = 1.0f;
    public float rotSpeed = 1.0f;
    
    public static FlyCam instance;
    
    private void Awake()
    {
        instance = this;
    }
    

    private void LateUpdate()
    {
        if (target == null) return;
        
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(transform.rotation,target.rotation, Time.deltaTime * rotSpeed);
    }
}
