using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigPusher : MonoBehaviour
{
    [SerializeField] Transform cig;
    [SerializeField] float detectionDistance = .1f; // How far BEYOND our body we want to detect.
    private float totalDetectionDistance; // How far TOTAL from the origin of our transform we will detect.
    [SerializeField] float cigPushbackDistance = .1f;
    private float totalCharWidth;
    private Vector3 initialPosition;
    private bool wasHitting = false;
    [SerializeField] float sphereCastRadius = .2f;
    private float resetTimer = 0;
    [SerializeField] float resetTime = 0.6f;
    [SerializeField] LayerMask collisionLayers;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = cig.transform.localPosition;
        CharacterController character = GetComponent<CharacterController>();
        totalCharWidth = character.radius + character.skinWidth;
        totalDetectionDistance = totalCharWidth + detectionDistance;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        // if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, totalDetectionDistance))
        if(Physics.SphereCast(transform.position, sphereCastRadius, transform.TransformDirection(Vector3.forward), out hit, totalDetectionDistance, collisionLayers))
        {
            // Debug.Log(hit.distance);
            // Debug.Log(hit.distance - totalCharWidth);
            // float pushAmount = detectionDistance / Mathf.Clamp(hit.distance - totalCharWidth, 0, 1);
            float pushAmount = Utility.Remap(Mathf.Clamp(hit.distance - totalCharWidth, 0, 1), 0, detectionDistance, 1, 0);
            // Debug.Log(pushAmount);
            // Debug.Log(hit.distance / totalDetectionDistance);
            // if(!wasHitting) {
                cig.transform.localPosition = new Vector3(cig.transform.localPosition.x, cig.transform.localPosition.y, initialPosition.z - (pushAmount * cigPushbackDistance));
                wasHitting = true;
            // }
        }
        else
        {
            if(wasHitting)
            {
                // float t = Utility.Remap(resetTimer, 0, resetTime, 0, 1);
                cig.transform.localPosition = Vector3.Lerp(cig.transform.localPosition, initialPosition, resetTimer);
                resetTimer += Time.deltaTime;
                

                // cig.transform.localPosition = initialPosition;
                if(resetTimer > 1) {
                    wasHitting = false;
                    resetTimer = 0;
                }
               
            }
            
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * totalDetectionDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
