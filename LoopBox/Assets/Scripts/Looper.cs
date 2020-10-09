using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    public GameObject clonePrefab;
    List<GameObject> currLoops = new List<GameObject>();
    GameObject currLoop;
    PlayerStats stats;
    List<InputAtTime> inputRecording;
    public bool recording = false;
    bool loopActivated = false;
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
            stats.recordLeft -= Time.deltaTime;
            if(stats.recordLeft < 0)
            {
                stats.recordLeft = 0;
                recording = false;
                InstantiateClone();
            }
        }
    }

    public void Record()
    {
        if(stats.recordLeft > 0)
        {
            recording = !recording;
            if (recording)
            {
                timeInRecording = 0f;
            }
            else
            {
                if (currLoop)
                {
                    //Destroy(currLoop);
                }

                InstantiateClone();
            }
        }
    }
    void InstantiateClone()
    {

        GameObject clone = Instantiate(clonePrefab, null);
        CloneController controller = clone.GetComponent<CloneController>();
        controller.playTime = Time.time;
        controller.recording = inputRecording;
        clone.transform.position = controller.recording[0].position;

        currLoop = clone;
        inputRecording = new List<InputAtTime>();
    }

    public void PlayPauseRecording()
    {
        if(currLoop)
        {
            if (loopActivated)
            {
                loopActivated = false;
                currLoop.GetComponent<CloneController>().DeactivateLoop();
            }
            else
            {
                loopActivated = true;
                currLoop.GetComponent<CloneController>().ActivateLoop();
            }
        }
    }
}

