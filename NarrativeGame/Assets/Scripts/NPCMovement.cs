using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCMovement : MonoBehaviour
{
    //This is not circular
    [Tooltip("At the end, NPC will reverse direction")]
    [SerializeField]
    private Vector3[] waypoints;

    [SerializeField]
    private float movementSpeed;

    private float minTimeToWait = 1f, maxTimeToWait = 3f;
    private int currentWaypointIndex;
    private bool isGoingFwd;
    private Rigidbody2D rb;
    private bool shouldWalk = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGoingFwd = true;

        if (waypoints.Length > 0 )
        {
            currentWaypointIndex = 0;
            transform.position = waypoints[currentWaypointIndex];
            currentWaypointIndex++;
            StartCoroutine(Walk());
        }
    }

    private IEnumerator Walk()
    {
        while (shouldWalk)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) <= 0.1f)
            {
                rb.velocity = Vector2.zero;
                yield return new WaitForSeconds(UnityEngine.Random.Range(minTimeToWait, maxTimeToWait));
                if (!shouldWalk)
                {
                    continue;
                }
                UpdateCurrentWaypoint();
            }

            Vector3 direction = waypoints[currentWaypointIndex] - transform.position;
            rb.velocity = direction.normalized * movementSpeed * Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }
        
    }

    private void UpdateCurrentWaypoint()
    {
        if (currentWaypointIndex == waypoints.Length - 1 || currentWaypointIndex == 0)
        {
            isGoingFwd = !isGoingFwd;
        }

        if (isGoingFwd)
        {
            currentWaypointIndex++;
        }
        else
        {
            currentWaypointIndex--;
        }
    }

    public void StopWalking()
    {
        rb.velocity = Vector2.zero;
        shouldWalk = false;
    }

    public void StartWalking()
    {
        shouldWalk = true;
        StartCoroutine(Walk());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name+ " collided with NPC: "+this.gameObject.name);

        // NPC will stop walking if it collides with player.
        // It is not made to stop when colliding with NPC as both NPCs will then stop and remain still,
        // even if StartWalking() is called in OnCollisionExit2D(), because neither will exit the
        // other"s collision.
        if (collision.gameObject.GetComponent<PlayerMovement>() != null) 
        {
            StopWalking();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            StartWalking();
        }
    }
}
