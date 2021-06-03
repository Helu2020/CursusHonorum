//Author: Héctor Luis De Pablo
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileButton : MonoBehaviour
{
    #region variables
    public float jumpSpeed = 3;
    private Rigidbody2D rigidBody;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    public void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        if (CheckGround.isGrounded)
        {
            Debug.Log("IsGrounded");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
    }
}
