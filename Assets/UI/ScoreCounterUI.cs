using System;
using TMPro;
using UnityEngine;

/// <summary>
/// show score in screen space in the direction of a player
/// </summary>
public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreLabel;
    Proximeter proximeter;
    Glider glider;
    Canvas canvas;

    private void Start()
    {
        proximeter = Proximeter.Instance;
        glider = FindObjectOfType<Glider>();
        canvas = GetComponent<Canvas>();
        ScoreCounter.OnScoreChanged.AddListener(UpdateScore);
    }

    private void Update()
    {
        //var textPos =
    }

    private void UpdateScore(int score)
    {
        scoreLabel.text = score.ToString();
    }
}
