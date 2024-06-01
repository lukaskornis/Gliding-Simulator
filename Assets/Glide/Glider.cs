using UnityEngine;

public class Glider : MonoBehaviour
{
    public float moveSpeed = 10;
    public float maxMoveSpeed = 30;
    public float turnSpeed = 100; 
    public float acceleration = 1;
    public float drag;
    public float minGlideSpeed = 5;
    float xRot;
    float yRot;
    public Vector2 turnDir;
    
    public void Turn( Vector2 dir )
    {
        turnDir = dir;
    }
    
    private void Update()
    {
        xRot += turnDir.x * turnSpeed * Time.deltaTime; 
        yRot += turnDir.y * turnSpeed * Time.deltaTime;
        yRot = Mathf.Clamp(yRot, -90, 90);
        transform.eulerAngles = new Vector3(yRot, xRot);

        float xDot = Vector3.Dot(Vector3.down, transform.forward);
        moveSpeed += xDot * acceleration *  Time.deltaTime;
        moveSpeed *= 1 - drag * Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed, 0, maxMoveSpeed);

        if (moveSpeed < minGlideSpeed)
        {
            yRot += 90 * Time.deltaTime;
        }
        
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
