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

        float angularDiffBetweenPortalRotation = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDiffBetweenPortalRotation, Vector3.up);

        Vector3 directionalPlayerOffsetFromPortal = portalRotationalDifference * playerOffsetFromPortal;

        // HACK: For some reason, when angle is >= 180, we must add.
        if(portalRotationalDifference.eulerAngles.y >= 180) 
        {
            transform.position = new Vector3(
                portal.position.x + directionalPlayerOffsetFromPortal.x,
                portal.position.y + directionalPlayerOffsetFromPortal.y,
                portal.position.z + directionalPlayerOffsetFromPortal.z
            );
        }
        else
        {
            transform.position = new Vector3(
                portal.position.x - directionalPlayerOffsetFromPortal.x,
                portal.position.y + directionalPlayerOffsetFromPortal.y, // Always add in case of jumping.
                portal.position.z - directionalPlayerOffsetFromPortal.z
            );
        }

        Vector3 newCameraDirection = Quaternion.AngleAxis(-angularDiffBetweenPortalRotation, Vector3.up) * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }
}
