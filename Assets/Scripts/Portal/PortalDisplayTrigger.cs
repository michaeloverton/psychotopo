using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDisplayTrigger : MonoBehaviour
{
    [SerializeField] GameObject portal;
    [SerializeField] GameObject receiver;

    void Start()
    {
        portal.SetActive(false);
        receiver.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        portal.SetActive(true);
        receiver.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        portal.SetActive(false);
        receiver.SetActive(false);
    }
}
