using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gets direction and intensity of nearby obstacles
/// other systems like scoring, camera shake and wind sounds use this system
/// </summary>
public class Proximeter : MonoBehaviour
{
    public static UnityEvent<float> onObstacleCountChanged = new(); 
    
    public int radius = 3;
    public int rayCount = 4;
    public float RayPercent => Mathf.InverseLerp( 0, rayCount, raysHit );

    public int raysHit;
    public LayerMask obstacleMask;

    private Vector3[] directions;
    
    private void Start()
    {
        GenerateDirectionsRing();
    }

    private void Update()
    {
        // ring of raycasts
        int lastRaysHit = raysHit;
        raysHit = 0;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 direction = transform.TransformDirection(directions[i]);
            if (Physics.Raycast(transform.position, direction, out var hit, radius,obstacleMask))
            {
                raysHit++;
            }
        }
        
        if (lastRaysHit != raysHit) onObstacleCountChanged.Invoke(RayPercent);
    }

    void GenerateDirectionsRing()
    {
        directions = new Vector3[rayCount];
        for (int i = 0; i < rayCount; i++)
        {
            float angle = (float)i / rayCount * 2 * Mathf.PI;
            directions[i] = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        }
    }

    
    private void OnDestroy()
    {
        onObstacleCountChanged.Invoke(0);
    }
}
