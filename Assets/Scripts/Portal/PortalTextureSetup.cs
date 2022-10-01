using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    // public Camera cameraA;
    // public Material cameraMatA;
    // public Camera cameraB;
    // public Material cameraMatB;
    // public Camera cameraC;
    // public Material cameraMatC;

    [SerializeField] List<Camera> portalCameras;
    [SerializeField] List<Material> portalMaterials;

    // Start is called before the first frame update
    void Start()
    {
        // if(cameraA.targetTexture != null) 
        // {
        //     cameraA.targetTexture.Release();
        // }

        // cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        // cameraMatA.mainTexture = cameraA.targetTexture;

        for(int i=0; i< portalCameras.Count; i++) {
            Camera cam = portalCameras[i];
            Material mat = portalMaterials[i];

            if(cam.targetTexture != null) 
            {
                cam.targetTexture.Release();
            }

            cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
            mat.mainTexture = cam.targetTexture;
        }
    }
}
