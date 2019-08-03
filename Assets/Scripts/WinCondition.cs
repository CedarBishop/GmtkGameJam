using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CircleCollider2D))]
public class WinCondition : MonoBehaviour
{
    public static event Action OnWin;
    private CircleCollider2D circleCollider2D;

    void Start ()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.isTrigger = true;
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>())
        {
            print("Triggered with player");
            if (OnWin != null)
            {
                OnWin();
            }
        }
    }   
}
