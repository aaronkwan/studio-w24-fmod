using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Sound : MonoBehaviour
{
    [SerializeField] private EventReference flashLight;
    [SerializeField] private EventReference pickUpStick;
    [SerializeField] private EventReference dropStick;

    // Event emitters:

    // One shots:
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

    // Reverb Zones:
    public void EnterCave()
    {
        Debug.Log("SFX: enter cave");
    }
    public void ExitCave()
    {
        Debug.Log("SFX: exit cave");
    }
}
