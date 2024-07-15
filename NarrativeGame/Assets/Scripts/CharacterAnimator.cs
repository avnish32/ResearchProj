using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myRigidBody.velocity.Equals(Vector2.zero))
        {
            //Debug.Log("Playing idle");
            myAnimator.Play("Idle");
            return;
        }

        Vector2 normalizedVelocity = myRigidBody.velocity.normalized;

        if (Mathf.Abs(normalizedVelocity.x).Equals(Mathf.Abs(normalizedVelocity.y)) ||
            Mathf.Abs(normalizedVelocity.y) > Mathf.Abs(normalizedVelocity.x))
        {
            if (normalizedVelocity.y > 0)
            {
                //Debug.Log("Playing up");
                myAnimator.Play("Up");
                return;
            }

            //Debug.Log("Playing down");
            myAnimator.Play("Down");
            return;
        }

        if (normalizedVelocity.x > 0)
        {
            //Debug.Log("Playing right");
            myAnimator.Play("Right");
            return;
        }

        //Debug.Log("Playing left");
        myAnimator.Play("Left");
        return;
    }
}
