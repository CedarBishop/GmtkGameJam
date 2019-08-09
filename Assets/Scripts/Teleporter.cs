using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Teleporter : MonoBehaviour
{
    public Teleporter partnerTeleporter;
    public Color activatedColor;
    public Vector2 spawnOffsetFromTeleporter;

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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TeleportPlayer (GameObject player)
    {
        if (isActivated)
        {
            AudioManager.instance.Play("Teleporter");
            player.transform.position = partnerTeleporter.transform.position + new Vector3(partnerTeleporter.spawnOffsetFromTeleporter.x, partnerTeleporter.spawnOffsetFromTeleporter.y, 0);
        }
    }
}
        
