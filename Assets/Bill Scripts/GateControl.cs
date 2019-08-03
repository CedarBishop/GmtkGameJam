using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{ 
    private BoxCollider2D collider;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider.enabled = true;
        sprite.enabled = true;
        //close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void open()
    {
        transform.parent.eulerAngles = new Vector3(
            transform.parent.eulerAngles.x,
            transform.parent.eulerAngles.y,
            transform.parent.eulerAngles.z + 90
        );
            //.z += 90;
        collider.enabled = false;
    }

    public void close()
    {
        transform.parent.eulerAngles = new Vector3(
            transform.parent.eulerAngles.x,
            transform.parent.eulerAngles.y,
            transform.parent.eulerAngles.z - 90
        );
        collider.enabled = true;
    }

    public void flip()
    {
        if (collider.enabled)
        {
            open();
        }
        else
        {
            close();
        }
    }
}
