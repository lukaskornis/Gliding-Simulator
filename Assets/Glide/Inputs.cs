using System;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10000)]
public class Inputs : MonoBehaviour
{
    public static UnityEvent<Vector2> OnTurn = new();
    Actions actions;
    
    private void Start()
    {
        actions = new Actions();
        actions.Enable();
    } 
  
    private void FixedUpdate()
    {
        var input = actions.Play.Turn.ReadValue<Vector2>();
        OnTurn.Invoke( input );
    }
}
