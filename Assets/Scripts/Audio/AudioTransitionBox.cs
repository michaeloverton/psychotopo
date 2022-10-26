using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AudioTransitionBox : MonoBehaviour
{
    [SerializeField] AmbienceManager ambienceManager;
    [SerializeField] string parameterName;
    [SerializeField] float initialValue = 0f;
    [SerializeField] float maxValue = 1f;
    [SerializeField] bool invert;
    [SerializeField] bool modulateCrowd;
    [SerializeField] bool modulateTraffic;
    [SerializeField] bool modulateBirds;
    [SerializeField] bool modulateMusic;
    [SerializeField] bool xModulation;
    [SerializeField] bool yModulation;
    private BoxCollider collider;
    private float minZ;
    private float maxZ;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        minZ = transform.position.z - collider.size.z/2;
        maxZ = transform.position.z + collider.size.z/2;

        minX = transform.position.x - collider.size.x/2;
        maxX = transform.position.x + collider.size.x/2;

        minY = transform.position.y - collider.size.y/2;
        maxY = transform.position.y + collider.size.y/2;
    }

    void OnTriggerStay(Collider other)
    {
        float paramValue;
        if(xModulation) {
            paramValue = Utility.Remap(other.transform.position.x, minX, maxX, initialValue, maxValue);
            if(invert) paramValue = Utility.Remap(other.transform.position.x, minX, maxX, maxValue, initialValue);
        }
        else if(yModulation)
        {
            paramValue = Utility.Remap(other.transform.position.y, minY, maxY, initialValue, maxValue);
            if(invert) paramValue = Utility.Remap(other.transform.position.y, minY, maxY, maxValue, initialValue);
        }
        else
        {
            paramValue = Utility.Remap(other.transform.position.z, minZ, maxZ, initialValue, maxValue);
            if(invert) paramValue = Utility.Remap(other.transform.position.z, minZ, maxZ, maxValue, initialValue);
        }
        
        if(modulateCrowd) ambienceManager.SetCrowdParameter(parameterName, paramValue);
        if(modulateMusic) ambienceManager.SetMusicParameter(parameterName, paramValue);
        if(modulateTraffic) ambienceManager.SetTrafficParameter(parameterName, paramValue);
        if(modulateBirds) ambienceManager.SetBirdsParameter(parameterName, paramValue);
    }
}
