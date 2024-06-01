using System;
using UnityEngine;
using UnityEngine.Events;

public class Inputs : MonoBehaviour
{
    public static UnityEvent<Vector2> OnTurn = new();
    
    private void Update()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnTurn.Invoke( input );
    }
}
