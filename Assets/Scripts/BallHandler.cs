using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D currentBallRigidBody;
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
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            if (currentBallRigidBody)
            {
                currentBallRigidBody.position = worldPosition;
            }

            Debug.Log(worldPosition);
        }
    }
}
