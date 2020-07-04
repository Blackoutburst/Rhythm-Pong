using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour
{


    Renderer rend;
    TrailRenderer trailRenderer;

    int player = 1;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = this.transform.parent.GetComponent<Ball>().player;
        rend.material.SetFloat("_Size", 1 - this.transform.parent.GetComponent<Ball>().speed / 80000);

        if (player == 1)
            SetColor(0, 150, 255);
        else
            SetColor(255, 150, 0);
    }

    // Set material color
    void SetColor(float red, float green, float blue)
    {
            rend.material.SetFloat("_RColor", red);
            rend.material.SetFloat("_GColor", green);
            rend.material.SetFloat("_BColor", blue);
            trailRenderer.material.SetFloat("_RColor", red);
            trailRenderer.material.SetFloat("_GColor", green);
            trailRenderer.material.SetFloat("_BColor", blue);
    }
}
