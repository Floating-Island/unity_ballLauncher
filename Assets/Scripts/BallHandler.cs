using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float detachDelay = 0.1F;
    [SerializeField] private float respawnDelay = 0.5F;
    
    private Rigidbody2D currentBallRigidBody;
    private SpringJoint2D currentBallSpringJoint;
    private Camera mainCamera;
    private bool isDragging;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        SpawnNewBall();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidBody == null)
        {
            return;
        }

        if (Touch.activeTouches.Count > 0)
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
        Vector2 touchPositionsSum = SumTouchPositions();

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPositionsSum);

        currentBallRigidBody.position = worldPosition;

        Debug.Log(worldPosition);
    }

    private static Vector2 SumTouchPositions()
    {
        Vector2 touchPositionsSum = new Vector2();
        foreach (Touch touch in Touch.activeTouches)
        {
            touchPositionsSum += touch.screenPosition;
        }

        touchPositionsSum /= Touch.activeTouches.Count;
        return touchPositionsSum;
    }

    private void SetBallRigidBodyAsKinematic()
    {
        currentBallRigidBody.bodyType = RigidbodyType2D.Kinematic;
    }

    private void SetIsDragging()
    {
        isDragging = true;
    }

    private void LaunchBall()
    {
        Invoke(nameof(DetachBall), detachDelay);
        currentBallRigidBody = null;
    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;

        Invoke(nameof(SpawnNewBall), respawnDelay);
    }

    private void ResetIsDragging()
    {
        isDragging = false;
    }

    private void SpawnNewBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);

        currentBallRigidBody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();

        currentBallSpringJoint.connectedBody = pivot;
    }
}
