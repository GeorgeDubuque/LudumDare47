using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement movement;
    public List<InputAtTime> recording;
    public float playTime = 0f;
    int currRecord = 0;
    bool shouldMove = false;
    float move = 0;
    InputAtTime currInput;

    float lastInputTime = 0f;
    float timeInRecording = 0f;

    void Start()
    {
        movement = GetComponent<PlayerMovement>(); 
    }

    // Update is called once per frame
    void Update()
    {

        currInput = recording[currRecord];
        if(currInput.time <= timeInRecording)
        {
            if (currInput.jump)
            {
                movement.Jump();
            }

            movement.JumpHigher(currInput.jumping);


            if (currInput.pickUp)
            {
                movement.PickUp();
            }

            move = (currInput.move);
            shouldMove = true;

            currRecord += 1;
            lastInputTime = Time.time;
        } 
        timeInRecording += Time.deltaTime;
        if(currRecord >= recording.Count)
        {
            currRecord = 0;
            timeInRecording = 0f;
            transform.position = recording[currRecord].position;
        }
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            movement.Move(move);
            shouldMove = false;
        }
    }
}
