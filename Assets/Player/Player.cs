using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wires all glider systems together
/// </summary>
public class Player : MonoBehaviour
{
    public static UnityEvent OnDie = new();

    public AnimationCurve cameraShakeBySpeed;
    public float shakeAngle = 3;

    public GameObject crashEffect;
    public float crashShakeAngle = 20;
    
    private Glider glider;
    private GliderVisuals visuals;
    
    private void Start()
    {
        glider = GetComponent<Glider>();
        visuals = GetComponent<GliderVisuals>();
        
        Inputs.OnTurn.AddListener(glider.Turn);
        FlyCam.Instance.target = transform;
    }

    private void Update()
    {
        CameraShake.Shake(cameraShakeBySpeed.Evaluate(glider.SpeedPercent) * shakeAngle);
    }


    private void OnCollisionEnter(Collision other)
    {
        var contact = other.GetContact(0);
        Instantiate( crashEffect, contact.point, Quaternion.LookRotation(contact.normal) );
        visuals.Die();
        OnDie.Invoke();
        CameraShake.Shake(crashShakeAngle);
    }
    
}
