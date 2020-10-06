using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public TMP_Text message;
    public string messageText;

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        message.text = messageText;
    }
}
