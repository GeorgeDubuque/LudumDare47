using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{

    CircleCollider2D coll;
    public float collTime = 0.2f;
    public float pickUpTime;
    public Transform pickUpPoint;
    public GameObject objInHands;
    PlayerMovement movement;

    public bool isHoldingSomething = false;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponentInParent<PlayerMovement>();
        coll = GetComponent<CircleCollider2D>();
        pickUpTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - pickUpTime > collTime)
        {
            coll.enabled = false;
        }
        //if (objInHands)
        //{
        //    objInHands.transform.up = Vector2.up;
        //}
    }

    public void PickUp()
    {
        coll.enabled = true;
        pickUpTime = Time.time;
    }

    public void LetGo()
    {
        if(isHoldingSomething)
        {
            isHoldingSomething = false;
            movement.animator.SetBool("HoldingSomething", false);
            movement.armsAnimator.SetBool("HoldingSomething", false);
            objInHands.transform.SetParent(null);
            Vector3 vel = objInHands.GetComponent<Rigidbody2D>().velocity;
            vel.y *= 0;
            objInHands.GetComponent<Rigidbody2D>().velocity = vel;
            objInHands.GetComponent<Rigidbody2D>().isKinematic = false;
            objInHands.GetComponent<Collider2D>().enabled = true;
            objInHands = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        GameObject obj = collision.gameObject;
        isHoldingSomething = true;
        movement.animator.SetBool("HoldingSomething", true);
        movement.armsAnimator.SetBool("HoldingSomething", true);
        objInHands = obj;
        obj.transform.SetParent(transform);
        obj.GetComponent<Rigidbody2D>().isKinematic = true;
        obj.GetComponent<Collider2D>().enabled = false;
        obj.transform.position = transform.position;
        //obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
