using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D currentBallRigidBody;
    [SerializeField] private SpringJoint2D currentBallSpringJoint;
    [SerializeField] private float detachDuration = 0.2F;
    private Camera mainCamera;
    private bool isDragging;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidBody == null)
        {
            return;
        }

        if (Touchscreen.current.primaryTouch.press.IsPressed())
        {
            SetBallRigidBodyAsKinematic();
            SetIsDragging();
            SetBallLocation();
        }
        else
        {
            SetBallRigidBodyAsDynamic();
            if (isDragging)
            {
                ResetIsDragging();
                LaunchBall();
            }
        }
    }

    private void SetBallRigidBodyAsDynamic()
    {
        currentBallRigidBody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void SetBallLocation()
    {
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidBody.position = worldPosition;

        Debug.Log(worldPosition);
    }

    private void SetBallRigidBodyAsKinematic()
    {
        currentBallRigidBody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void SetIsDragging()
    {
        isDragging = true;
    }

    void LaunchBall()
    {
        Invoke(nameof(DetachBall), detachDuration);
        currentBallRigidBody = null;
    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }

    private void ResetIsDragging()
    {
        isDragging = false;
    }
}
