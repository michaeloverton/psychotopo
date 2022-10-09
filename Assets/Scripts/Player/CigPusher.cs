using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigPusher : MonoBehaviour
{
    [SerializeField] Transform cig;
    [SerializeField] float detectionDistance = .1f;
    [SerializeField] float cigPushbackDistance = .1f;
    private Vector3 initialPosition;
    private bool wasHitting = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = cig.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, detectionDistance))
        {
            if(!wasHitting) {
                cig.transform.localPosition = new Vector3(cig.transform.localPosition.x, cig.transform.localPosition.y, cig.transform.localPosition.z - cigPushbackDistance);
                wasHitting = true;
            }
        }
        else
        {
            if(wasHitting)
            {
                cig.transform.localPosition = initialPosition;
                wasHitting = false;
            }
            
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * detectionDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
