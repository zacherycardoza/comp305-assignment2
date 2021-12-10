using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipToHazardPlatformController : MonoBehaviour
{
    public float rotationDelay;
    public float timeTillRotation;
    public GameObject anchor;
    void Start()
    {
        timeTillRotation = rotationDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTillRotation <= 0)
        {
            anchor.transform.Rotate(new Vector3(0, 0, 180));
            timeTillRotation = rotationDelay;
        }
        else timeTillRotation -= Time.deltaTime;
    }
}
