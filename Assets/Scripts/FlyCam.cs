using System;
using UnityEngine;

public class FlyCam : UnitySingleton<FlyCam>
{
    public Transform target;
    
    public float speed = 1.0f;
    public float rotSpeed = 1.0f;
    [Header("Field of View")]
    public Vector2 fovSpeeds = new Vector2(10, 20);
    public Vector2 fovRange = new Vector2(60, 100);
    public float fowSmoothSpeed = 1.0f;
    public AnimationCurve fowCurve;
    
    private Vector3 lastPos;
    private Vector3 targetPos;
    private Quaternion targetRot;
    private Camera cam;
    
    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            targetPos = target.position;
            targetRot = target.rotation;
        }
        
        lastPos = transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        
        float moveSpeed = Vector3.Distance(lastPos, transform.position) / Time.deltaTime;
        float fovPower = Mathf.InverseLerp(fovSpeeds.x, fovSpeeds.y, moveSpeed);
        float fov = Mathf.Lerp(fovRange.x, fovRange.y, fovPower);
        cam.fieldOfView = Mathf.Lerp( cam.fieldOfView, fov, fowSmoothSpeed * Time.deltaTime );
        
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRot, Time.deltaTime * rotSpeed);
    }
}
