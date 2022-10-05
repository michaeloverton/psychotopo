using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindChangeTrigger : MonoBehaviour
{
    [SerializeField] private MindManager mindManager;
    [SerializeField] private float mindModAmount;
    [SerializeField] private bool positiveChange;

    void OnTriggerStay(Collider other)
    {
        if(positiveChange) mindManager.gainMind(mindModAmount);
        else mindManager.loseMind(mindModAmount);
    }
}
