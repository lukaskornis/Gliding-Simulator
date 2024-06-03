using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gets direction and intensity of nearby obstacles
/// other systems like scoring, camera shake and wind sounds use this system
/// </summary>
public class Proximeter : UnitySingleton<Proximeter>
{
    public static UnityEvent<float> onProximityChanged = new(); 
    
    public int radius = 5;
    public int rayCount = 16;
    public float RayPercent => Mathf.InverseLerp( 0, rayCount, raysHit );

    public int raysHit;
    public float minDistance;
    public float totalProximity;
    public LayerMask obstacleMask;
    public Vector3 averageDirection;

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
        minDistance = radius;
        totalProximity = 0;
        averageDirection = Vector3.zero;
        
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 direction = transform.TransformDirection(directions[i]);
            Vector3 center = transform.position + direction * 1f;
            Vector3 half = new Vector3(0.5f,0.5f,2f);
            Quaternion orientation = Quaternion.LookRotation(direction);
            
            if (Physics.BoxCast(center, half,direction, out var hit,orientation,radius,obstacleMask))
            {
                float distance = Vector3.Distance(transform.position, hit.point);
                if (distance < minDistance) minDistance = distance;
                
                float distancePercent = distance / radius;
                totalProximity += distancePercent;
                raysHit++;

                averageDirection += directions[i] * distancePercent;
            }
        }
        
        totalProximity /= rayCount;
        if (lastRaysHit != raysHit) onProximityChanged.Invoke(totalProximity);
    }

    void GenerateDirectionsRing()
    {
        directions = new Vector3[rayCount];
        for (int i = 0; i < rayCount; i++)
        {
            float angle = (float)i / rayCount * 2 * Mathf.PI;
            directions[i] = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle),0);
        }
    }

    
    private void OnDestroy()
    {
        onProximityChanged.Invoke(0);
    }
}
