using UnityEngine;

public class GoatHandler : MonoBehaviour
{
    IsometricRigidbody isoRigidbody;
    void Awake()
    {
        isoRigidbody = GetComponent<IsometricRigidbody>();
    }

    void Update()
    {

        isoRigidbody.AddForce(new Vector3(5, 0, 0), true);
        
    }
}
