//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMobileController : MonoBehaviour
{
    #region variables
    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    public Joystick joystick;

    public float speed = 2;
    public float jumpSpeed = 3;

    private Rigidbody2D rigidBody;

    Animator anim;
    #endregion

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontalMove = joystick.Horizontal * speed;
        verticalMove = joystick.Vertical * speed;

        transform.position += new Vector3(horizontalMove, verticalMove, 0) * Time.deltaTime * speed;

        //right's movement
        if (horizontalMove > 0)
        {
            anim.SetTrigger("Derecha");
        }
        //left's movement
        else if (horizontalMove < 0)
        {
            anim.SetTrigger("Izquierda");
        }
        else if (verticalMove<0)
        {
            anim.SetTrigger("Abajo");
        }
        else if (verticalMove>0)
        {
            anim.SetTrigger("Arriba");
        }
        else
        {
            anim.SetTrigger("Quieto");
        }

        if (Input.GetKey("space") && CheckGround.isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }

    }

    public void Jump()
    {
        //rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        if (CheckGround.isGrounded)
        {
            Debug.Log("IsGrounded");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
    }

    private void FixedUpdate()
    {
        horizontalMove = joystick.Horizontal * speed;
        verticalMove = joystick.Vertical * speed;

        transform.position += new Vector3(horizontalMove, verticalMove, 0) * Time.deltaTime * speed;
    }
}
