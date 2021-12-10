using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelPlatform : MonoBehaviour
{
    public float rotationDelay;
    public float timeTillHold;
    public GameObject anchor;
    public bool rotating = true;
    public float holdDelay;
    private float holdDelayInitial;

    void Start()
    {
        timeTillHold = rotationDelay;
        holdDelayInitial = holdDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            anchor.transform.Rotate(Vector3.back * .5f);
            if (timeTillHold <= 0)
            {
                rotating = false;
            }
            else timeTillHold -= Time.deltaTime;
        }
        else
        {
            if (holdDelay <= 0)
            {
                rotating = true;
                holdDelay = holdDelayInitial;
                timeTillHold = rotationDelay;
            }
            else holdDelay -= Time.deltaTime;
        }
    }

    void HoldWheel()
    {
        rotating = false;
    }
}
