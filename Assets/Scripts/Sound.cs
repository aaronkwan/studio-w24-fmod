using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{


    // Event emitters:

    // One shots:
    public void FlashLight()
    {
        Debug.Log("SFX: flashing light");
    }
    public void PickupStick()
    {
        Debug.Log("SFX: picking up stick");
    }
    public void DropStick()
    {
        Debug.Log("SFX: dropping stick");
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
