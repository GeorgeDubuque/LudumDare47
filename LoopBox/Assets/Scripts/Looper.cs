using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    public Queue<Vector3> positionQueue;
    public Queue<bool> pickUpQueue;
    public int numFramesRecorded;
    int currFrame = 0;
    Rigidbody2D rb;
    Hands hands;
    PlayerLooper playerLooper;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hands = GetComponentInChildren<Hands>();
        playerLooper = GetComponent<PlayerLooper>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = positionQueue.Dequeue();
        bool pickUp = pickUpQueue.Dequeue();
        Vector3 vel = position - transform.position;

        if(currFrame == 0)
        {
            transform.position = position;
        }
        else
        {
            rb.MovePosition(position);
        }

        if (pickUp)
        {
            hands.PickUp();
        }

        positionQueue.Enqueue(position);
        pickUpQueue.Enqueue(pickUp);
        currFrame += 1;

        if(currFrame >= numFramesRecorded)
        {
            currFrame = 0;
        }

        playerLooper.FlipCharacter(vel.x);
    }
}
