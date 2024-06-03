using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FlyCam : UnitySingleton<FlyCam>
{
    public Transform target;
    
    [FormerlySerializedAs("speed")] public float moveLerpSpeed = 1.0f;
    [FormerlySerializedAs("rotSpeed")] public float rotLerpSpeed = 1.0f;
    [Header("Field of View")]
    public Vector2 fovSpeeds = new(10, 20);
    public Vector2 fovRange = new(60, 100);
    public float fowSmoothSpeed = 1.0f;
    
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
        float currentSpeed = Vector3.Distance(lastPos, transform.position) / Time.deltaTime;
        
        transform.position = Vector3.Lerp(lastPos, targetPos, moveLerpSpeed * Time.deltaTime);
  
        float fovPower = Mathf.InverseLerp(fovSpeeds.x, fovSpeeds.y, currentSpeed);
        float fov = Mathf.Lerp(fovRange.x, fovRange.y, fovPower);
        cam.fieldOfView = Mathf.Lerp( cam.fieldOfView, fov, fowSmoothSpeed * Time.deltaTime );
        
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRot, Time.deltaTime * rotLerpSpeed);
    }
}
