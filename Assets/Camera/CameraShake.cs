using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : UnitySingleton<CameraShake>
{
    public float frequency = 0.5f;
    public float amplitude = 0.5f;
    public float decay = 0.5f;
    public Vector3 angleOffset = Vector3.one;
    
    // Shake rotation with perlin noise
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            amplitude = 3;
        }
        Shake();
    }

    public void Shake()
    {
        Vector3 offset = angleOffset;
        offset.x *= Mathf.PerlinNoise1D( Time.time * frequency ) - 0.5f;
        offset.y *= Mathf.PerlinNoise1D( Time.time * frequency + 1 ) - 0.5f;
        offset.z *= Mathf.PerlinNoise1D( Time.time * frequency + 2 ) - 0.5f;
        
        amplitude -= decay * Time.deltaTime;
        if (amplitude <= 0) amplitude = 0;
        offset *= amplitude;
     
        transform.localRotation = Quaternion.Euler( offset );
    }
    
    public static void Shake(float amplitude)
    {
        Instance.amplitude = Mathf.Max( Instance.amplitude, amplitude );
    }
}
