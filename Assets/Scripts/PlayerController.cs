using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 100;
    public float pushTime = 0.1f;
    public LayerMask layerMask;
    public float boxCastOffset = 1;
    public Vector2 boxCastSize = new Vector2(1,1);

    private Vector2 direction;
    private Rigidbody2D playerRigidbody;
    private Vector2 boxCastOrigin;

    enum CurrentDirection {Down, Left, Up, Right}
    CurrentDirection currentDirection;
    CurrentDirection directionWhenGrabbed;
    private bool isGrabbing;
    private bool canPushPull;
    private float inverseMoveTime;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.gravityScale = 0;
        inverseMoveTime = 1.0f / pushTime;
        SlidingBlock.blockDropped += LetGoOfBlock;
    }

    void OnDestroy ()
    {
        SlidingBlock.blockDropped -= LetGoOfBlock;
    }

    void Update()
    {
        if (isGrabbing)
        {
            PushAndPull();
        }
        else
        {
            Movement();
            GetDirection();
            RepositionBoxCast();
            DetectInteractables();
        }       
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
            // up facing animtion here
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDirection = CurrentDirection.Right;
            // right facing animtion here
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDirection = CurrentDirection.Down;
            // down facing animtion here
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDirection = CurrentDirection.Left;
            // left facing animtion here
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
                if (collider.gameObject.GetComponent<SlidingBlock>().IsMovable)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ResetVelocities();
                        collider.GetComponent<SlidingBlock>().GrabBlock(transform);
                        isGrabbing = true;
                        directionWhenGrabbed = currentDirection;
                        canPushPull = true;
                    }
                }                               
            }
        }
    }


    void PushAndPull ()
    {
        if (canPushPull)
        {
            if (directionWhenGrabbed == CurrentDirection.Left || directionWhenGrabbed == CurrentDirection.Right)
            {
                direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                if (direction.x > 0)
                {
                    StartCoroutine(CoPushAndPull(transform.position + Vector3.right));
                }
                else if (direction.x < 0)
                {
                    StartCoroutine(CoPushAndPull(transform.position + Vector3.left));
                }               
            }
            else if (directionWhenGrabbed == CurrentDirection.Up || directionWhenGrabbed == CurrentDirection.Down)
            {
                direction = new Vector2(0, Input.GetAxisRaw("Vertical"));
                if (direction.y > 0)
                {
                    StartCoroutine(CoPushAndPull(transform.position + Vector3.up));
                }
                else if (direction.y < 0)
                {
                    StartCoroutine(CoPushAndPull(transform.position + Vector3.down));
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                LetGoOfBlock();
            }
        }        
    }

    IEnumerator CoPushAndPull (Vector3 end)
    {
        canPushPull = false;
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > 0.01f)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(playerRigidbody.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            playerRigidbody.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        ResetVelocities();
        
        canPushPull = true;
        print("ended coroutine");
    }

    void ResetVelocities ()
    {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = 0;
    }

    void LetGoOfBlock ()
    {
        isGrabbing = false;
        transform.DetachChildren();
        canPushPull = false;
        StopAllCoroutines();       
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCastOrigin,boxCastSize);
    }
}
