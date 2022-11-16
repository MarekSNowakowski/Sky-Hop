using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameplayManager.SharedInstance.AddPoint();
        Destroy(gameObject);
    }
}
