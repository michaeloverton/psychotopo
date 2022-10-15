using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbberationChangeTrigger : MonoBehaviour
{
    [SerializeField] private MindManager mindManager;
    [SerializeField] [Range(0, 1)] float abberation;

    void OnTriggerEnter(Collider other)
    {
        mindManager.SetAbberation(abberation);
    }
}
