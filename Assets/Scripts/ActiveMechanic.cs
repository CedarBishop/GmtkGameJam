using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentMechanic {None, Gate, Teleporter, Blocks }

public class ActiveMechanic : MonoBehaviour
{
    public static ActiveMechanic instance = null;
    public CurrentMechanic currentMechanic;


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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
