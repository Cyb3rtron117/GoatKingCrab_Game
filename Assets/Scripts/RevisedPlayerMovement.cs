using UnityEngine;

public class RevisedPlayerMovement : MonoBehaviour
{
    PlayerInputSystem playerInputSystem;

    IsometricRigidbody rb;

    bool isGrounded = false;

    float groundDelay = 0f;

    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Enable();

        rb = GetComponent<IsometricRigidbody>();
    }

    private void Start()
    {
        rb.AddForce(new Vector3(10, 7, 0), true);
    }

    private void Update()
    {
        Vector3 targetVelccity = rb.Velocity;
        targetVelccity.z = playerInputSystem.Player.Move.ReadValue<Vector2>().x * -5f;

        Vector3 newVelocity = Vector3.Lerp(rb.Velocity, targetVelccity, Time.deltaTime * 10f);

        // Set position
        rb.AddForce(newVelocity, true);

        if (!isGrounded && rb.IsGrounded)
        {
            isGrounded = true;
            LandCheck();
        }
        else if (isGrounded && !rb.IsGrounded)
        {
            isGrounded = false;
        }

        if (playerInputSystem.Player.Move.ReadValue<Vector2>().y < -0.5f && groundDelay <=0f)
        {
            rb.AddForce(new Vector3(0, -8, 0));

        }

        if (isGrounded)
            LandCheck();
        else
            groundDelay -= Time.deltaTime;

    }

    void CameraFollow()
    {
        Transform cameraTransform = Camera.main.transform;

        Vector3 targetPos = new Vector3(transform.position.x, cameraTransform.position.y, cameraTransform.position.z);
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPos, Time.deltaTime * 5f);
    }

    void LandCheck()
    {
        groundDelay = 0.2f;

        GameObject[] goats = FindAnyObjectByType<LevelGenerator>().GetGameObjects();

        // if close to a goat, bounce up
        foreach (GameObject goat in goats)
        {
            if (goat == null)
                continue;
            float distance = Vector3.Distance(transform.position, goat.transform.position);
            if (distance < 0.5f)
            {
                rb.AddForce(new Vector3(10, 7, 0), true);
                break;
            }
        }

    }

}
