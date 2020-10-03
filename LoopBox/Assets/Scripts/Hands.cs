using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{

    BoxCollider2D coll;
    public float collTime = 0.2f;
    public float pickUpTime;
    public Transform pickUpPoint;
    public GameObject objInHands;
    PlayerLooper playerLooper;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        pickUpTime = Time.time;
        playerLooper = GetComponentInParent<PlayerLooper>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - pickUpTime > collTime)
        {
            coll.enabled = false;
        }
    }

    public void PickUp()
    {
        coll.enabled = true;
        pickUpTime = Time.time;
    }

    public void LetGo()
    {
        if(objInHands != null)
        {
            objInHands.transform.SetParent(null);
            objInHands.GetComponent<BoxCollider2D>().enabled = true;
            objInHands.GetComponent<Rigidbody2D>().isKinematic = false;
            coll.enabled = false;
            transform.position = transform.parent.position + Vector3.right * transform.localScale.x;
            objInHands = null;
            playerLooper.animator.SetTrigger("PutDown");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.layer != LayerMask.NameToLayer("Looper"))
        {
            objInHands = obj;
            obj.transform.SetParent(transform);
            obj.GetComponent<BoxCollider2D>().enabled = false;
            obj.GetComponent<Rigidbody2D>().isKinematic = true;
            coll.enabled = true;
            //transform.position = pickUpPoint.position;
            playerLooper.animator.SetTrigger("PickUp");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision: " + collision.gameObject.name); 
    }
}
