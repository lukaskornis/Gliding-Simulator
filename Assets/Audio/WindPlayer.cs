using System;
using UnityEngine;

/// <summary>
/// Wind sound and intensity controls. Based on speed and obstacles
/// </summary>
public class WindPlayer : MonoBehaviour
{
    public float nearObstacleShakeAngle = 5;
    
    private void Start()
    {
        Proximeter.onObstacleCountChanged.AddListener( OnObstacleCountChanged );
    }

    
    private void OnObstacleCountChanged(float percent)
    {
        if (percent > 0)
        {
            CameraShake.Shake(percent * nearObstacleShakeAngle);
        }
    }
}
