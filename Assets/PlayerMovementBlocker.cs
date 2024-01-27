using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBlocker : MonoBehaviour
{
    [Tooltip("If false then direction to left")]
    [SerializeField] private bool directionToRight;
    [SerializeField] private bool ableToMove;

    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        SetAbleToMove(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == "InvisibleWall")
        {
            SetAbleToMove(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InvisibleWall")
        {
            SetAbleToMove(true);
        }
    }

    private void SetAbleToMove(bool value)
    {
        ableToMove = value;
        if(directionToRight)
        {
            playerMovement.UpdateAbleToMoveRight(value);
        } else
        {
            playerMovement.UpdateAbleToMoveLeft(value);
        }
    }
}
