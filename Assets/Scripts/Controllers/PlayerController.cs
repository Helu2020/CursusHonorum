//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour

{
    #region variables
    public float speed=2;
    private Rigidbody2D rigidBody;
    Animator anim;
    #endregion

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
        }
        else if (Input.GetKey("s") || Input.GetKey("down"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -speed);
        }
        else if (Input.GetKey("w") || Input.GetKey("up"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, 0);
        }

        anim.SetFloat("MovX", rigidBody.velocity.x);
        anim.SetFloat("MovY", rigidBody.velocity.y);
    }
}
