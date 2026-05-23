using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputSystem playerInputSys; //input system reference

    public Rigidbody2D rb;
    public Animator anim;
    public float normalSpeed = 1f;
    public float moveSpeed = 1f;
    
    [Header("Jumping")]
    public float jumpforce = 1f;
    public float lowjumpMultiplier = 2f;
    public float fallMultiplier = 3f;
    public bool isGrounded = true;
    public bool isFalling = false;

    private float coyoteTime = 0.1f;
    [SerializeField] private float coyoteTimeCounter = 0.1f;
    public float rayDist = 1f;
    public float rayOffset = 0.1f;

    [Header("Boosting")]
    private bool boosting = false;
    [SerializeField] private float BoostTime = 1f;
    [SerializeField] private float CurrentBoostTime = 1f;
    public float boostSpeed = 1.5f;

    [Header("Audio")]
    FootstepManager footsteps;

    void Awake()
    {
        playerInputSys = new PlayerInputSystem(); //initialising the input system

    }
    void OnEnable()
    {
        playerInputSys.Enable(); //needed for the input system
    }
    void OnDisable()
    {
        playerInputSys.Disable(); //needed for the input system
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footsteps = GetComponent<FootstepManager>();

        moveSpeed = normalSpeed;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        boostSpeed = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isMoving = true;       //Checks if the pplayer is moving (set to true since they are always running)
        footsteps.TriggerFootstep(isMoving);        //Calls the Footstep sounds

        Vector2 playerInput = playerInputSys.Player.Move.ReadValue<Vector2>(); //reads the player's input from the input system and turns it into a vector2
        playerInput.x = -playerInput.x;
        //rb.linearVelocity = new Vector2(playerInput.x * moveSpeed, rb.linearVelocity.y);
        
        Vector2 isoOffset = new Vector2(-1f, 0.5f);

        Vector2 moveDir = new Vector2(playerInput.x, boostSpeed);
        rb.linearVelocity = new Vector2(moveDir.x - moveDir.y, moveDir.x + moveDir.y) * isoOffset * moveSpeed;

        if (playerInputSys.Player.Sprint.WasPressedThisFrame())
        {
            Boost();
        }
        if(boosting)
        {
            if(CurrentBoostTime > 0f)
            {
                CurrentBoostTime -= Time.fixedDeltaTime;
                //moveSpeed = boostSpeed;
                boostSpeed = 2f;
            }
            else
            {
                boosting = false;
                //moveSpeed = normalSpeed;
                boostSpeed = 1f;
            }
        }
        

        //Jumping with coyote time
        /*
        Vector3 rayPos = new Vector3(transform.position.x, transform.position.y - rayOffset, transform.position.z);
        isGrounded = Physics2D.Raycast(rayPos, Vector2.down, rayDist, LayerMask.GetMask("Ground"));
        Debug.DrawRay(rayPos, Vector2.down * rayDist, Color.red);
        

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;

        }

        if (playerInputSys.Player.Jump.WasPressedThisFrame() && coyoteTimeCounter > 0f)
        {
            rb.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            coyoteTimeCounter = 0;
        }

        if (rb.linearVelocity.y < 0f) //falling
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            isFalling = true;
        }
        else if (rb.linearVelocity.y > 0f && !playerInputSys.Player.Jump.IsPressed()) //low jump
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowjumpMultiplier - 1) * Time.fixedDeltaTime;
        }
        if (rb.linearVelocity.y >= 0f)
        {
            isFalling = false;
        }
        */

        //UpdateAnims();
    }

    void UpdateAnims()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", isFalling);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isGrounded)
        {
            isGrounded = false;
        }
    }
    void Boost()
    {
        if (!boosting)
        {
            boosting = true;
            CurrentBoostTime = BoostTime;
        }
    }
}
