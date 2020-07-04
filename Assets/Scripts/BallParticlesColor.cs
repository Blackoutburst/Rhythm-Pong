using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallParticlesColor : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem pSystem;
    ParticleSystem.MainModule mainSystem;
    int player = 1;

    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
        mainSystem = pSystem.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player = this.transform.parent.GetComponent<Ball>().player;

        if (player == 1)
            mainSystem.startColor = new Color(0, 0.6f, 1);
        else
            mainSystem.startColor = new Color(1, 0.6f, 0);
    }
}
