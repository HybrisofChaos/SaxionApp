using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[@RequireComponent (typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float speed = 10;
    public float jumpSpeed = 25;

    public LayerMask groundMask;

    private Rigidbody2D rb2d;
    private Animator anim;

    private float horizontalSpeed;

    private float coyoteTime = 0;

    private float maxCoyoteTime = 0.1f;

    private bool grounded = false;
    private bool canJump = true;


    private float originalXScale;
    private int direction;


    void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.rb2d = this.GetComponent<Rigidbody2D>();

        direction = 1;
        originalXScale = transform.localScale.x;
    }

    void Update()
    {
        this.grounded = isGrounded();

        if(grounded){
            coyoteTime = maxCoyoteTime;
            this.anim.SetBool("isJumping", false);
        }
        else{
            coyoteTime -= Time.deltaTime;
            this.anim.SetBool("isJumping", true);
        }
    }

    public void OnMove(InputValue inputValue){
        horizontalSpeed = inputValue.Get<Vector2>().x *  speed;
    }

    public void OnJump(){
        if (coyoteTime > 0 && canJump){
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            StartCoroutine(timeJump(maxCoyoteTime));
        }
    }

    void FixedUpdate()
    {
        if(horizontalSpeed != 0)
        {
            this.anim.SetBool("isRunning", true);
        }
        else
        {
            this.anim.SetBool("isRunning", false);
        }
        CharacterDirectionCheck(horizontalSpeed);
        rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);
    }

    bool isGrounded(){
               Vector2 topLeft = new Vector2(transform.position.x - transform.localScale.x / 4 - direction, transform.position.y);
               Vector2 bottomRight = new Vector2(transform.position.x + transform.localScale.x / 4 - direction, transform.position.y - transform.localScale.y / 2 - 0.06f- 1.3f);
               Vector3 debugStart = topLeft;
               Vector3 debugEnd = bottomRight;
               Debug.DrawLine(debugStart, debugEnd, Color.green, 0.8f);

               return Physics2D.OverlapArea(topLeft, bottomRight, groundMask);
    }

    IEnumerator timeJump(float timeToWait){
        coyoteTime = 0;
        canJump = false;
        yield return new WaitForSeconds(timeToWait);
        canJump = true;
    }

    private void CharacterDirectionCheck(float xSpeed)
    {
        // If the sign of the character's horizontal speed
        // is different from the sign of the direction they are facing in,
        // then we need to flip the character to face the other direction.
        if (xSpeed * direction < 0.0f)
        {
            // Flip the direction the character is facing in
            direction *= -1;

            // Record the character's current scale
            Vector3 scale = transform.localScale;

            // Set the character's X-scale to be their original scale
            // times the direction they are facing in
            scale.x = originalXScale * direction;

            // Apply the new scale to the character's transform
            transform.localScale = scale;
        }
    }


}
