using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    private float yPos;
    public BoxCollider2D solid;
    public BoxCollider2D trigger;
    public bool solidActive = true;
    public float delay = 1;
    void Start()
    {
        yPos = this.gameObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!solidActive)
        {
            delay -= Time.deltaTime;
        }
        if (delay <= 0)
        {
            solid.enabled = true;
            solidActive = !solidActive;
            delay = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (yPos >= collision.gameObject.transform.position.y && solidActive)
        {
            Debug.Log("hit underneath");
            solidActive = !solidActive;
            solid.enabled = false;
        }
    }
}
