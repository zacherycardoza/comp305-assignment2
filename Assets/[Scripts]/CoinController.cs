using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource coinSound;

    void Start()
    {
        coinSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            coinSound.Play();
            transform.position = Vector3.down * 50.0f;
        }
    }
}
