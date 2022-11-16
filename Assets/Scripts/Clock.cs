using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField][Range(0.01f, 5f)] float timeAddAmount = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameplayManager.SharedInstance.AddTime(timeAddAmount);
        Destroy(gameObject);
    }
}
