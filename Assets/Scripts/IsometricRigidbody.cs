using UnityEngine;

public class IsometricRigidbody : MonoBehaviour
{
    Vector3 velocity;
    Vector3 isometricPosition;

    void Start()
    {
        isometricPosition = Vector3.zero;
        velocity = Vector3.zero;

    }

    
    void Update()
    {
        IsometricMovement();

    }


    void IsometricMovement()
    {
        // Apply gravity
        velocity += Physics.gravity * Time.deltaTime;

        // Add velccity to position
        isometricPosition += velocity * Time.deltaTime;

        transform.position = ConvertToIsometric(isometricPosition);

    }

    public Vector2 ConvertToIsometric(Vector3 cartesianPos)
    {
        // The isometric X is calculated from the horizontal (X) and depth/vertical (Z)
        float isoX = (cartesianPos.x - cartesianPos.z) * 0.5f;

        // The isometric Y is calculated from the horizontal (X), depth (Z), and height (Y)
        float isoY = (cartesianPos.x + cartesianPos.z) * 0.25f - cartesianPos.y;

        return new Vector2(isoX, isoY);
    }

    public void AddForce(Vector3 force, bool velocityChange = false)
    {
        if (velocityChange)
            velocity = force;
        else
            velocity += force;
    }


}
