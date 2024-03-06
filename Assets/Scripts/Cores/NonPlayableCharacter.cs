using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayableCharacter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Animator's Parameters")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool isMad;

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

    public void SetToMad(bool value)
    {
        isMad = value;
        animator.SetBool("isMad", isMad);
    }
}
