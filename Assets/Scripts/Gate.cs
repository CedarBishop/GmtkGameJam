using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public float angle = 90;

    private BoxCollider2D collider;
    private SpriteRenderer sprite;
    private bool isOpen;
    public bool _IsOpen
    {
        get { return isOpen; }
        set
        {            
            if (value == true)
            {
                if (isOpen == false)
                {
                    open();
                }
            }
            else
            {
                if (isOpen)
                {
                    close();
                }
            }
            isOpen = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponentInParent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider.enabled = true;
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
            transform.parent.eulerAngles.z + angle
        );
            //.z += 90;
        collider.enabled = false;
    }

    public void close()
    {
        transform.parent.eulerAngles = new Vector3(
            transform.parent.eulerAngles.x,
            transform.parent.eulerAngles.y,
            transform.parent.eulerAngles.z - angle
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
