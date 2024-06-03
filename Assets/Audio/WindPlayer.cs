using System;
using UnityEngine;

/// <summary>
/// Wind sound and intensity controls. Based on speed and obstacles
/// </summary>
public class WindPlayer : MonoBehaviour
{
    public float nearObstacleShakeAngle = 5;
    
    private AudioSource windSource;
    [SerializeField] AnimationCurve volumeBySpeedCurve;
    Glider glider;
    
    private void Start()
    {
        windSource = GetComponent<AudioSource>();
        glider = FindObjectOfType<Glider>();
        
        Proximeter.onProximityChanged.AddListener( OnObstacleCountChanged );
    }

    private void Update()
    {
        float volume = volumeBySpeedCurve.Evaluate(glider.SpeedPercent);
        windSource.volume = volume;
    }


    private void OnObstacleCountChanged(float percent)
    {
        if (percent > 0)
        {
            CameraShake.Shake(percent * nearObstacleShakeAngle);
        }
    }
}
