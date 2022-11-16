using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreValueText;
    [SerializeField] TextMeshProUGUI pointsValueText;
    [SerializeField] Image timerFillerImage;
    [SerializeField] TextMeshProUGUI timeValueText;

    public void UpdateScore(int score)
    {
        scoreValueText.text = score.ToString();
    }

    public void UpdatePoints(int points)
    {
        pointsValueText.text = points.ToString();
    }

    public void UpdateTimer(float timerPercentage, float timeLeft)
    {
        timerFillerImage.fillAmount = timerPercentage;
        timeValueText.text = timeLeft.ToString("F1");
    }
}
