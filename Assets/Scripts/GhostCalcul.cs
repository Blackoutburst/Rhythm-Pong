using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCalcul : MonoBehaviour
{
    public Vector2 position;
    public Vector2 velocity;

    public bool done = false;

    Ball ballH;

    void Start()
    {
        GameObject ballhitbox = GameObject.Find("BallHitBox");
        ballH = ballhitbox.GetComponent<Ball>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < 10000; i++)
        {
            BallDirection();
            TopBottomCollision();
            MiddleGravity();
            Move();
        }
    }

    // Control ball direction
    void BallDirection()
    {
        if (ballH.player == 1)
            velocity.x = ballH.speed;
        else
            velocity.x = -ballH.speed;
    }

    // Check collision with the top an bottom of the screen
    void TopBottomCollision()
    {
        if (position.y > 5)
        {
            position.y = 5;
            velocity.y = -velocity.y - ballH.speed / 2;
        }
        else if (position.y < -5)
        {
            position.y = -5;
            velocity.y = -velocity.y + ballH.speed / 2;
        }
    }

    // Simulate a center of gravity in the middle of the screen
    void MiddleGravity()
    {
        if (position.y < 0)
            velocity.y += ballH.grav;
        else
            velocity.y -= ballH.grav;
    }

    // Move the ghost ball
    void Move()
    {
        if ((ballH.player == 1 && position.x > 7.65) || (ballH.player == 2 && position.x < -7.65))
            done = true;
        else
        {
            position.x += velocity.x / 1000;
            position.y += velocity.y / 1000;
            transform.position = new Vector3(position.x, position.y, 0.0f);
        }
    }
}
