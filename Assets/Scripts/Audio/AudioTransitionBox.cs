using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AudioTransitionBox : MonoBehaviour
{
    [SerializeField] AmbienceManager ambienceManager;
    [SerializeField] string parameterName;
    [SerializeField] bool invert;
    private BoxCollider collider;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        minZ = transform.position.z - collider.size.z/2;
        maxZ = transform.position.z + collider.size.z/2;
    }

    void OnTriggerStay(Collider other)
    {
        float paramValue = Utility.Remap(other.transform.position.z, minZ, maxZ, 0, 1);
        if(invert) paramValue = Utility.Remap(other.transform.position.z, minZ, maxZ, 1, 0);
        ambienceManager.SetMusicParameter(parameterName, paramValue);
    }
}
