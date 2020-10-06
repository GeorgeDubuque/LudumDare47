using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelWall : MonoBehaviour
{
    Animator animator;
    public bool startOpen = false;
    public bool open = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", open);
    }

    private void Update()
    {
        animator.SetBool("Open", open);
    }
}
