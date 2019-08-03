using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class SlidingBlock : MonoBehaviour
{
    private bool isMovable;
    public bool IsMovable
    {
        get { return isMovable; }
        set
        {
            isMovable = value;
            rigidbody2D.isKinematic = (isMovable) ? false : true ;
        }
    }



    private Rigidbody2D rigidbody2D;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
        rigidbody2D.isKinematic = true;
    }

    public void PushBlock (Vector2 currentPlayerDirection)
    {

    }
}
