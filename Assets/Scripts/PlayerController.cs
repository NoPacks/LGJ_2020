using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 25.0f;
    [SerializeField] bool hasDoubleJump = true;
    [SerializeField] float startDashTime = 0.5f;
    [SerializeField] float dashSpeed = 50.0f;
    [SerializeField] float startDashCoolDown = 2.0f;


    float dashTime;
    float dashCoolDown;
    float dashDirection;
    bool canDoubleJump = true;

    // Cached componenet references
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        dashCoolDown = startDashCoolDown;
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Dash();
    }


    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); // Value between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        if(controlThrow < 0)
        {
            dashDirection = -1;
        }
        else if(controlThrow > 0)
        {
            dashDirection = 1;
        }


        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        // Add animation control here, use playerHorizontalSpeed as trigger
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if (canDoubleJump && hasDoubleJump)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                    myRigidbody.velocity += jumpVelocityToAdd;
                    canDoubleJump = false;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            canDoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
                myRigidbody.velocity += jumpVelocityToAdd;
            }
        }
    }

    private void Dash()
    {
        if(dashCoolDown <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                if (dashTime <= 0)
                {
                    dashDirection = 0;
                    myRigidbody.velocity = Vector2.zero;
                    dashCoolDown = startDashCoolDown;
                    dashTime = startDashTime;
                }
                else
                {
                    dashTime -= Time.deltaTime;
                    myRigidbody.velocity = Vector2.right * dashDirection * dashSpeed;
                } 
            }
        }
        else
        {
            dashCoolDown -= Time.deltaTime;
        }
    }
}
