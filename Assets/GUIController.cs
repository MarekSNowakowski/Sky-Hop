using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreValueText;
    [SerializeField] TextMeshProUGUI pointsValueText;
    
    public void UpdateScore(int score)
    {
        scoreValueText.text = score.ToString();
    }

    public void UpdatePoints(int points)
    {
        pointsValueText.text = points.ToString();
    }
}
