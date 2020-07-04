using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    Ball ballH;
    GhostCalcul ghost;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ballhitbox = GameObject.Find("BallHitBox");
        ballH = ballhitbox.GetComponent<Ball>();
        GameObject gb = GameObject.Find("Ghost");
        ghost = gb.GetComponent<GhostCalcul>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetBackToMid();
        GoToTheGhost();
    }

    // Move back player to middle position
    void GetBackToMid()
    {
        if (transform.position.x < -7.65 && ballH.player == 1)
        {
            if (transform.position.y > 1)
                transform.position = new Vector3(transform.position.x, transform.position.y - (ballH.speed / 3), 0.0f);
            if (transform.position.y < 2)
                transform.position = new Vector3(transform.position.x, transform.position.y + (ballH.speed / 3), 0.0f);
        }
    }

    // Move the player to the ghost ball position
    void GoToTheGhost()
    {
        if (ghost.done && ballH.player == 2)
        {
            if (transform.position.y - 1 < ghost.transform.position.y)
                transform.position = new Vector3(transform.position.x, transform.position.y + (ballH.speed / 3), 0.0f);
            if (transform.position.y + 1 > ghost.transform.position.y)
                transform.position = new Vector3(transform.position.x, transform.position.y - (ballH.speed / 3), 0.0f);
        }
    }
}
