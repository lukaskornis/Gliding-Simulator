using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wires all glider systems together
/// </summary>
public class Player : MonoBehaviour
{
    public static UnityEvent OnDie = new();
    
    private Glider glider;
    private GliderVisuals visuals;
    
    private void Start()
    {
        glider = GetComponent<Glider>();
        visuals = GetComponent<GliderVisuals>();
        
        Inputs.OnTurn.AddListener(glider.Turn);
        FlyCam.Instance.target = transform;
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        visuals.Die();
        OnDie.Invoke();
    }
}
