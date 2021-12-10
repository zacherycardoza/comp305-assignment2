using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheelPlatform : MonoBehaviour
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
            anchor.transform.Rotate(new Vector3(0, 0, 45));
            timeTillRotation = rotationDelay;
        }
        else timeTillRotation -= Time.deltaTime;
    }
}
