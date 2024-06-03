using UnityEngine;

/// <summary>
/// Visuals including glider joints, trails and particle effects, death animation
/// </summary>
public class GliderVisuals : MonoBehaviour
{
    public Transform visuals;
    public float leanAngle = 20;
    public float leanSpeed = 1;
    private Glider glider;
    public GameObject deathParticle;
    
    private void Start()
    {
        glider = GetComponent<Glider>();
    }
    
    private void Update()
    {
        Quaternion targetAngle = Quaternion.Euler(0, 0, -glider.turnDir.x * leanAngle);
        visuals.transform.localRotation = Quaternion.Lerp(visuals.transform.localRotation, targetAngle, Time.deltaTime * leanSpeed);
    }
    
    public void Die()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
        Destroy( gameObject );
    }
}
