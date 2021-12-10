using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatformController : MonoBehaviour
{
    public bool decaying = false;
    public float decayDelay;
    public float delayTimer;

    void Start()
    {
        delayTimer = decayDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (decaying)
        {
            if (delayTimer <= 0)
            {
                foreach (Transform child in transform) Destroy(child.gameObject);
                for (int i = 0; i < 1; i++)
                    delayTimer = decayDelay;
            }
            else delayTimer -= Time.deltaTime;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        decaying = true;
    }
}
