using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Array to hold all patrol points
    private Rigidbody2D rb;
    private Animator anim; 
    private Transform currentPoint;
    public float speed;

    private int currentPatrolIndex = 0; // Index of the current patrol point

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Check if there are any patrol points assigned
        if (patrolPoints.Length > 0)
        {
            currentPoint = patrolPoints[currentPatrolIndex];
        }
        else
        {
            Debug.LogWarning("No patrol points assigned to enemyPatrol script on " + gameObject.name);
            enabled = false; // Disable the script if no patrol points are assigned
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolPoints.Length == 0)
            return; // If no patrol points assigned, exit the update loop

        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint.position.x > transform.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length; // Move to the next patrol point
            currentPoint = patrolPoints[currentPatrolIndex];
            flip();

            Debug.Log("Velocity: " + rb.velocity);

        }
    }

    private void flip() 
    {
        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Sign(rb.velocity.x); // Use the sign of velocity to determine the direction
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        if (patrolPoints == null)
            return;

        Gizmos.color = Color.yellow;

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (patrolPoints[i] != null)
            {
                Gizmos.DrawWireSphere(patrolPoints[i].position, 0.5f);
                if (i < patrolPoints.Length - 1)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
                else if (i == patrolPoints.Length - 1 && patrolPoints.Length > 1)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
                }
            }
        }
    }
}
