using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTrigger : MonoBehaviour
{
    [SerializeField] bool hide;
    [SerializeField] bool hideOnExit = false;
    [SerializeField] List<GameObject> things = new List<GameObject>();

    void OnTriggerEnter(Collider other) {
        foreach(GameObject thing in things)
        {
            if(hide)
            {
                thing.SetActive(false);
            }
            else
            {
                thing.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(hideOnExit)
        {
            foreach(GameObject thing in things)
            {
                thing.SetActive(false);
            }
        }
    }
}
