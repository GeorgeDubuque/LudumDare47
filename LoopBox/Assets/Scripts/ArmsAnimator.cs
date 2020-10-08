using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ArmsAnimator : MonoBehaviour
{
    public Hands hands;
    private void Start()
    {
    }
    public void EnableObjCollider()
    {
        if (hands.objInHands)
        {
            hands.objInHands.GetComponent<Collider2D>().enabled = true;
            hands.objInHands.transform.up = Vector2.up;
        }
    }
    public void HandsPickUp()
    {
        hands.PickUp();
    }
    // Start is called before the first frame update
}
