using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class SlidingBlock : MonoBehaviour
{
   // public static event Action blockDropped;
    private SpriteRenderer spriteRenderer;
    private bool isMovable;
    public bool IsMovable
    {
        get { return isMovable; }
        set
        {
            isMovable = value;
            spriteRenderer.color = (isMovable) ? Color.blue : Color.white;
        }
    }

    void Start ()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void GrabBlock (Transform playerTransform)
    {
        transform.parent = playerTransform;
    }
}
