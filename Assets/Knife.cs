using UnityEngine;

public class Knife : MonoBehaviour
{   
    private void Update()
    {
        GetComponent<Transform>().Rotate(0,0,1);   
    }
}
      