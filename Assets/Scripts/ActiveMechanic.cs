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
            if (currentMechanic == CurrentMechanic.Gate && value != CurrentMechanic.Gate)
            {
                AudioManager.instance.Play("Close Gate");
            }
            currentMechanic = value;
            switch (currentMechanic)
            {
                case CurrentMechanic.None:
                    ActivateGates(false);
                    ActivateTeleporters(false);
                    ActivateBlocks(false);
                    break;
                case CurrentMechanic.Gate:
                    ActivateGates(true);
                    ActivateTeleporters(false);
                    ActivateBlocks(false);
                    AudioManager.instance.Play("Gate Open");
                    break;
                case CurrentMechanic.Teleporter:
                    ActivateGates(false);
                    ActivateTeleporters(true);
                    ActivateBlocks(false);
                    break;
                case CurrentMechanic.Blocks:
                    ActivateGates(false);
                    ActivateTeleporters(false);
                    ActivateBlocks(true);
                    break;
                default:
                    break;
            }
        }
    }

    private Gate[] gates;
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

    void Start ()
    {
        gates = FindObjectsOfType<Gate>();
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

    void ActivateGates(bool answer)
    {
        foreach (Gate gate in gates)
        {
            gate._IsOpen = answer;
        }
    }
}
