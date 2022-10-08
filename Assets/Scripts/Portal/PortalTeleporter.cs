using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PortalTeleporter : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        if(playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, portalToPlayer);

            if(dotProduct > 0f)
            {
                // float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                float rotationDiff = -1*(transform.rotation.eulerAngles.y - receiver.rotation.eulerAngles.y);
                // rotationDiff += 180;
                // player.Rotate(Vector3.up, rotationDiff);

                player.GetComponent<FirstPersonController>().RotateCharacterAdditional(0, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;
            }

            // Only allow one teleportation attempt per trigger.
            playerIsOverlapping = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
