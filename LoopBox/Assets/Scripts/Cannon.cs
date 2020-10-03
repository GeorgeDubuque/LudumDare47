using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Start is called before the first frame update
    public float fireRate = 0.5f;
    public float firePower = 5f;
    public GameObject cannonBallPrefab;
    public Transform firePosition;

    float lastFireTime;
    void Start()
    {
        lastFireTime = Time.time; 
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Time.time - lastFireTime > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(cannonBallPrefab);
        cannonBallPrefab.transform.position = firePosition.position;
        cannonBallPrefab.GetComponent<CannonBall>().fireDir = transform.right;
        cannonBallPrefab.GetComponent<CannonBall>().firePower = firePower;
    }
}
