using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkills
{
    doubleJump,
    dash
}

public class PlayerController : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 7.0f;
    [SerializeField] float startDashTime = 0.1f;
    [SerializeField] float dashSpeed = 50.0f;
    [SerializeField] float startDashCoolDown = 2.0f;
    [SerializeField] ParticleSystem skillParticles;
    [SerializeField] ParticleSystem dashParticles;
    [SerializeField] LevelLoader levelLoader;
    [SerializeField] ParticleSystem restartParticlesEffect;
    public bool hasDoubleJump = false;
    public bool hasDash= false;

    bool singing;
    CircleCollider2D singArea;

    float dashTime;
    float dashCoolDown;
    float dashDirection;
    bool canDoubleJump = true;

    float verticalDirection;

    private const string STATE_RUNNING = "Running";
    private const string STATE_FALLING = "Falling";
    private const string STATE_JUMPING = "Jumping";
    private const string STATE_GROUND = "Grounded";

    // Cached componenet references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        singArea = GetComponentInChildren<CircleCollider2D>();

        singArea.enabled = false;
        dashCoolDown = startDashCoolDown;
        dashTime = startDashTime;

        singing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Sing();
        if (!singing) {
            Run();
            Jump();
            if (hasDash) {
                Dash();
            }
            FlipSprite();
        }
        StartJumpFallAnimation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hazzards"))
        {
            StartCoroutine(ProcessPlayerDeath());
        }
    }
    IEnumerator ProcessPlayerDeath()
    {
        GameObject.Instantiate(restartParticlesEffect, transform.position, transform.rotation);
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(0.2f);
        levelLoader.RestartScene();
    }

    private void StartJumpFallAnimation()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            verticalDirection = myRigidbody.velocity.y;
            if(verticalDirection == 0)
            {
                myAnimator.SetBool(STATE_GROUND, true);
                myAnimator.SetBool(STATE_FALLING, false);
                myAnimator.SetBool(STATE_JUMPING, false);
            }
            else if (verticalDirection > 0)
            {
                myAnimator.SetBool(STATE_GROUND, false);
                myAnimator.SetBool(STATE_FALLING, false);
                myAnimator.SetBool(STATE_JUMPING, true);
            }
            else
            {
                myAnimator.SetBool(STATE_GROUND, false);
                myAnimator.SetBool(STATE_FALLING, true);
                myAnimator.SetBool(STATE_JUMPING, false);
            }

        }
        else
        {
            myAnimator.SetBool(STATE_GROUND, true);
            myAnimator.SetBool(STATE_FALLING, false);
            myAnimator.SetBool(STATE_JUMPING, false);
        }

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

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool(STATE_RUNNING, playerHorizontalSpeed);
        }
        else
        {
            myAnimator.SetBool(STATE_RUNNING, false);
        }
    }
    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
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
                    StartCoroutine("ShowDashAnimation");
                    dashTime -= Time.deltaTime;
                    myRigidbody.velocity = Vector2.right * dashDirection * dashSpeed;
                }
            }
            else
            {
                dashCoolDown = 0;
            }
        }
        else
        {
            dashCoolDown -= Time.deltaTime;
        }
    }

    public void GetNewSkill(PlayerSkills altarSkill)
    {
        myRigidbody.velocity += Vector2.up * 8;
        StartCoroutine("ShowSkillAnimation");

        if(altarSkill == PlayerSkills.dash)
        {
            hasDash = true;
        }
        else if(altarSkill == PlayerSkills.doubleJump)
        {
            hasDoubleJump = true;
        }
    }

    IEnumerator ShowSkillAnimation()
    {
        myAnimator.SetTrigger("Praying");
        yield return new WaitForSeconds(0.3f);
        ParticleSystem partycleSystem = GameObject.Instantiate(skillParticles, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.6f);
        Destroy(partycleSystem.gameObject);
    }

    IEnumerator ShowDashAnimation()
    {
        ParticleSystem partycleSystem = GameObject.Instantiate(dashParticles, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.6f);
        Destroy(partycleSystem.gameObject);
    }

    private void Sing() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            singArea.enabled = true;
        } else if (Input.GetKeyUp(KeyCode.Q)){
            singArea.enabled = false;
        }
    }
}
