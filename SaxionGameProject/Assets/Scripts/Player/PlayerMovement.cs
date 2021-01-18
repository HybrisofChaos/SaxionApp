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

    private float horizontalSpeed;

    private float coyoteTime = 0;

    private float maxCoyoteTime = 0.1f;

    private bool grounded = false;
    private bool canJump = true;

    void Start()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        this.grounded = isGrounded();

        if(grounded){
            coyoteTime = maxCoyoteTime;
        }else{
            coyoteTime -= Time.deltaTime;
        }
    }

    public void OnMove(InputValue inputValue){
        horizontalSpeed = inputValue.Get<Vector2>().x *  speed;
    }

    public void OnJump(){
            Debug.Log(coyoteTime);
        if (coyoteTime > 0 && canJump){
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            StartCoroutine(timeJump(maxCoyoteTime));
        }
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);
    }

    bool isGrounded(){
        Vector2 topLeft = new Vector2(transform.position.x - transform.localScale.x / 4, transform.position.y + transform.localScale.y / 2);
        Vector2 bottomRight = new Vector2(transform.position.x + transform.localScale.x / 4, transform.position.y - transform.localScale.y / 2 - 0.06f);
        
        return Physics2D.OverlapArea(topLeft, bottomRight, groundMask);
    }

    IEnumerator timeJump(float timeToWait){
        coyoteTime = 0;
        canJump = false;
        yield return new WaitForSeconds(timeToWait);
        canJump = true;
    }
}
