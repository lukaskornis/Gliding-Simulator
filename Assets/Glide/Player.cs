using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        var glider = GetComponent<Glider>();
        Inputs.OnTurn.AddListener(glider.Turn);
    }
}
