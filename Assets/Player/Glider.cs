using System;
using UnityEngine;

public class Glider : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;
    public float minMoveSpeed = 3;
    public float maxMoveSpeed = 20;
    public float SpeedPercent => Mathf.InverseLerp( 0, maxMoveSpeed, moveSpeed );
    
    public float downAcceleration = 10;
    public float upDeceleration = 10;
    public float drag = 1;
    
    [Header("Turning")]
    public float vTurnSpeed = 100;
    public float hTurnSpeed = 100;
    public float stallDiveSpeed = 50;
    
    [HideInInspector]public Vector2 turnDir;
    const float maxTurnAngle = 89.999f;
    float vAngle;
    float hAngle;
    
    
    public void Turn(Vector2 dir)
    {
        turnDir = dir;
    }

    private void Start()
    {
        vAngle = transform.eulerAngles.x;
        hAngle = transform.eulerAngles.y;
    }

    private void Update()
    {
        float glidePercent = Mathf.InverseLerp( 5, 10, moveSpeed );

        if (turnDir.y > 0) vAngle += turnDir.y * vTurnSpeed * Time.deltaTime;       // turn down
        else vAngle += turnDir.y * vTurnSpeed * glidePercent * Time.deltaTime;      // turn up
        vAngle += (1 -glidePercent) * stallDiveSpeed * Time.deltaTime;              // nose dive on low speed
        vAngle = Mathf.Clamp(vAngle, -maxTurnAngle, maxTurnAngle);         // no back flips
        
        hAngle += turnDir.x * hTurnSpeed * Time.deltaTime;
        hAngle = Mathf.Repeat( hAngle, 360 );
        
        float downPercent = Vector3.Dot( transform.forward, Vector3.down );
        if(downPercent > 0)moveSpeed += downPercent * downAcceleration * Time.deltaTime;
        else moveSpeed += downPercent * upDeceleration * Time.deltaTime;

        moveSpeed -= drag * Time.deltaTime;
        moveSpeed = Mathf.Clamp( moveSpeed, minMoveSpeed, maxMoveSpeed );
        

        transform.eulerAngles  = new Vector3( vAngle, hAngle, 0 );
        transform.position += transform.forward * moveSpeed * Time.deltaTime; 
    }
}
