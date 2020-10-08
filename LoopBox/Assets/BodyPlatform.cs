using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = Vector2.up;
        //rb.MovePosition(transform.parent.position);
    }
}
