using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyBehaviour : MonoBehaviour
{
    [Header("Player Detection")] 
    public Transform LOSPoint;
    public ContactFilter2D LOSContactFilter;
    public List<RaycastHit2D> contactList;

    [Header("Movement")]
    public float runForce;
    public Transform groundAheadPoint;
    public Transform lookAheadPoint;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public bool isGroundAhead;

    private Rigidbody2D rigidbody2D;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckIfGroundAhead();
        CheckIfWallInFront();

        if (!HasLOS()) // patrol
        {
            animator.enabled = true;
            animator.Play("Run");
            MoveEnemy();
        }
        else // Stop Patrolling
        {
            animator.enabled = false;
        }
        
    }

    private void CheckIfGroundAhead()
    {
        var hit = Physics2D.Linecast(transform.position, groundAheadPoint.position, groundLayerMask);
        isGroundAhead = (hit) ? true : false;
    }

    private void CheckIfWallInFront()
    {
        var hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, wallLayerMask);
        if (hit)
        {
            Flip();
        }
    }

    private void MoveEnemy()
    {
        if (isGroundAhead)
        {
            rigidbody2D.AddForce(Vector2.left * runForce * transform.localScale.x);
            rigidbody2D.velocity *= 0.99f;
        }
        else
        {
            Flip();
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }

    private bool HasLOS()
    {
        contactList = new List<RaycastHit2D>(); // makes empty ContactList
        var hit = Physics2D.Linecast(transform.position, LOSPoint.position, LOSContactFilter, contactList);

        return ((hit > 0) && (contactList[0].collider.gameObject.CompareTag("Player")));
    }

    // UTILITY Functions
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, groundAheadPoint.position);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, lookAheadPoint.position);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, LOSPoint.position);
    }
}
