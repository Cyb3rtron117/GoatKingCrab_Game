using System.Linq;
using UnityEngine;

public class GoatDetect : MonoBehaviour
{
    private PlayerManager playerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = transform.GetComponentInParent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goat"))
        {
            if (!playerScript.possibleGoats.Contains(collision.gameObject))
            {
                playerScript.possibleGoats.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goat"))
        {
            if (playerScript.possibleGoats.Contains(collision.gameObject))
            {
                playerScript.possibleGoats.Remove(collision.gameObject);
            }
        }

    }
}
