using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-10000)]
public class Inputs : UnitySingleton<Inputs>
{
    public static UnityEvent<Vector2> OnTurn = new();
    public static UnityEvent OnPause = new();
    InputActions actions;
    
    private void Start()
    {
        actions = new InputActions();
        actions.Enable();
        actions.Play.Pause.performed += _ => OnPause.Invoke();
    } 
  
    private void FixedUpdate()
    {
        var input = actions.Play.Turn.ReadValue<Vector2>();
        OnTurn.Invoke( input );
    }
}
