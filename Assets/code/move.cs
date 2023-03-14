using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 6;
    private float moveInput;
    private bool isGrounded;
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private Rigidbody2D rb;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private Animator anim; 


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded= Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);


        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0); 
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }



        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) 
        {
            anim.SetBool("isJumping", true);
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
            isJumping = true;
        }

        if ( Input.GetKey(KeyCode.Space) && isJumping == true )
        {
            if (jumpTimeCounter > 0)
            {
                anim.SetBool("isJumping", true);
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping= false;
            }    
            
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isJumping= false;
        }

        if (moveInput == 0)
        {
            anim.SetBool("isinWalking", false);
        }
        else
        {
            anim.SetBool("isinWalking", true);
        }

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

    }



}
