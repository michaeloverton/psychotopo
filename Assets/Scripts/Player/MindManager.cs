using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MindManager : MonoBehaviour
{
    [SerializeField] PauseManager pauseManager;
    [SerializeField] AudioManager audioManager;
    [SerializeField] bool activate;
    [SerializeField] float maxMind = 100f;
    private float currentMind;
    [SerializeField] float smokingMindAmount = 5f;
    [SerializeField] float partyingMindAmount = 2f;
    [SerializeField] GameObject mindMeter;
    

    // Chem
    [SerializeField] GameObject chemMeter;
    private bool tookChem = false;
    private bool onChem = false;
    [SerializeField] float maxChem = 100f;
    private float currentChem = 0;
    [SerializeField] float chemDecay = 5f;
    [SerializeField] float chemBuild = 30f;

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
    ColorGrading colorGrading;
    [SerializeField] [Range(-100f, 100f)] float maxTemp = -100f;
    [SerializeField] [Range(-100f, 100f)] float maxTint = 100f;
    [SerializeField] [Range(-50f, 50f)] float maxPostExposure = 2.7f;
    [SerializeField] [Range(-180f, 180f)] float maxHueShift = 103f;
    [SerializeField] [Range(-100f, 100f)] float maxSaturation = 6.3f;

    // Start is called before the first frame update
    void Start()
    {
        currentMind = maxMind;
        chemMeter.SetActive(false);

        abberation = ScriptableObject.CreateInstance<ChromaticAberration>();
        abberation.intensity.Override(minAbberation);
        abberation.enabled.Override(true);

        lensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
        lensDistortion.enabled.Override(true);
        lensDistortion.intensity.Override(0f);

        bloom = ScriptableObject.CreateInstance<Bloom>();
        bloom.enabled.Override(true);
        bloom.intensity.Override(minBloom);

        colorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        colorGrading.enabled.Override(true);
        colorGrading.temperature.Override(0);
        colorGrading.tint.Override(0);
        colorGrading.postExposure.Override(0);
        colorGrading.hueShift.Override(0);
        colorGrading.saturation.Override(0);

        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        processVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, abberation, lensDistortion, bloom, colorGrading);
    }

    void Update()
    {
        if(pauseManager.GetPaused()) return;

        // Tookchem 
        if(tookChem) 
        {
            currentChem = Mathf.Clamp(currentChem + (chemBuild * Time.deltaTime), 0, maxChem);
            adjustChemMeter();
            adjustProcessing();
            adjustAudio();

            if(currentChem == maxChem)
            {
                onChem = true;
                tookChem = false;
            }
        }

        if(onChem) 
        {
            currentChem = Mathf.Clamp(currentChem - (chemDecay * Time.deltaTime), 0, maxChem);
            adjustChemMeter();
            adjustProcessing();
            adjustAudio();

            if(currentChem == 0)
            {
                onChem = false;
                chemMeter.SetActive(false);
            }
        }
    }

    public void loseMind(float loseAmount)
    {
        if(activate && !pauseManager.GetPaused())
        {
            currentMind = Mathf.Clamp(currentMind - (loseAmount * Time.deltaTime), 0, maxMind);
            adjustMindMeter();
            adjustProcessing();
        }
    }

    public void gainMind(float gainAmount)
    {
        if(activate && !pauseManager.GetPaused())
        {
            currentMind = Mathf.Clamp(currentMind + (gainAmount * Time.deltaTime), 0, maxMind);
            adjustMindMeter();
            adjustProcessing();
        }
    }

    public float getSmokingMindAmount()
    {
        return smokingMindAmount;
    }

    private void adjustMindMeter()
    {
        float newMeterYScale = Utility.Remap(currentMind, 0, maxMind, 0, 1);
        Vector3 currentMeterScale = mindMeter.transform.localScale;
        mindMeter.transform.localScale = new Vector3(currentMeterScale.x, newMeterYScale, currentMeterScale.z);
    }

    private void adjustChemMeter()
    {
        float newMeterYScale = Utility.Remap(currentChem, 0, maxChem, 0, 1);
        Vector3 currentMeterScale = chemMeter.transform.localScale;
        chemMeter.transform.localScale = new Vector3(currentMeterScale.x, newMeterYScale, currentMeterScale.z);
    }

    private void adjustAudio()
    {
        float normalizedChemValue = Utility.Remap(currentChem, 0, maxChem, 0, 1);
        audioManager.SetMasterPhaser(normalizedChemValue);
    }

    public void TakeChem()
    {
        tookChem = true;
        chemMeter.SetActive(true);
    }

    public void SetAbberation(float val)
    {
        abberation.intensity.Override(val);
    }

    private void adjustProcessing()
    {
        float newAbberation = Utility.Remap(currentMind, maxMind, 0, minAbberation, maxAbberation);
        abberation.intensity.Override(newAbberation);
        float newDistortion = Utility.Remap(currentMind, maxMind, 0, 0, maxLensDistortion);
        lensDistortion.intensity.Override(newDistortion);
        
        // For bloom, use the bigger of the mind or chem bloom.
        float mindBloom = Utility.Remap(currentMind, maxMind, 0, minBloom, maxBloom);
        float chemBloom = Utility.Remap(currentChem, 0, maxChem, minBloom, maxBloom);
        bloom.intensity.Override(mindBloom > chemBloom ? mindBloom : chemBloom);

        if(tookChem || onChem)
        {
            float newTemp = Utility.Remap(currentChem, 0, maxChem, 0, maxTemp);
            colorGrading.temperature.Override(newTemp);
            float newTint = Utility.Remap(currentChem, 0, maxChem, 0, maxTint);
            colorGrading.tint.Override(newTint);
            float newPostExposure = Utility.Remap(currentChem, 0, maxChem, 0, maxPostExposure);
            colorGrading.postExposure.Override(newPostExposure);
            float newHue = Utility.Remap(currentChem, 0, maxChem, 0, maxHueShift);
            colorGrading.hueShift.Override(newHue);
            float newSaturation = Utility.Remap(currentChem, 0, maxChem, 0, maxSaturation);
            colorGrading.saturation.Override(newSaturation);

            
            
        }
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(processVolume, true, true);
    }
}
