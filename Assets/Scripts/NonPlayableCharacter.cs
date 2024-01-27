using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayableCharacter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Update()
    {
        UpdateSpriteOrientation();
    }

    private void UpdateSpriteOrientation()
    {
        var signedXDistance = player.gameObject.transform.position.x - transform.position.x;
        if(signedXDistance >= 0)
        {
            spriteRenderer.flipX = true;
        } else
        {
            spriteRenderer.flipX = false;
        }
    }
}
