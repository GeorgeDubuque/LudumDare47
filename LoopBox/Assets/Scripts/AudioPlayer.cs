using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource leftFoot; 
    public AudioSource rightFoot; 
    private void Start()
    {
    }
    public void LeftFoot()
    {
        leftFoot.Play();
    }
    public void RightFoot()
    {
        rightFoot.Play();
    }
}
