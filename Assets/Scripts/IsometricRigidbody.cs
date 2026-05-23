using UnityEngine;

public class IsometricRigidbody : MonoBehaviour
{
    [Header("Isometric Rigidbody Stats")]
    [SerializeField] Vector3 velocity;
    public Vector3 Velocity { get { return velocity; } }
    [SerializeField] Vector3 isometricPosition = Vector3.zero;
    public Vector3 IsometricPosition { get { return isometricPosition; } }

    bool isGrounded = false;
    public bool IsGrounded { get { return isGrounded; } }

    [SerializeField] Transform shadow;

    void Start()
    {
        velocity = Vector3.zero;

    }

    
    void Update()
    {
        IsometricMovement();

    }


    void IsometricMovement()
    {
        // Apply gravity
        velocity += Physics.gravity * Time.deltaTime * 0.7f;

        // Add velccity to position
        isometricPosition += velocity * Time.deltaTime;
        if (isometricPosition.y < 0)
        {
            isometricPosition.y = 0;
            velocity.y = 0;
        }

        // Limit horizontal speed
        isometricPosition.z = Mathf.Clamp(isometricPosition.z, -4, 4);

        // Grounde check
        if (isometricPosition.y <= 0)
            isGrounded = true;
        else
            isGrounded = false;

        // Drag 
        if (isGrounded)
            velocity.x *= 0.8f;

        transform.position = ConvertToIsometric(isometricPosition);

        if (shadow == null)
            return;
        Vector3 shadowPos = isometricPosition;
        shadowPos.y = 0.01f;
        shadow.position = ConvertToIsometric(shadowPos);

    }

    public Vector2 ConvertToIsometric(Vector3 cartesianPos)
    {
        // The isometric X is calculated from the horizontal (X) and depth/vertical (Z)
        float isoX = (cartesianPos.x - cartesianPos.z) * Mathf.Cos(Mathf.Deg2Rad * 30);

        // The isometric Y is calculated from the horizontal (X), depth (Z), and height (Y)
        float isoY = (cartesianPos.x + cartesianPos.z) * Mathf.Sin(Mathf.Deg2Rad * 30) + cartesianPos.y;

        return new Vector2(isoX, isoY);

    }

    public void AddForce(Vector3 force, bool velocityChange = false)
    {
        if (velocityChange)
            velocity = force;
        else
            velocity += force;

    }
    public void SetPosition(Vector3 newPos)
    {
        isometricPosition = newPos;
    }


}
