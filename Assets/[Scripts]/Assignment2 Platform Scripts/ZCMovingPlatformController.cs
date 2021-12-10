using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZCMovingPlatformController : MonoBehaviour
{
    public float speed;
    private float moveSpeed;
    public bool startRight = true;
    public float moveDistance;
    private Vector2 startPos;
    private Vector2 endPos;
    private Rigidbody2D rb;
    public bool moveHorizontal = false;
    public bool moveVertical = false;
    public bool movingRight = false;
    public bool movingUp = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = new Vector2(rb.position.x, rb.position.y);
        if (moveHorizontal) endPos = startRight ? new Vector2(rb.position.x + moveDistance, rb.position.y) : new Vector2(rb.position.x - moveDistance, rb.position.y);
        Debug.Log(startPos);
        Debug.Log(endPos);
        if (startRight) movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDirectionSwap();
        if (moveHorizontal) HorizontalMovement();
    }

    void HorizontalMovement()
    {
        moveSpeed = movingRight ? speed : -speed;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    void CheckDirectionSwap()
    {
        if (startRight)
        {
            if (movingRight && rb.position.x >= endPos.x) movingRight = false;
            else if (!movingRight && rb.position.x <= startPos.x) movingRight = true;
        }
        else
        {
            if (movingRight && rb.position.x >= startPos.x) movingRight = false;
            else if (!movingRight && rb.position.x <= endPos.x) movingRight = true;
        }
    }
}
