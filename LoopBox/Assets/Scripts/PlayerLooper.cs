using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLooper : MonoBehaviour
{
    public Animator animator;
    public bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
