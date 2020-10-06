using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    public GameObject clonePrefab;
    PlayerStats stats;
    List<InputAtTime> inputRecording;
    bool recording = false;
    float recordStartTime = 0f;
    float timeInRecording = 0f;

    // Start is called before the first frame update
    void Start()
    {
        inputRecording = new List<InputAtTime>();
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {
            InputAtTime inputAtTime = new InputAtTime();
            inputAtTime.position = transform.position;
            inputAtTime.time = timeInRecording;
            inputAtTime.move = Input.GetAxis("Horizontal");
            inputAtTime.jump = Input.GetButtonDown("Jump");
            inputAtTime.jumping = Input.GetButton("Jump");
            inputAtTime.pickUp = Input.GetButtonDown("PickUp");

            inputRecording.Add(inputAtTime);
            timeInRecording += Time.deltaTime;
        }
    }

    public void Record()
    {
        recording = !recording;
        if (recording)
        {
            timeInRecording = 0f;
        }
    }

    public void PlayRecording()
    {
        if(inputRecording.Count > 0)
        {
            GameObject clone = Instantiate(clonePrefab, null);
            CloneController controller = clone.GetComponent<CloneController>();
            controller.playTime = Time.time;
            controller.recording = inputRecording;
            clone.transform.position = controller.recording[0].position;
            inputRecording = new List<InputAtTime>();
        }
    }
}

