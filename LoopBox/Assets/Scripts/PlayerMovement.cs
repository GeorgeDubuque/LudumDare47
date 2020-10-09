using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{

    #region Constants
    const float GROUNDED_BUFFER = 0.3f;
    const float GROUND_RAYCAST_BUFFER = 0.01f;
    #endregion


    Rigidbody2D rb;
    Vector3 velocity;

    CapsuleCollider2D capColl;
    BoxCollider2D boxColl;

    bool isFacingRight = true;
    bool jumping;
    bool isGrounded = false;
    bool isJumping = false;
    Vector3 jumpStartPosition;

    public Animator animator;
    public Animator armsAnimator;

    GameManager gameManager;
    Hands hands;
    PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = GetComponent<PlayerStats>();
        hands = GetComponentInChildren<Hands>();
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        capColl = GetComponent<CapsuleCollider2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        IsGrounded();
    }

    public void Move(float move)
    {
        move = move * stats.movementSpeed * Time.fixedDeltaTime;
        animator.SetBool("Moving", move != 0);
        armsAnimator.SetBool("Moving", move != 0);

        float movementSmoothing = isGrounded ? stats.groundedMovementSmoothing : stats.inAirMovementSmoothing;
        if(rb.velocity.y < 0 || transform.position.y - jumpStartPosition.y > stats.maxJumpSpeed)
        {
            rb.velocity += Physics2D.gravity * (stats.fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if(rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Physics2D.gravity * (stats.lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        Vector2 targetVelocity = new Vector2(move * 100, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        FlipCharacter(move);
    }

    public void PickUp()
    {

        if(hands.isHoldingSomething)
        {
            hands.LetGo();
        }
        else
        {
            armsAnimator.SetTrigger("PickUp");
        }
    }

    public void Jump()
    {
        if(isGrounded && !isJumping)
        {
            if(transform.parent != null)
            {
                boxColl.enabled = true;
                capColl.enabled = true;
                transform.parent.GetComponent<Hands>().LetGo();
                transform.localRotation = Quaternion.identity;
                Vector3 currVel = rb.velocity;
                currVel.y *= 0;
                rb.velocity = currVel;
            }
            jumpStartPosition = transform.position;
            rb.velocity += (Vector2.up * stats.jumpForce);
            isJumping = true;
        }
    }

    public void JumpHigher(bool jumpHigher)
    {
        jumping = jumpHigher;
    }
    public void FlipCharacter(float move)
    {
        if(move > 0 && !isFacingRight)
        {
            Flip();
        }
        if(move < 0 && isFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
    }

    public bool IsGrounded()
    {
        Vector3 middleBottom = 
            (capColl.bounds.center + new Vector3(capColl.offset.x, capColl.offset.y, 0) ) + 
            (Vector3.down * capColl.size.y) + 
            (Vector3.down * GROUND_RAYCAST_BUFFER);

        Vector3 middleRight = 
            (capColl.bounds.center + new Vector3(capColl.offset.x, capColl.offset.y, 0) ) + 
            (Vector3.down * (capColl.size.y)) + 
            (Vector3.right * capColl.size.x/2) + 
            (Vector3.down * GROUND_RAYCAST_BUFFER);

        Vector3 middleLeft = 
            (capColl.bounds.center + new Vector3(capColl.offset.x, capColl.offset.y, 0) ) + 
            (Vector3.down * capColl.size.y) + 
            (Vector3.left * capColl.size.x/2) + 
            (Vector3.down * GROUND_RAYCAST_BUFFER);

        Debug.DrawRay(middleBottom, Vector3.down * GROUNDED_BUFFER, Color.red);
        Debug.DrawRay(middleLeft, Vector3.down * GROUNDED_BUFFER, Color.red);
        Debug.DrawRay(middleRight, Vector3.down * GROUNDED_BUFFER, Color.red);

        isGrounded = 
            (Physics2D.Raycast(middleBottom, Vector2.down, GROUNDED_BUFFER).collider != null) || 
            (Physics2D.Raycast(middleLeft, Vector2.down, GROUNDED_BUFFER).collider != null) || 
            (Physics2D.Raycast(middleRight, Vector2.down, GROUNDED_BUFFER).collider != null);


        if (isGrounded)
        {
            isJumping = false;
        }

        return isGrounded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            if (hands.objInHands && hands.objInHands.CompareTag("Key"))
            {
                hands.objInHands = null;
                string nextScene = collision.gameObject.GetComponent<Door>().nextLevelName;
                gameManager.LoadLevel(nextScene);
            }
        }
        if (collision.gameObject.CompareTag("Death"))
        {
            gameManager.ResetLevel();
        }
    }
}
