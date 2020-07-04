using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Ball : MonoBehaviour
{


    public Vector2 position;
    public Vector2 velocity;

    public ParticleSystem TopLine;
    public ParticleSystem BottomLine;
    public ParticleSystem OrangeLine;
    public ParticleSystem BlueLine;

    public TextAsset timingFile;

    public float grav = 0.05f;
    public float multiplier;
    public float speed = 0.01f;
    private float timing1;
    private float timing2;

    public int player = 1;
    private int pos = 0;

    Arena arena;
    GhostCalcul ghost;

    string[] lines;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gb = GameObject.Find("Ghost");
        ghost = gb.GetComponent<GhostCalcul>();

        GameObject ar = GameObject.Find("arena");
        arena = ar.GetComponent<Arena>();

        string file = timingFile.text;
        lines = file.Split('\n');
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsEnd())
            return;
        for (int i = 0; i < 1000; i++)
        {
            BallDirection();
            TopBottomCollision();
            MiddleGravity();
            Move();

            if ((player == 2 && position.x <= -7.65))
                PlayerCollision(2, 1);

            if ((player == 1 && position.x >= 7.65))
                PlayerCollision(1, 2);
        }
    }

    // Check song end
    bool IsEnd()
    {
        try
        {
            float.Parse(lines[pos + 1]);
        }
        catch (Exception)
        {
            return true;
        }
        return false;
    }

    // Check collision with player
    void PlayerCollision(int actualPlayer, int futurePlayer)
    {
        if (player == actualPlayer)
        {
            player = futurePlayer;
            UpdateValue();
            OrangeLine.Play();
            BlueLine.Play();
        }
        velocity.y = (this.transform.position.y - GameObject.Find("player"+player).transform.position.y) / 5;
        if (player == 1)
        {
            speed = (Mathf.Abs(position.x - 7.65f) / Mathf.Abs(timing2 - timing1)) * multiplier;
            velocity.x = speed;
        }
        else
        {
            speed = (Mathf.Abs(position.x + 7.65f) / Mathf.Abs(timing2 - timing1)) * multiplier;
            velocity.x = -speed;
        }
        ghost.velocity = velocity;
        ghost.position = position;
    }

    // Update timing and arena value
    void UpdateValue()
    {
        timing1 = float.Parse(lines[pos]);
        timing2 = float.Parse(lines[pos + 1]);
        pos++;
        arena.height = 1.2f;
        arena.phase = 20;
        arena.glow = 0.1f;
    }

    // Move the ball
    void Move()
    {
        position.x += velocity.x / 1000;
        position.y += velocity.y / 1000;
        transform.position = new Vector3(position.x, position.y, 0.0f);
    }

    // Check collision with the top an bottom of the screen
    void TopBottomCollision()
    {
        if (position.y > 5)
        {
            TopLine.Play();
            position.y = 5;
            velocity.y = -velocity.y - speed / 2;
        }
        else if (position.y < -5)
        {
            BottomLine.Play();
            position.y = -5;
            velocity.y = -velocity.y + speed / 2;
        }
    }

    // Control ball direction
    void BallDirection()
    {
        if (player == 1)
            velocity.x = speed;
        else
            velocity.x = -speed;
    }

    // Simulate a center of gravity in the middle of the screen
    void MiddleGravity()
    {
        if (position.y < 0)
            velocity.y += grav;
        else
            velocity.y -= grav;
    }
}
