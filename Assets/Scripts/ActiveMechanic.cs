using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentMechanic {None, Gate, Teleporter, Blocks }

public class ActiveMechanic : MonoBehaviour
{
    public static ActiveMechanic instance = null;
   [SerializeField] private CurrentMechanic currentMechanic;
    public CurrentMechanic _CurrentMechanic
    {
        get { return currentMechanic; }
        set
        {
            currentMechanic = value;
            print(currentMechanic.ToString() + " activated");
            switch (currentMechanic)
            {
                case CurrentMechanic.None:
                    ActivateTeleporters(false);
                    ActivateBlocks(false);
                    break;
                case CurrentMechanic.Gate:
                    ActivateTeleporters(false);
                    ActivateBlocks(false);
                    break;
                case CurrentMechanic.Teleporter:
                    ActivateTeleporters(true);
                    ActivateBlocks(false);
                    break;
                case CurrentMechanic.Blocks:
                    ActivateTeleporters(false);
                    ActivateBlocks(true);
                    break;
                default:
                    break;
            }
        }
    }

    //Gate class hasnt been pushed yet so does not exist in this context

    //private Gate[] gates;
    private Teleporter[] teleporters;
    private SlidingBlock[] blocks;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void OnValidate ()
    {
        //gates = FindObjectsOfType<Gate>();
        teleporters = FindObjectsOfType<Teleporter>();
        blocks = FindObjectsOfType<SlidingBlock>();
    }

    void ActivateTeleporters (bool answer)
    {
        foreach (Teleporter teleporter in teleporters)
        {
            teleporter.IsActivated = answer;
        }
    }

    void ActivateBlocks(bool answer)
    {
        foreach (SlidingBlock block in blocks)
        {
            block.IsMovable = answer;
        }
    }
}
