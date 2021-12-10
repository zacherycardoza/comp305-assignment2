using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    public float lifeTime;
    public float timeLeft;
    void Start()
    {
        timeLeft = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0) Destroy(this.gameObject); else timeLeft -= Time.deltaTime;
    }
}
