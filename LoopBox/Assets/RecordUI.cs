using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordUI : MonoBehaviour
{
    public TMP_Text recordTimeLeft;
    public Image recordBlinker;
    public PlayerStats playerStats;
    public Looper looper;
    public float blinkSpeed = 0.3f;

    bool prevRecording;
    float startBlinkTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        looper = FindObjectOfType<Looper>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!prevRecording && looper.recording)
        {
            startBlinkTime = Time.time;
        }

        if (looper.recording)
        {
           if(Time.time - startBlinkTime > blinkSpeed)
           {
                recordBlinker.enabled = !recordBlinker.enabled;
                startBlinkTime = Time.time;
           }
        }
        else
        {
            recordBlinker.enabled = true;
        }

        prevRecording = looper.recording;

        recordTimeLeft.text = Math.Round(playerStats.recordLeft, 2) + "(s)";
    }
}
