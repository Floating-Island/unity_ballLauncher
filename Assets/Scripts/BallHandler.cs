using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.IsPressed())
        {
            Vector2 TouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Debug.Log(TouchPosition);
        }
    }
}
