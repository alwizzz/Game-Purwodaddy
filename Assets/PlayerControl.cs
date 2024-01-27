using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
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

    [Header("Caches")]
    [SerializeField] private SpriteRenderer model;
    [SerializeField] private PlayerSensor sensor;


    private void Update()
    {
        ProcessInteractInput();
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
            //transform.rotation = Quaternion.Euler(0, -180, 0);
            FlipModel(true);
        }
        else if (moveRight)
        {
            int neutralizer = (ableToMoveRight ? 1 : 0);
            transform.Translate(Vector2.right * moveSpeed * neutralizer * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            FlipModel(false);
        }
        else { print("ERROR"); }
    }

    private void ProcessInteractInput()
    {
        bool interact = Input.GetKeyDown(interactKey);

        if(interact)
        {
            if(sensor.InteractableDetected())
            {
                print("interact");
            } else
            {
                print("attempt to interact when there is no interactable nearby");
            }
        }
    }

    private void FlipModel(bool value)
    {
        if(value == true)
        {
            model.flipX = true;
        } else
        {
            model.flipX = false;
        }

        sensor.Flip(value);
    }

    public void UpdateAbleToMoveLeft(bool value) { ableToMoveLeft = value; }
    public void UpdateAbleToMoveRight(bool value) { ableToMoveRight = value; }
}
