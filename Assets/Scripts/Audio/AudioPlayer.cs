using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

#pragma warning disable 618, 649
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] private string footstepEventName;
        EventInstance footstepEventInstance;
        [SerializeField] private string jumpEventName;
        EventInstance jumpEventInstance;
        [SerializeField] private string landEventName;
        EventInstance landEventInstance;

        void Start()
        {
            footstepEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + footstepEventName);
            jumpEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + jumpEventName);
            landEventInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + landEventName);
        }

        public void PlayFootstep()
        {
            footstepEventInstance.start();
        }

        public void PlayJump()
        {
            jumpEventInstance.start();
        }

        public void PlayLand()
        {
            landEventInstance.start();
        }
    }
}
