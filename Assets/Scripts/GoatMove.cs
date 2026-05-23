using UnityEngine;
using UnityEngine.InputSystem;

public class GoatMove : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    public bool isRidden = false;
    [SerializeField] private float maxRidetime = 10f;
    [SerializeField] private float currentRidetime = 1f;
    public GameObject player;

    [SerializeField] private bool startRidden = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRidetime = maxRidetime;
        if(startRidden)
        {
            isRidden = true;
            Ride(player);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isRidden && !startRidden)
        {
            if(rb.bodyType == RigidbodyType2D.Kinematic)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            Vector2 isoOffset = new Vector2(-1f, 0.5f);
            Vector2 moveDir =  new Vector2(0, moveSpeed);
            rb.linearVelocity = new Vector2(moveDir.x - moveDir.y, moveDir.x + moveDir.y) * isoOffset;
        }
        else if (isRidden)
        {
            if(currentRidetime > 0f)
            {
                currentRidetime -= Time.fixedDeltaTime;
            }
            else
            {
                Kick();
            }
        }
        
    }
    public void Ride( GameObject playerObj)
    {
        isRidden = true;
        player = playerObj;
        
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        transform.SetParent(player.transform, true);
        transform.localPosition = new Vector2 (0, -0.5f);
        rb.linearVelocity = Vector2.zero;

        currentRidetime = maxRidetime;
        
        player.GetComponent<PlayerManager>().Boost();
    }
    public void Die()
    {
        Kick();
        gameObject.SetActive(false);
    }

    public void Kick()
    {
        
        transform.SetParent(null, true);
        
        
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        isRidden = false;
        startRidden = false;

        player.GetComponent<PlayerManager>().Jump();
        player.GetComponent<PlayerManager>().possibleGoats.Remove(gameObject);
        player = null;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Crab"))
        {
            Die();
        }
    }
}
