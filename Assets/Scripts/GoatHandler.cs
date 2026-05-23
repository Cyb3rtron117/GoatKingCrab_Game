using UnityEngine;

public class GoatHandler : MonoBehaviour
{
    IsometricRigidbody isoRigidbody;

    float moveSpeed = 2f;

    void Awake()
    {
        isoRigidbody = GetComponent<IsometricRigidbody>();
    }

    void Update()
    {

        isoRigidbody.AddForce(new Vector3(moveSpeed, 0, 0), true);
        
    }
}
