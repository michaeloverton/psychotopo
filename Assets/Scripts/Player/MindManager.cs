using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MindManager : MonoBehaviour
{
    [SerializeField] float maxMind = 100f;
    private float currentMind;
    [SerializeField] float smokingMindAmount = 5f;
    [SerializeField] float partyingMindAmount = 2f;
    [SerializeField] GameObject mindMeter;
    private float currentMeterScale;

    // Postprocessing
    PostProcessVolume processVolume;
    ChromaticAberration abberation;
    [SerializeField] float maxAbberation = 1f;
    [SerializeField] float minAbberation = .3f;
    LensDistortion lensDistortion;
    [SerializeField] float maxLensDistortion = 100f;
    Bloom bloom;
    [SerializeField] float maxBloom = 20f;
    [SerializeField] float minBloom = 3f;

    // Start is called before the first frame update
    void Start()
    {
        currentMind = maxMind;
        currentMeterScale = 1;

        abberation = ScriptableObject.CreateInstance<ChromaticAberration>();
        abberation.intensity.Override(minAbberation);
        abberation.enabled.Override(true);

        lensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
        lensDistortion.enabled.Override(true);
        lensDistortion.intensity.Override(0f);

        bloom = ScriptableObject.CreateInstance<Bloom>();
        bloom.enabled.Override(true);
        bloom.intensity.Override(minBloom);

        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        processVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, abberation, lensDistortion, bloom);
    }

    public void loseMind(float loseAmount)
    {
        currentMind = Mathf.Clamp(currentMind - (loseAmount * Time.deltaTime), 0, maxMind);
        adjustMeter();
        adjustProcessing();
    }

    public void gainMind(float gainAmount)
    {
        currentMind = Mathf.Clamp(currentMind + (gainAmount * Time.deltaTime), 0, maxMind);
        adjustMeter();
        adjustProcessing();
    }

    public float getSmokingMindAmount()
    {
        return smokingMindAmount;
    }

    public float getPartyingMindAmount()
    {
        return partyingMindAmount;
    }

    private void adjustMeter()
    {
        float newMeterYScale = Utility.Remap(currentMind, 0, maxMind, 0, 1);
        Vector3 currentMeterScale = mindMeter.transform.localScale;
        mindMeter.transform.localScale = new Vector3(currentMeterScale.x, newMeterYScale, currentMeterScale.z);
    }

    private void adjustProcessing()
    {
        float newAbberation = Utility.Remap(currentMind, maxMind, 0, minAbberation, maxAbberation);
        abberation.intensity.Override(newAbberation);
        float newDistortion = Utility.Remap(currentMind, maxMind, 0, 0, maxLensDistortion);
        lensDistortion.intensity.Override(newDistortion);
        float newBloom = Utility.Remap(currentMind, maxMind, 0, minBloom, maxBloom);
        bloom.intensity.Override(newBloom);
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(processVolume, true, true);
    }
}
