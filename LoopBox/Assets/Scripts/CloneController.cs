using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovement movement;
    public List<InputAtTime> recording;
    public float playTime = 0f;
    int currRecord = 0;
    bool shouldMove = false;
    float move = 0;
    InputAtTime currInput;
    Hands hands;
    public GameObject bodyPlatform;

    float lastInputTime = 0f;
    float timeInRecording = 0f;
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        hands = GetComponentInChildren<Hands>();
        DeactivateLoop();
    }

    // Update is called once per frame
    void Update()
    {

        currInput = recording[currRecord];
        if(currInput.time <= timeInRecording)
        {
            if (currInput.jump)
            {
                movement.Jump();
            }

            movement.JumpHigher(currInput.jumping);


            if (currInput.pickUp)
            {
                movement.PickUp();
            }

            move = (currInput.move);
            shouldMove = true;

            currRecord += 1;
            lastInputTime = Time.time;
        } 
        timeInRecording += Time.deltaTime;
        if(currRecord >= recording.Count)
        {
            hands.LetGo();
            currRecord = 0;
            timeInRecording = 0f;
            transform.position = recording[currRecord].position;
        }
    }

    private void FixedUpdate()
    {
        if (shouldMove)
        {
            movement.Move(move);
            shouldMove = false;
        }
    }

    public void ActivateLoop()
    {
        ChangeOpacity(1f);
        gameObject.layer = LayerMask.NameToLayer("Looper");
        hands.gameObject.layer = LayerMask.NameToLayer("Hands");
        bodyPlatform.SetActive(true);
        bodyPlatform.layer = LayerMask.NameToLayer("BodyPlatform");
    }
    public void DeactivateLoop()
    {
        ChangeOpacity(.5f);
        gameObject.layer = LayerMask.NameToLayer("Ghost");
        hands.gameObject.layer = LayerMask.NameToLayer("Ghost");
        bodyPlatform.SetActive(false);
        bodyPlatform.layer = LayerMask.NameToLayer("Ghost");
    }

    void ChangeOpacity(float opacity)
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        opacity = Mathf.Clamp(opacity, 0f, 1f);

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.color = new Vector4(1f, 1f, 1f, opacity);
        }
    }
}
