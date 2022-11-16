using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [SerializeField][Min(1)] private float timeLeft;
    [SerializeField][Min(0)] private float timeGain;

    private int score;
    private int points;

    [HideInInspector]
    public static GameplayManager SharedInstance { get; private set; }

    protected virtual void Awake()
    {
        if (SharedInstance != null && SharedInstance != this)
        {
            Debug.LogWarning($"There are more than one instance of singleton: {name}");
            Destroy(gameObject);
        }
        else
        {
            SharedInstance = this;
        }
    }

    private void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime;
        if(timeLeft < 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnEnable()
    {
        Movement.OnJump += AugmentScoreAndTime;
    }

    private void OnDisable()
    {
        Movement.OnJump -= AugmentScoreAndTime;
    }

    private void AugmentScoreAndTime()
    {
        score++;
        timeLeft += timeGain;
    }

    public void AddPoint()
    {
        points++;
    }

    public void AddTime(float amount)
    {
        if(amount > 0)
        {
            timeLeft += amount;
        }
        else
        {
            Debug.LogWarning("Time add amount needs to be positive!");
        }
    }
}
