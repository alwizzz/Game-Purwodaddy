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
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float normalMoveSpeed;
    [SerializeField] private float panicMoveSpeed;
    [SerializeField] private bool ableToMoveLeft;
    [SerializeField] private bool ableToMoveRight;

    [Header("Caches")]
    [SerializeField] private SpriteRenderer model;
    [SerializeField] private PlayerSensor sensor;
    [SerializeField] private AdvancedButtonUI leftVirtualButton;
    [SerializeField] private AdvancedButtonUI rightVirtualButton;

    [SerializeField] private GameObject virtualButtons;
    [SerializeField] private AudioSource footstepAudioSource;


    [Header("Animator")]
    [SerializeField] private Animator animator;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isPanic;



    private void Awake()
    {
        BaseAwake(this);

        // browser and mobile use the same input: button
        //playedOnPC = (Application.isMobilePlatform ? false : true);
        //playedOnPC = false;

        virtualButtons.SetActive((playedOnPC ? false : true));
        SetToPanicMode(false);
    }


    private void Update()
    {
        if (freeze) 
        {
            footstepAudioSource.Stop(); // bad practice

            if(playedOnPC)
            {
                ProcessDialogInputByKeyboard();
            }
            return;  
        }

        ProcessInteractInput();
        ProcessPlayerInput();

        // small debugging, REMOVE WHEN DEPLOY
        if(Input.GetKeyDown(KeyCode.P))
        {
            SetToPanicMode(!isPanic);
        }
    }
    
    private void ProcessDialogInputByKeyboard()
    {
        bool dialogNextLine = Input.GetKeyDown(dialogKey);

        if (dialogNextLine)
        {
            DialogSystem.Instance().NextLine();
        }
    }

    public void ProcessDialogInputByButton() // by UI Button (Image DialogBox)
    {
        // for now, dialog only happened when the game freeze.
        // If freeze mechanic changed, this codeblock should be reviewed
        if (!freeze) { return; }

        DialogSystem.Instance().NextLine();
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
            isMoving = false;
            UpdateAnimatorParams();
            footstepAudioSource.Stop();
            return; 
        }

        isMoving = true;
        UpdateAnimatorParams();

        // neutralizer will stop the movement if unable to move in that direction
        if (moveLeft)
        {
            int neutralizer = (ableToMoveLeft ? 1 : 0);
            transform.Translate(Vector2.left * currentMoveSpeed * neutralizer * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, -180, 0);
            FlipModel(true);

            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else if (moveRight)
        {
            int neutralizer = (ableToMoveRight ? 1 : 0);
            transform.Translate(Vector2.right * currentMoveSpeed * neutralizer * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            FlipModel(false);

            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Play();
            }
        }
        else { print("ERROR"); }
    }

    private void ProcessInteractInput()
    {
        if(playedOnPC) 
        {
            bool interact = Input.GetKeyDown(interactKey);

            if (interact)
            {
                DoInteraction();
            }
        } else
        {
            // without input's conditional logic as in mobile & browser it is called by button

            //DoInteraction();
        }
    }

    public void ProcessInteractInputByButton()
    {
        if (freeze) return;
        DoInteraction();
    }

    private void DoInteraction()
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
        isMoving = (freeze ? false : true);
        UpdateAnimatorParams();

        if(value == true)
        {
            footstepAudioSource.Stop();
        }
    }

    private void UpdateAnimatorParams()
    {
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isPanic", isPanic);
    }

    public void SetToPanicMode(bool value)
    {
        isPanic = value;
        currentMoveSpeed = (isPanic ? panicMoveSpeed : normalMoveSpeed);
        UpdateAnimatorParams();
    }


    private void OnDestroy()
    {
        BaseOnDestroy();
    }
}
