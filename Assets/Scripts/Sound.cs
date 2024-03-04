using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Sound : MonoBehaviour
{
    [SerializeField] private EventReference flashLight;
    [SerializeField] private EventReference pickUpStick;
    [SerializeField] private EventReference dropStick;

    [SerializeField] private EventReference footsteps;
    private EventInstance footstepInstance;


    [SerializeField] private EventReference ambience;
    private EventInstance ambienceInstance;

    public void FlashLight()
    {
        Debug.Log("SFX: flashing light");
        RuntimeManager.PlayOneShot(flashLight, transform.position);
    }
    public void PickupStick()
    {
        Debug.Log("SFX: picking up stick");
        RuntimeManager.PlayOneShot(pickUpStick, transform.position);
    }
    public void DropStick()
    {
        Debug.Log("SFX: dropping stick");
        RuntimeManager.PlayOneShot(dropStick, transform.position);
    }

    void OnEnable()
    {
        footstepInstance = RuntimeManager.CreateInstance(footsteps);
        footstepInstance.start();

        ambienceInstance = RuntimeManager.CreateInstance(ambience);
        RuntimeManager.AttachInstanceToGameObject(ambienceInstance, transform);
        ambienceInstance.start();
    }
    void OnDisable()
    {
        footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        footstepInstance.release();

        ambienceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambienceInstance.release();
    }
    void FixedUpdate()
    {
        // Dynamic Footsteps Method:
        footstepInstance.setParameterByName("currentSpeed", Manager.Instance.currentSpeed);
        footstepInstance.setParameterByName("currentGround", (int) Manager.Instance.currentGround);

        // Start / Stop Method:
        //bool isMoving = (Manager.Instance.currentSpeed > 0);
        //FMOD.Studio.PLAYBACK_STATE playbackState;
        //footstepInstance.getPlaybackState(out playbackState);
        //bool soundStopped = (playbackState == PLAYBACK_STATE.STOPPED);

        //if (isMoving && soundStopped)
        //{
        //    footstepInstance.start();
        //}
        //else if (!isMoving && !soundStopped)
        //{
        //    footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //}

        // Pause/Unpause Method:
        //bool isMoving = (Manager.Instance.currentSpeed > 0);
        //bool isPaused;
        //footstepInstance.getPaused(out isPaused);

        //if (isMoving && isPaused)
        //{
        //    footstepInstance.setPaused(false);
        //}
        //else if (!isMoving && !isPaused)
        //{
        //    footstepInstance.setPaused(true);
        //}
    }

    // Reverb Zones:
    public void EnterCave()
    {
        Debug.Log("SFX: enter cave");
        ambienceInstance.setParameterByName("location", 1); // "cave"
    }
    public void ExitCave()
    {
        Debug.Log("SFX: exit cave");
        ambienceInstance.setParameterByName("location", 0); // "field"
    }
}
