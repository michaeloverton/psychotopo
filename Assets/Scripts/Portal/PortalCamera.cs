using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        // transform.position = portal.position - playerOffsetFromPortal;

        // Hack: Force Y to be correct because of jumping.
        transform.position = new Vector3(
            portal.position.x - playerOffsetFromPortal.x, 
            portal.position.y + playerOffsetFromPortal.y, 
            portal.position.z - playerOffsetFromPortal.z
        );

        float angularDiffBetweenPortalRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDiffBetweenPortalRotation, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }
}
