using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float move = 0f;
    public bool jump = false;
    public bool jumping = false;
    public bool pickUp = false;
    public bool record = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        jumping = Input.GetButton("Jump");
        record = Input.GetButtonDown("Record");
        pickUp = Input.GetButtonDown("PickUp");
    }
}
