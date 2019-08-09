using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public delegate void SendText(string sentence);
    public static event SendText textSent;

    public float movementSpeed = 100;
    public float pushForce = 200;
    public LayerMask layerMask;
    public float boxCastOffset = 1;
    public Vector2 boxCastSize = new Vector2(1,1);

    private Vector2 direction;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private Vector2 boxCastOrigin;

    enum CurrentDirection {Down, Left, Up, Right}
    CurrentDirection currentDirection;
    CurrentDirection directionWhenGrabbed;
    CurrentDirection origialDirection;
    private bool isGrabbing;
    private bool canPushPull;
    private bool isTriggeringTeleporter;
    private KeyCode lastPressedKey;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        playerRigidbody.gravityScale = 0;
    }

    void Update()
    {
        if (isGrabbing)
        {
            PushAndPull();
        }
        else if (Dialogue.dialogueHasStarted == false)
        {
            Movement();
            GetDirection();
            RepositionBoxCast();
            DetectInteractables();
        }       
    }

    void OnTriggerStay2D (Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Teleporter>())
        {
            isTriggeringTeleporter = true;
            if (textSent != null && ActiveMechanic.instance._CurrentMechanic == CurrentMechanic.Teleporter)
            {
                textSent("Press E To Activate Teleporter");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                collider.gameObject.GetComponent<Teleporter>().TeleportPlayer(gameObject);
            }            
        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Teleporter>())
        {
            isTriggeringTeleporter = false;
        }
    }

    void Movement ()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        animator.SetBool("isWalking",(Mathf.Abs(direction.x) > 0.1f || Mathf.Abs(direction.y) > 0.1f) ? true : false);
        playerRigidbody.velocity = new Vector2(direction.x, direction.y) * movementSpeed * Time.deltaTime;
    }

    void GetDirection()
    {
        origialDirection = currentDirection;
        if (direction.y > 0.5f)
        {
            currentDirection = CurrentDirection.Up;
        }
        if (direction.x > 0.5f)
        {
            currentDirection = CurrentDirection.Right;
        }
        if (direction.y < -0.5f)
        {
            currentDirection = CurrentDirection.Down;
        }
        if (direction.x < -0.5f)
        {
            currentDirection = CurrentDirection.Left;           
        }
        if (origialDirection != currentDirection)
        {
            animator.SetBool("up", (currentDirection == CurrentDirection.Up));
            animator.SetBool("right", (currentDirection == CurrentDirection.Right));
            animator.SetBool("down", (currentDirection == CurrentDirection.Down));
            animator.SetBool("left", (currentDirection == CurrentDirection.Left));
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
                if (isTriggeringTeleporter == false)
                    textSent("Press E to Activate Switch");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    collider.GetComponent<Switch>().SwitchMechanic();
                }                
            }
            else if (collider.GetComponent<SlidingBlock>())
            {
                if (collider.gameObject.GetComponent<SlidingBlock>().IsMovable)
                {
                    if (isTriggeringTeleporter == false && ActiveMechanic.instance._CurrentMechanic == CurrentMechanic.Blocks)
                        textSent("Press E To Grab The Crate");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ResetVelocities();
                        collider.GetComponent<SlidingBlock>().GrabBlock(transform);
                        isGrabbing = true;
                        directionWhenGrabbed = currentDirection;
                        canPushPull = true;
                        textSent("Press E To Let Go Of The Crate");
                    }
                }                               
            }
            else if (collider.GetComponent<Npc>())
            {
                if (Dialogue.dialogueHasStarted == false)
                {
                    if (isTriggeringTeleporter == false)
                        textSent("Press E To Talk To NPC");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        collider.gameObject.GetComponent<Npc>().InteractNpc();
                    }
                }                
            }
        }
        else
        {
            if (isTriggeringTeleporter == false)
                textSent("");
        }
    }


    void PushAndPull ()
    {
        if (canPushPull)
        {
            if (directionWhenGrabbed == CurrentDirection.Left || directionWhenGrabbed == CurrentDirection.Right)
            {
                direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
                playerRigidbody.AddForce(direction * pushForce);
                StartCoroutine("CoPushAndPull");        
            }
            else if (directionWhenGrabbed == CurrentDirection.Up || directionWhenGrabbed == CurrentDirection.Down)
            {
                direction = new Vector2(0, Input.GetAxisRaw("Vertical"));
                playerRigidbody.AddForce(direction * pushForce);
                StartCoroutine("CoPushAndPull");
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                LetGoOfBlock();
            }
        }        
    }

    IEnumerator CoPushAndPull ()
    {
        AudioManager.instance.Play("Move Crate");
        canPushPull = false;
        while (Mathf.Abs( playerRigidbody.velocity.x) > 0 && Mathf.Abs(playerRigidbody.velocity.y) > 0)
        {
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

    public void PlayFootstep ()
    {
        AudioManager.instance.Play("Footstep");
    }
}