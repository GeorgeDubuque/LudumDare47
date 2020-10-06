using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject[] onOffObj;
    Animator animator;
    AudioSource audioSource;
    public AudioClip switchOn;
    public AudioClip switchOff;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //animator.SetBool("On", false);
    }

    public void ToggleGameObject()
    {
        for(int i = 0; i < onOffObj.Length; i++)
        {
            bool currOpen = onOffObj[i].GetComponentInChildren<SteelWall>().open;
            onOffObj[i].GetComponentInChildren<SteelWall>().open = !currOpen;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!on)
    //    {
    //        animator.SetBool("On", true);
    //        on = true;
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("On", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        animator.SetBool("On", true);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        animator.SetBool("On", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("On", false);
    }

    public void SwitchOnSound()
    {
        audioSource.Stop();
        audioSource.clip = switchOn;
        audioSource.Play();
    }
    public void SwitchOffSound()
    {
        audioSource.Stop();
        audioSource.clip = switchOff;
        audioSource.Play();
    }
}
