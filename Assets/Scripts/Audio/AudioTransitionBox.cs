using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AudioTransitionBox : MonoBehaviour
{
    private BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }
    
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("thihng");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(Time.frameCount);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("thihng");
    }
}
