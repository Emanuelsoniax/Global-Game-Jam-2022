using System.Collections.Generic;
using UnityEngine;

public class Player : Attractable
{
    public float movementSpeed;
    public float walkSpeed = 4f;
    public float sprintSpeed = 8f;
    public float jumpingPower = 600f;
    private float horizontal;
    private float vertical;
    private bool facingRight = true;
    public bool isGrounded;
    private bool doJump;
    private bool doThrow;
    public float throwForce = 1000f;
    public int playerId = 1;

    public List<GameObject> drops;

    public Animator animator;
    public Camera playerCamera;
    public BoxCollider2D area;
    private Inventory inventory;

    //Inputs:
    private string horizontalInput;
    private string verticalInput;
    private string jumpInput;
    public string interactInput;
    private string throwInput;
    public string inventoryInput;
    public string nextInput;
    public string previousInput;

    void Start()
    {
        playerCamera.enabled = false;
        inventory = GetComponent<Inventory>();
        InputStart();
    }

    void InputStart()
    {

        horizontalInput = "Horizontal" + playerId;
        verticalInput = "Vertical" + playerId;

        jumpInput = "Jump" + playerId;
        interactInput = "Interact" + playerId;
        throwInput = "Throw" + playerId;
        inventoryInput = "Inventory" + playerId;

        nextInput = "Next" + playerId;
        previousInput = "Previous" + playerId;

    }

    void Update()
    {
        if (isGrounded)
        {
            horizontal = Input.GetAxis(horizontalInput);
            vertical = Input.GetAxis(verticalInput);
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }

        if (Input.GetButtonDown(jumpInput))
        {
            if (isGrounded)
            {
                doJump = true;
            }
        }

        if (Input.GetButtonDown(throwInput))
        {
            doThrow = true;
        }

        if (Input.GetButtonDown(nextInput))
        {
            inventory.NextItem();
        }

        if (Input.GetButtonDown(previousInput))
        {
            inventory.PreviousItem(); ;
        }
    }

    void FixedUpdate()
    {

        Vector2 normalDir = transform.up;
        normalDir.Normalize();

        float moveDir;

        if (transform.rotation.z >= 0.7 && transform.rotation.z < 1)
        {
            //Bottom Left
            moveDir = vertical - horizontal;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z >= -0.7)
        {
            //Top Right
            moveDir = horizontal - vertical;
        }
        else if (transform.rotation.z > -1 && transform.rotation.z <= -0.7)
        {
            //Bottom Right
            moveDir = -horizontal - vertical;
        }
        else
        {
            //Top Left
            moveDir = horizontal + vertical;
        }

        //Variable speed
        if (rb.velocity.magnitude <= movementSpeed)
        {
            rb.AddForce(new Vector2(horizontal * (movementSpeed * 10f), vertical * (movementSpeed * 10f)));

            if (Mathf.Abs(horizontal) >= 1 || Mathf.Abs(vertical) >= 1)
            {
                animator.SetFloat("Speed", 1);
            }

            if (horizontal == 0 && vertical == 0 && isGrounded)
            {
                rb.velocity = new Vector2(0, 0);
                animator.SetFloat("Speed", 0f);
            }
        }

        if ((moveDir > 0 && !facingRight) || (moveDir < 0 && facingRight))   //If player is moving towards right but is facing left,
        {       // or if player is moving towards left but is facing right,
            facingRight = !facingRight;
            Vector2 tempScale = transform.localScale;   // get player scale,
            tempScale.x *= -1;                          // flip x-axis [-1 (left) or 1 (right)],
            transform.localScale = tempScale;           // apply new scale to player.
        }

        if (doJump)
        {
            Vector2 jumpForce = new Vector2(jumpingPower, jumpingPower);
            jumpForce *= normalDir;
            rb.AddForce(jumpForce);  //Apply upwards force to player (jump)
            doJump = false;
            isGrounded = false;

            animator.SetBool("IsJumping", true);
        }

        if (doThrow)
        {
            Item selectedItem = inventory.characterItems[inventory.selectedItem];
            if (selectedItem != null)
            {
                GameObject G = Instantiate(selectedItem.gameObject, transform.position, Quaternion.identity);
                inventory.RemoveItem(selectedItem);
                G.GetComponent<Rigidbody2D>().AddForce(normalDir * throwForce);
                
            }
            doThrow = false;
        }

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {   //If player is on ground,
            isGrounded = true;              // set bool to true  
            animator.SetBool("IsJumping", false);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other == area)
        {
            playerCamera.enabled = false;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == area)
        {
            playerCamera.enabled = true;
        }
    }

}