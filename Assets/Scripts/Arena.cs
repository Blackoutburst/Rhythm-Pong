using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    private float time = 0;
    public float height = 0.1f;
    public float phase = 0.0f;
    public float glow = 0.02f;

    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += 0.1f;

        RestoreValue();
        SetMaterialValue();
    }

    // Set actual value inside the material
    void SetMaterialValue()
    {
        rend.material.SetFloat("_Times", time);
        rend.material.SetFloat("_Height", height);
        rend.material.SetFloat("_Phase", phase);
        rend.material.SetFloat("_Glow", glow);
    }

    // Set back material value to their default state 
    void RestoreValue()
    {
        if (height > 0.0)
            height -= 0.1f;
        if (phase > 0)
            phase -= 1.0f;
        if (glow > 0.03f)
            glow -= 0.01f;
    }
}
