using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.01f;
    
    private void Update()
    {
        if (Time.frameCount % 60 == 0)
        {
            speed = -speed;
        }
        
        transform.position += Vector3.left * speed;
    }
}
