using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float groundedMovementSmoothing = 0.05f;
    public float inAirMovementSmoothing = 0.07f;
    public float jumpForce = 7f;
    public float maxJumpSpeed = 2f;
    public float totalLoopTime = 10f;
    public float recordLeft;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
}
