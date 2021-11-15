using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement")]
    public PlatformDirection direction;
    [Range(1, 10)]
    public float distance;
    [Range(0.5f, 10)]
    public float speed;
    public float distanceOffset;
    public bool isLooping;

    private Vector2 startingPosition;
    private bool isMoving;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        isMoving = true;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();

        if (isLooping)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            UpdateTimer();
        }
    }

    private void MovePlatform()
    {
        Vector2 newPosition = new Vector2();

        var pingPongValue = isMoving ? Mathf.PingPong(timer * speed, distance) : distance;

        if ((!isLooping) && (pingPongValue >= distance - distanceOffset))
        {
            isMoving = false;
        }


        switch (direction)
        {
            case PlatformDirection.HORIZONTAL:
                newPosition = new Vector2(startingPosition.x + pingPongValue, transform.position.y);
                break;
            case PlatformDirection.VERTICAL:
                newPosition = new Vector2(transform.position.x, startingPosition.y + pingPongValue);
                break;
            case PlatformDirection.DIAGONAL_UP:
                newPosition = new Vector2(startingPosition.x + pingPongValue, startingPosition.y + pingPongValue);
                break;
            case PlatformDirection.DIAGONAL_DOWN:
                newPosition = new Vector2(startingPosition.x + pingPongValue, startingPosition.y - pingPongValue);
                break;
        }

        transform.position = newPosition;

    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
    }
}
