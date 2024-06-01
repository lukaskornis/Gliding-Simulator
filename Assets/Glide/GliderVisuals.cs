using UnityEngine;

public class GliderVisuals : MonoBehaviour
{
    public Transform visuals;
    public float leanAngle = 20;
    private Glider glider;
    
    private void Start()
    {
        glider = GetComponent<Glider>();
    }
    
    private void Update()
    {
        Quaternion targetAngle = Quaternion.Euler(0, 0, -glider.turnDir.x * leanAngle);
        visuals.transform.localRotation = Quaternion.Lerp(visuals.transform.localRotation, targetAngle, Time.deltaTime * 10);
    }
}
