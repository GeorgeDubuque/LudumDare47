using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsAnimator : MonoBehaviour
{
    public Hands hands;
    private void Start()
    {
    }
    public void HandsPickUp()
    {
        hands.PickUp();
    }
    // Start is called before the first frame update
}
