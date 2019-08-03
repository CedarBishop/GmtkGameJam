
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public CurrentMechanic currentMechanic;

    public void SwitchMechanic ()
    {
        if (ActiveMechanic.instance._CurrentMechanic != currentMechanic)
        {
            ActiveMechanic.instance._CurrentMechanic = currentMechanic;
        }
    }
}
