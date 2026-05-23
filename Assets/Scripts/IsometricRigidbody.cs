using UnityEngine;

public class IsometricRigidbody : MonoBehaviour
{
    Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    void Update()
    {
        // Apply gravity
        velocity += Physics.gravity * Time.deltaTime;

        // Move the object
        transform.position += velocity * Time.deltaTime;
    }


    void IsometricMovement()
    {
        Vector2 isometricVelocity = new Vector2(velocity.x, velocity.z);

    }
}
