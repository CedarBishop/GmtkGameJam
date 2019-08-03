using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 100;
    public LayerMask layerMask;
    public float boxCastOffset = 1;
    public Vector2 boxCastSize = new Vector2(1,1);
    private Vector2 direction;
    private Rigidbody2D playerRigidbody;
    private Vector2 boxCastOrigin;

    enum CurrentDirection {Down, Left, Up, Right}
    CurrentDirection currentDirection;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.gravityScale = 0;
    }

    void Update()
    {
        Movement();
        GetDirection();
        RepositionBoxCast();
        DetectInteractables();
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Teleporter>())
        {
            collider.gameObject.GetComponent<Teleporter>().TeleportPlayer(gameObject);
        }
    }

    void Movement ()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        playerRigidbody.velocity = new Vector2(direction.x, direction.y) * movementSpeed * Time.deltaTime;
    }

    void GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentDirection = CurrentDirection.Up;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDirection = CurrentDirection.Right;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDirection = CurrentDirection.Down;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDirection = CurrentDirection.Left;
        }
    }

    void RepositionBoxCast()
    {
        switch (currentDirection)
        {
            case CurrentDirection.Down:
                boxCastOrigin = transform.position + (Vector3.down * boxCastOffset);
                break;
            case CurrentDirection.Left:
                boxCastOrigin = transform.position + (Vector3.left * boxCastOffset);
                break;
            case CurrentDirection.Up:
                boxCastOrigin = transform.position + (Vector3.up * boxCastOffset);
                break;
            case CurrentDirection.Right:
                boxCastOrigin = transform.position + (Vector3.right * boxCastOffset);
                break;
            default:
                break;
        }
    }

    void DetectInteractables ()
    {
        Collider2D collider = Physics2D.OverlapBox(boxCastOrigin, boxCastSize, 0, layerMask);
        if (collider != null)
        {
            if (collider.GetComponent<Switch>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    collider.GetComponent<Switch>().SwitchMechanic();
                }                
            }
            else if (collider.GetComponent<SlidingBlock>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    collider.GetComponent<SlidingBlock>().PushBlock(boxCastOrigin / boxCastOffset);
                }                
            }
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCastOrigin,boxCastSize);
    }
}
