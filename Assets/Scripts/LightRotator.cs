using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotator : MonoBehaviour
{
    private Quaternion from;
    [SerializeField] Quaternion to;
    [SerializeField] float speed = 0.01f;
    private float timeCount = 0.0f;
    [SerializeField] float switchDistance = .01f;
    private bool backwards = false;

    // Start is called before the first frame update
    void Start()
    {
        from = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(!backwards) 
        {
            transform.rotation = Quaternion.Lerp(from, to, timeCount * speed);
            if(Quaternion.Angle(transform.rotation, to) < switchDistance) 
            {
                backwards = true;
            }
        }
        else 
        {
            transform.rotation = Quaternion.Lerp(to, from, timeCount * speed);
            if(Quaternion.Angle(transform.rotation, from) < switchDistance) 
            {
                backwards = false;
            }
        }

        timeCount = timeCount + Time.deltaTime;

        
    }
}
