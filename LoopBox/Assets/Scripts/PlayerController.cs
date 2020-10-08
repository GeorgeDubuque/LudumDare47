using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Button Variables
    float move = 0f;
    bool record = false;
    bool playerRecording = false;
    bool jump = false;
    bool jumping = false;
    bool pickUp = false;
    public bool recording = false;
    Looper looper;
    #endregion

    PlayerMovement movement;
    PlayerStats stats;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        movement = GetComponent<PlayerMovement>();
        stats = GetComponent<PlayerStats>();
        looper = GetComponent<Looper>();
        transform.position = gameManager.start.position;
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        jumping = Input.GetButton("Jump");
        pickUp = Input.GetButtonDown("PickUp");
        record = Input.GetButtonDown("Record");
        playerRecording = Input.GetButtonDown("PlayRecording");

        if (jump)
        {
            movement.Jump();
        }

        movement.JumpHigher(jumping);


        if (pickUp)
        {
            movement.PickUp();
        }

        if (record)
        {
            looper.Record();
        }

        if (playerRecording)
        {
            looper.PlayPauseRecording();
        }
    }

    private void FixedUpdate()
    {
        movement.Move(move);
    }

}
