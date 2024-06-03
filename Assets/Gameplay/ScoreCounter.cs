using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int score;
    float percent;

    [Min(0.01f)]public float scoreSoundInterval = 0.1f;
    [SerializeField]private AudioClip scoreSound;
    
    private void Start()
    {
        Proximeter.onObstacleCountChanged.AddListener( OnObstacleCountChanged );
        ScoreSoundRoutine();
    }

    private void Update()
    {
         score += (int)(percent * 10);
    }


    private void OnObstacleCountChanged(float percent)
    {
        this.percent = percent;
    }

    async void ScoreSoundRoutine()
    {
        while (true)
        {
            await new WaitForSeconds(scoreSoundInterval);
            if(percent > 0)Audio.PlayClip(scoreSound);
        }
    }
}
