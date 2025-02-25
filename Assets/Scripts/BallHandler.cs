using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.IsPressed())
        {
            Vector2 TouchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 WorldPosition = mainCamera.ScreenToWorldPoint(TouchPosition);

            Debug.Log(WorldPosition);
        }
    }
}
