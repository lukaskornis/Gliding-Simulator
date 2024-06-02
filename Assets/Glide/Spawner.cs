using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float radius = 50;
    public int count = 10;
    public bool autoParent = true;


    private void Start()
    {
        Random.InitState(100);
        Spawn(); 
    }

    public void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos =transform.position +  Random.insideUnitSphere * radius;
            Quaternion rot = Random.rotation;
            int scale = Random.Range(5, 15);
            GameObject obj = Instantiate(prefab, transform.position + Random.insideUnitSphere * radius, rot);
            obj.transform.localScale = Vector3.one * scale;
            if ( autoParent ) obj.transform.parent = transform;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
