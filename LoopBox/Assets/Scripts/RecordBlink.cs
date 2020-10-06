using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordBlink : MonoBehaviour
{

    Image image;
    float lastBlick;
    bool recording = false;
    private void Start()
    {
        image = GetComponent<Image>();
        lastBlick = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (recording && Time.time - lastBlick > 0.5f)
        {
            lastBlick = Time.time;
            image.enabled = !image.enabled;
        }
    }

    public void StopRecording()
    {
        recording = false;
        image.enabled = true;
    }
    public void StartRecording()
    {
        recording = true;
        image.enabled = true;
    }
}
