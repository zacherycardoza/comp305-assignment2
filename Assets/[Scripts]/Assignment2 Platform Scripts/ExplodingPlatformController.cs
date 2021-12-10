using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlatformController : MonoBehaviour
{
    public GameObject explosionObject;
    public float explosionDelay;
    public bool explosionActive;
    public float explosionForce;
    public Sprite[] clockNumbers;
    public GameObject clock;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (explosionActive)
        {
            if (explosionDelay <= 0) Explode();
            else
            {
                explosionDelay -= Time.deltaTime;
                clock.GetComponent<SpriteRenderer>().sprite = clockNumbers[((int)Math.Floor(explosionDelay))];
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        explosionActive = true;
    }

    void Explode()
    {
        Debug.Log("Boom!!!");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 8f);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null && hit.gameObject.name == "Player") rb.AddForce(new Vector2(explosionForce * (hit.transform.position.x + transform.position.x), explosionForce * (hit.transform.position.y + transform.position.y)));
        }
        GameObject clone = Instantiate(explosionObject, transform.position, transform.rotation);
        clone.SetActive(true);
        Destroy(this.gameObject);
    }
}
