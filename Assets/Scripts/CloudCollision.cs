using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(0);
    }
}
