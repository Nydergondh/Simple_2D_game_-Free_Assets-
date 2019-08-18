using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement player;
    private PlayerAttack attackType; //if attack is none can move else stop movement to attack

    public int maxHealth = 16;
    public int health;
    public int damage = 1;
    public float walkSpeed = 2f;
    public float runSpeed = 3.5f;
    public float jumpSpeed = 5f;
    public int whipLv = 1;

    private float deltaX;
    private float deltaY;
    public float deltaYMinimum;

    public bool transitioning;
    private bool gotHit;
    private bool invincible;
    public bool isTouchingGround;


    public bool jumping;
    public bool running;
    public bool walking;

    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    private SpriteRenderer playerRenderer;
    public new Rigidbody2D rigidbody { get; set; }
    private Animator playerAnim;

    void Awake() {

        if (player == null) {
            player = this;
        }
        else {
            Destroy(player);
            player = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        attackType = GetComponent<PlayerAttack>();
        jumping = false;
        running = false;
        walking = false;
        isTouchingGround = true;

        health = maxHealth;
        transitioning = false;
        invincible = false;
        gotHit = false;

        playerAnim = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        playerAnim.SetBool("Jump", false);
    }

    // Update is called once per frame
    void Update() {
        if (attackType.GetAttackType() == AttackTypes.None) {
            Movement();      
        }
        
    }

    private void Movement() {
        Vector2 vel;
        if (!playerAnim.GetBool("CrouchJump") && attackType.GetAttackType() == AttackTypes.None) {

            if (Input.GetButton("Run")) {
                deltaX = Input.GetAxis("Horizontal") * runSpeed;
            }
            else {
                deltaX = Input.GetAxis("Horizontal") * walkSpeed;
            }

        }

        else {
            deltaX = Input.GetAxis("Horizontal");
            deltaY = 0;
        }

        //IF not on "crocuh jump" state and is not attacking THEN player can move
        if (!playerAnim.GetBool("CrouchJump") && attackType.GetAttackType() == AttackTypes.None) {
            //set the speed value of the animator so that it can change aniamtions
            playerAnim.SetFloat("SpeedX",Mathf.Abs(deltaX));
            //Jump if on Ground
            if (Input.GetButtonDown("Jump") && isTouchingGround && !jumping) {
                playerAnim.SetBool("Jump", true);
                jumping = playerAnim.GetBool("Jump");
                deltaY = jumpSpeed;
            }
            //Change Variables if was juming and hitted the ground
            else if (rigidbody.velocity.y == 0 && jumping && !isTouchingGround) {
                playerAnim.SetBool("Jump", false);
                jumping = false;
                deltaY = 0f;
            }
            //Is On Air
            else if ((rigidbody.velocity.y > 0 || rigidbody.velocity.y < 0) && jumping) {
                deltaY = rigidbody.velocity.y; ;
            }
            //on Ground and not Jumping
            else {
                deltaY = rigidbody.velocity.y;
            }

            playerAnim.SetFloat("SpeedY",deltaY);

            if (jumping) {
                playerAnim.SetBool("Run", false);
                playerAnim.SetBool("Walk", false);
            }

            ChangeDirection(deltaX);
        }
        vel = new Vector2(deltaX, deltaY);
        rigidbody.velocity = vel;

    }

    private void ChangeDirection(float delta) {

        if (delta < 0 || (transform.localScale.x < 0 && delta == 0)) {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }
        else {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }

    }

    private void SetVariables() {

        running = playerAnim.GetBool("Run");
        walking = playerAnim.GetBool("Walk");
        jumping = playerAnim.GetBool("Jump");

    }

    public void CrouchAfterJump() {

        playerAnim.SetBool("CrouchJump", true);
        playerAnim.SetBool("Jump", false);
        jumping = false;
        rigidbody.velocity = new Vector2(deltaX, 0f);

    }

    public void FinishCrouchJump() {

        playerAnim.SetBool("CrouchJump", false);
        isTouchingGround = true;

    }


    public void Fall() {

        playerAnim.SetBool("CrouchJump", false);
        isTouchingGround = false;

    }

    public float GetYSpeed() {
        return deltaY;
    }

}
