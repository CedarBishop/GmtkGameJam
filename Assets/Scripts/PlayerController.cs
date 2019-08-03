using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5;

    private Vector2 direction;
    private Rigidbody2D playerRigidbody;  

    enum CurrentDirection {Left,Up,Right,Down}

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerRigidbody.gravityScale = 0;
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        playerRigidbody.velocity = new Vector2(direction.x, direction.y) * movementSpeed;
    }
}
