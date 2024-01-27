using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Key Inputs")]
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private KeyCode pauseKey;

    [Header("Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool ableToMoveLeft;
    [SerializeField] private bool ableToMoveRight;

    private void Update()
    {
        ProcessPlayerInput();
    }

    private void ProcessPlayerInput()
    {
        bool moveLeft = Input.GetKey(leftKey);
        bool moveRight = Input.GetKey(rightKey);

        // do nothing if either player not pushing any button or pushing both button
        if (moveLeft == moveRight) { return; }

        // neutralizer will stop the movement if unable to move in that direction
        if (moveLeft)
        {
            int neutralizer = (ableToMoveLeft ? 1 : 0);
            transform.Translate(Vector2.left * moveSpeed * neutralizer * Time.deltaTime);
        }
        else if (moveRight)
        {
            int neutralizer = (ableToMoveRight ? 1 : 0);
            transform.Translate(Vector2.right * moveSpeed * neutralizer * Time.deltaTime);
        }
        else { print("ERROR"); }
    }

    public void UpdateAbleToMoveLeft(bool value) { ableToMoveLeft = value; }
    public void UpdateAbleToMoveRight(bool value) { ableToMoveRight = value; }
}
