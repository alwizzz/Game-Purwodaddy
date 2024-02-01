using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : StaticReference<PlayerControl>
{
    [SerializeField] private bool playedOnPC;
    [SerializeField] private bool freeze;

    [Header("Key Inputs")]
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private KeyCode dialogKey;

    [Header("Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool ableToMoveLeft;
    [SerializeField] private bool ableToMoveRight;

    [Header("Caches")]
    [SerializeField] private SpriteRenderer model;
    [SerializeField] private PlayerSensor sensor;
    [SerializeField] private AdvancedButtonUI leftVirtualButton;
    [SerializeField] private AdvancedButtonUI rightVirtualButton;
    [SerializeField] private AdvancedButtonUI interactVirtualButton;



    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool isWalking;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        BaseAwake(this);

        // browser and mobile use the same input: button
        //playedOnPC = (Application.isMobilePlatform ? false : true);
        playedOnPC = true;
    }


    private void Update()
    {
        ProcessInteractInput();

        if (freeze) { return; }

        ProcessPlayerInput();
    }

    private void ProcessPlayerInput()
    {
        bool moveLeft;
        bool moveRight;

        if(playedOnPC)
        {
            moveLeft = Input.GetKey(leftKey);
            moveRight = Input.GetKey(rightKey);
        } else
        {
            moveLeft = leftVirtualButton.IsButtonDown();
            moveRight = rightVirtualButton.IsButtonDown();
        }


        // do nothing if either player not pushing any button or pushing both button
        if (moveLeft == moveRight) 
        {
            animator.SetBool("isWalking", false);
            audioSource.Stop();
            return; 
        }

        animator.SetBool("isWalking", true);
        // neutralizer will stop the movement if unable to move in that direction
        if (moveLeft)
        {
            int neutralizer = (ableToMoveLeft ? 1 : 0);
            transform.Translate(Vector2.left * moveSpeed * neutralizer * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, -180, 0);
            FlipModel(true);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (moveRight)
        {
            int neutralizer = (ableToMoveRight ? 1 : 0);
            transform.Translate(Vector2.right * moveSpeed * neutralizer * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            FlipModel(false);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else { print("ERROR"); }
    }

    private void ProcessInteractInput()
    {
        bool interact;

        if(playedOnPC)
        {
            interact = Input.GetKeyDown(interactKey);
        } else
        {
            interact = interactVirtualButton.IsButtonDown();
        } 


        if(interact)
        {
            if (sensor.InteractableDetected())
            {
                sensor.Interact();
            }
            else
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

    public void SetFreeze(bool value) 
    { 
        freeze = value;
        animator.SetBool("isWalking", (freeze ? false : true));
    }


    private void OnDestroy()
    {
        BaseOnDestroy();
    }
}
