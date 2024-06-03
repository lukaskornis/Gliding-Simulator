using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    public static UnityEvent<int> OnScoreChanged = new();
    public int score;
    float proximity;

    [SerializeField]AnimationCurve intervalByProximity;
    [Min(0.01f)]public Vector2 scoreSoundIntervalRange = Vector2.one;
    public float scoreSoundInterval;
    
    [SerializeField]private AudioClip scoreSound;
    
    private void Start()
    {
        Proximeter.onProximityChanged.AddListener( p => proximity = p );
        ScoreSoundRoutine();
    }

    private void Update()
    {
         score += (int)(proximity * 10);
         OnScoreChanged.Invoke(score);
         
         var t = intervalByProximity.Evaluate(proximity);
         scoreSoundInterval = Mathf.Lerp(scoreSoundIntervalRange.x, scoreSoundIntervalRange.y, t);
         print (proximity);
    }

    async void ScoreSoundRoutine()
    {
        while (true)
        {
            await new WaitForSeconds(scoreSoundInterval);
            if(proximity > 0)Audio.PlayClip(scoreSound);
        }
    }
}
