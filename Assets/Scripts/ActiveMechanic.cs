using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentMechanic {None, Gate, Teleporter, Blocks }

public class ActiveMechanic : MonoBehaviour
{
    public static ActiveMechanic instance = null;
    private CurrentMechanic currentMechanic;
    public CurrentMechanic _CurrentMechanic
    {
        get { return currentMechanic; }
        set
        {
            currentMechanic = value;
            switch (currentMechanic)
            {
                case CurrentMechanic.None:
                    break;
                case CurrentMechanic.Gate:
                    break;
                case CurrentMechanic.Teleporter:
                    break;
                case CurrentMechanic.Blocks:
                    break;
                default:
                    break;
            }
        }
    }

    //These classes dont exist yet but when they do I will uncomment it out

    //private Gate[] gates;
    //private Teleporter[] teleporters;
    //private Block[] blocks;


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
        //These classes dont exist yet but when they do I will uncomment it out

        //gates = FindObjectsOfType<Gate>();
        //teleporters = FindObjectsOfType<Teleporter>();
        //blocks = FindObjectsOfType<Block>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
