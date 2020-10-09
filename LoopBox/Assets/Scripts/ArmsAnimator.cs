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
            foreach(Collider2D coll in hands.objInHands.GetComponents<Collider2D>())
            {
                coll.enabled = true;
            }
            hands.objInHands.transform.up = Vector2.up;
        }
    }
    public void HandsPickUp()
    {
        hands.PickUp();
    }
    // Start is called before the first frame update
}
