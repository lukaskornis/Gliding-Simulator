using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float radius = 50;
    public int count = 10;
    public bool autoParent = true;
    public Vector2 scaleRange = new Vector2(1, 2);
    public Vector3 rotationRange = new Vector3(0, 360, 0);

    private void Start()
    {
        Random.InitState(100);
        GridScatter(); 
    }

    public void RandomScatter()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos =transform.position +  Random.insideUnitSphere * radius;
           SpawnAt( pos );
        }
    }

    public float stepSize = 10;
    private float offset = 3;
    
    
    public void GridScatter()
    {
        // xyz radius
        float step = stepSize;
        for (float x = -radius; x <= radius; x += step)
        {
            for (float y = -radius; y <= radius; y += step)
            {
                for (float z = -radius; z <= radius; z += step)
                {
                    var pos = new Vector3(x, y, z);
                    pos += Random.insideUnitSphere * offset;
                    SpawnAt( pos);
                }
            }
        }
    }

    void SpawnAt(Vector3 pos)
    {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Quaternion rot = Quaternion.Euler(Random.Range(-rotationRange.x,rotationRange.x), Random.Range(-rotationRange.y,rotationRange.y), Random.Range(-rotationRange.z,rotationRange.z));
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        GameObject obj = Instantiate(prefab, transform.position + Random.insideUnitSphere * radius, rot);
        obj.transform.localScale = Vector3.one * scale;
        if ( autoParent ) obj.transform.parent = transform;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
