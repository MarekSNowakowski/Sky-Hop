using UnityEngine;

public class Movement : MonoBehaviour
{
    private int screenWidth = Screen.width;

    private void JumpLeft()
    {
        Debug.Log("Left");
    }

    private void JumpRight()
    {
        Debug.Log("Right");
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
                JumpRight();
            }
            else
            {
                JumpLeft();
            }
        }
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > screenWidth / 2)
            {
                JumpRight();
            }
            else
            {
                JumpLeft();
            }
        }
    }
}
