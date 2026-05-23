using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputSystem playerInputSys; //input system reference

    public Rigidbody2D rb;
    public Animator anim;
    public float normalSpeed = 1f;
    public float moveSpeed = 1f;
    public Transform playerVisuals;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float gravity = 25f;
    private bool Freeze = false;
    private bool slam = false;

    // Internal Fake 3D Z-Axis Variables
    private float verticalPosition = 0f; // Fake Z height
    private float verticalVelocity = 0f; // Fake Z velocity
    private bool isGrounded;

    [Header("Boosting")]
    private bool boosting = false;
    [SerializeField] private float BoostTime = 1f;
    [SerializeField] private float CurrentBoostTime = 1f;
    public float boostSpeed = 1.5f;

    [Header("other colliders")]
    [SerializeField] private PolygonCollider2D enemTrigger;
    public List <GameObject> possibleGoats = new List<GameObject>();
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
        isGrounded = true;
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
        possibleGoats[0].transform.SetParent(transform, true);
        possibleGoats[0].GetComponent<GoatMove>().Ride(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 playerInput = playerInputSys.Player.Move.ReadValue<Vector2>(); //reads the player's input from the input system and turns it into a vector2
        playerInput.x = -playerInput.x;
        //rb.linearVelocity = new Vector2(playerInput.x * moveSpeed, rb.linearVelocity.y);
        
        Vector2 isoOffset = new Vector2(-1f, 0.5f);

        Vector2 moveDir = new Vector2(playerInput.x, boostSpeed);
        if(!Freeze)
        {
            rb.linearVelocity = new Vector2(moveDir.x - moveDir.y, moveDir.x + moveDir.y) * isoOffset * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }


        if (playerInputSys.Player.Sprint.WasPressedThisFrame())
        {
            //Boost();
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

        if (playerInputSys.Player.Jump.WasPressedThisFrame())
        {
            if (isGrounded)
            {
                //Jump();
                isGrounded = false;
                if (possibleGoats.Count > 0)
                {
                    print("jump");
                    possibleGoats[0].GetComponent<GoatMove>().Kick();
                }
            }
            else
            {
                    print("slam");
                    slam = true;
                
            }
        }

        if (!isGrounded || verticalVelocity > 0)
        {
            if (!Freeze)
            {
                if (!slam)
                {
                    verticalVelocity -= gravity * Time.fixedDeltaTime;
                    verticalPosition += verticalVelocity * Time.fixedDeltaTime;
                }
                else
                {
                    verticalVelocity -= 2 * gravity * Time.fixedDeltaTime;
                    verticalPosition += verticalVelocity * Time.fixedDeltaTime;
                }

                // Land detection
                if (verticalPosition <= 0 && possibleGoats.Count > 0)
                {
                    verticalPosition = 0;
                    verticalVelocity = 0;
                    isGrounded = true;
                    enemTrigger.enabled = true;
                    possibleGoats[0].GetComponent<GoatMove>().Ride(gameObject);
                    slam = false;
                }
                else if (verticalPosition <= -0.5f)
                {
                    Freeze = true;
                }
            }
        }

        // 5. Apply the visual height offset to the sprite child object
        Vector3 localPos = playerVisuals.localPosition;
        localPos.y = verticalPosition;
        playerVisuals.localPosition = localPos;
        //UpdateAnims();

    }

    void UpdateAnims()
    {
        anim.SetBool("isGrounded", isGrounded);
        //anim.SetBool("isFalling", isFalling);
    }
    public void Boost()
    {
        if (!boosting)
        {
            boosting = true;
            CurrentBoostTime = BoostTime;
        }
    }
    public void Jump()
    {
        verticalVelocity = jumpForce;
        isGrounded = false;
        enemTrigger.enabled = false;
    }
}
