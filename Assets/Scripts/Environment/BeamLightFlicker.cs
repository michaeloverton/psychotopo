using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLightFlicker : MonoBehaviour
{
    private Material lightMaterial;
    [SerializeField] private float flickerAmount = 0.3f;
    [SerializeField] private Color color;

    void Start() {
        lightMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0f, 1.0f) > flickerAmount) 
        {
            lightMaterial.SetColor("_Color", color);
        } 
        else 
        {
            lightMaterial.SetColor("_Color", Color.black);
        }
    }
}
