using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpDelay;
    [SerializeField] private Transform jumpTargetLeft;
    [SerializeField] private Transform jumpTargetRight;

    private Rigidbody2D myRigidbody;

    private int screenWidth = Screen.width;
    private Coroutine jumpCoroutine = null;

    public static event System.Action OnJump;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        screenWidth = Screen.currentResolution.width;
    }

    private void Jump(bool directionRight)
    {
        if (jumpCoroutine == null)
        {
            jumpCoroutine = StartCoroutine(JumpCoroutine(directionRight));
            OnJump?.Invoke();
        }
    }

    private IEnumerator JumpCoroutine(bool directionRight)
    {
        myRigidbody.simulated = false;

        Vector2 startPosition = transform.position;
        Vector2 targetPosition = directionRight ? jumpTargetRight.position : jumpTargetLeft.position;

        float timeElapsed = 0;
        float t;

        while (timeElapsed < jumpTime)
        {
            t = timeElapsed / jumpTime;
            transform.position = new Vector2(Mathf.Lerp(startPosition.x, targetPosition.x, t * t),
                Mathf.Lerp(startPosition.y, targetPosition.y, t));   // Parabolic movement
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        myRigidbody.simulated = true;
        yield return new WaitForSeconds(jumpDelay);
        jumpCoroutine = null;
    }

    private void Update()
    {
        if(Input.touchSupported)
        {
            HandleTouch();
        }
        else
        {
            HandleMouseClick();
        }
    }

    private void HandleTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > screenWidth / 2)
            {
                Jump(true);
            }
            else
            {
                Jump(false);
            }
        }
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > screenWidth / 2)
            {
                Jump(true);
            }
            else
            {
                Jump(false);
            }
        }
    }
}
