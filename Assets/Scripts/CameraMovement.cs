using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float moveStep;
    [SerializeField]
    private float moveTime;

    private void OnEnable()
    {
        Movement.OnJump += MoveCamera;
    }

    private void OnDisable()
    {
        Movement.OnJump -= MoveCamera;
    }

    private void MoveCamera()
    {
        StartCoroutine(MoveCameraCoroutine());
    }

    private IEnumerator MoveCameraCoroutine()
    {
        float cameraPositionY = transform.position.y;
        float targetPositionY = transform.position.y + moveStep;

        float timeElapsed = 0;

        while (timeElapsed < moveTime)
        {
            transform.position = new Vector3(0, Mathf.Lerp(cameraPositionY, targetPositionY, timeElapsed / moveTime), transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(0, targetPositionY, transform.position.z);
    }
}
