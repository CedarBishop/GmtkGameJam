using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Teleporter : MonoBehaviour
{
    public Teleporter partnerTeleporter;
    public Color activatedColor;

    private SpriteRenderer spriteRenderer;
    private bool isActivated;
    public bool IsActivated
    {
        get { return isActivated; }
        set
        {
            isActivated = value;
            spriteRenderer.color = (isActivated) ? activatedColor : Color.white;
        }
    }

    void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TeleportPlayer (GameObject player)
    {
        player.transform.position = partnerTeleporter.transform.position + Vector3.right;
    }
}
