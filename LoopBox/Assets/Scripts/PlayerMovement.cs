using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{

    #region Constants
    const float GROUNDED_BUFFER = 0.1f;
    const float GROUND_RAYCAST_BUFFER = 0.01f;
    #endregion

    public GameObject playerPrefab;

    public float movementSpeed = 5f;
    public float groundedMovementSmoothing = 0.1f;
    public float inAirMovementSmoothing = 0.3f;
    public float jumpForce = 4f;

    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 0.6f;

    public Transform startingPoint;
    bool isFacingRight = true;

    Hands hands;

    PlayerLooper playerLooper;
    Rigidbody2D rb;
    Vector3 velocity;
    float move;
    bool jump;
    bool jumping;
    bool pickUp;
    BoxCollider2D coll;
    bool isGrounded = false;
    bool isJumping = false;

    //Recording
    bool record = false;
    bool recording = false;
    int numFramesRecorded = 0;
    Queue<Vector3> positionRecording;
    Queue<bool> pickUpRecording;

    // Start is called before the first frame update
    void Start()
    {
        hands = GetComponentInChildren<Hands>();
        transform.position = startingPoint.position;
        velocity = Vector2.zero;
        positionRecording = new Queue<Vector3>();
        pickUpRecording = new Queue<bool>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        playerLooper = GetComponent<PlayerLooper>();
    }

    void PlayerInput()
    {
        move = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        jumping = Input.GetButton("Jump");
        record = Input.GetButtonDown("Record");
        pickUp = Input.GetButtonDown("PickUp");

        if (recording)
        {
            pickUpRecording.Enqueue(pickUp);
            positionRecording.Enqueue(transform.position);
            numFramesRecorded += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        IsGrounded();
        if (jump)
        {

            Jump();
        }

        if (record)
        {
            ToggleRecording();
        }

        if (pickUp)
        {
            if(hands.objInHands != null)
            {
                hands.LetGo();
            }
            else
            {
                hands.PickUp();
            }
        }
    }

    private void FixedUpdate()
    {
        Move(move * movementSpeed * Time.fixedDeltaTime);
    }

    void Move(float move)
    {
        float movementSmoothing = isGrounded ? groundedMovementSmoothing : inAirMovementSmoothing;
        if(rb.velocity.y < 0)
        {
            rb.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if(rb.velocity.y > 0 && !jumping)
        {
            rb.velocity += Physics2D.gravity * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        Vector2 targetVelocity = new Vector2(move * 100, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoothing);

        playerLooper.FlipCharacter(move);
    }

    void Jump()
    {
        if(isGrounded && !isJumping)
        {
            if(transform.parent != null)
            {
                transform.parent.GetComponent<Hands>().LetGo();
            }
            rb.velocity += (Vector2.up * jumpForce);
            isJumping = true;
        }
    }

    void CreateNewPlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab, null);
        newPlayer.GetComponent<PlayerMovement>().enabled = false;
        Looper looper = newPlayer.GetComponent<Looper>();
        newPlayer.layer = LayerMask.NameToLayer("Looper");
        looper.enabled = true;
        looper.positionQueue = positionRecording;
        looper.pickUpQueue = pickUpRecording;
        looper.numFramesRecorded = numFramesRecorded;
    }

    void IsGrounded()
    {
        Vector3 middleBottom = transform.position + (Vector3.down * coll.size.y/2) + (Vector3.down * GROUND_RAYCAST_BUFFER);
        Vector3 middleRight = transform.position + (Vector3.down * coll.size.y/2) + (Vector3.right * coll.size.x/2) + (Vector3.down * GROUND_RAYCAST_BUFFER);
        Vector3 middleLeft = transform.position + (Vector3.down * coll.size.y/2) + (Vector3.left * coll.size.x/2) + (Vector3.down * GROUND_RAYCAST_BUFFER);

        isGrounded = 
            (Physics2D.Raycast(middleBottom, Vector2.down, GROUNDED_BUFFER).collider != null) || 
            (Physics2D.Raycast(middleLeft, Vector2.down, GROUNDED_BUFFER).collider != null) || 
            (Physics2D.Raycast(middleRight, Vector2.down, GROUNDED_BUFFER).collider != null);


        if (isGrounded)
        {
            isJumping = false;
        }
    }


    void ToggleRecording()
    {
        record = false;
        if (!recording)
        {
            if (isGrounded)
            {
                recording = true;
            }
        }
        else
        {
            recording = false;
            CreateNewPlayer();
            numFramesRecorded = 0;
            positionRecording = new Queue<Vector3>();
            pickUpRecording = new Queue<bool>();
        }
    }
}
