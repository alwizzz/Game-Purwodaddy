using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] private bool interactableDetected;
    [SerializeField] private List<Interactable> detectedInteractables;
    [SerializeField] private Interactable focusedInteractable;

    private float xValue;

    private void Awake()
    {
        xValue = Mathf.Abs(transform.localPosition.x);
    }

    private void Update()
    {
        UpdateInteractableDetected();
        if(!interactableDetected) { return; }

        SortDetectedInteractables();
        UpdateFocusedInteractable();
    }

    private void UpdateInteractableDetected()
    {
        interactableDetected = (detectedInteractables.Count > 0 ? true : false);
        if (!interactableDetected) { focusedInteractable = null; }
    }

    private void SortDetectedInteractables()
    {
        detectedInteractables.Sort(
            delegate(Interactable i1, Interactable i2)
            {
                var i1Distance = Vector3.Distance(transform.position, i1.gameObject.transform.position);
                var i2Distance = Vector3.Distance(transform.position, i2.gameObject.transform.position);

                if(i2Distance < i1Distance)
                {
                    return 1;
                } else
                {
                    return -1;
                }
            }
        );
    }

    private void UpdateFocusedInteractable()
    {
        focusedInteractable = detectedInteractables[0];
        for(int i=0; i<detectedInteractables.Count; i++)
        {
            if (i == 0)
            {
                detectedInteractables[i].SetIsFocusedByPlayer(true);
            } else
            {
                detectedInteractables[i].SetIsFocusedByPlayer(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Interactable")
        {
            //SetInteractableDetected(collision.gameObject);
            AddToDetectedInteractables(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            RemoveFromDetectedInteractables(collision.gameObject);
        }
    }

    private void AddToDetectedInteractables(GameObject obj)
    {
        var interactable = obj.GetComponent<Interactable>();
        detectedInteractables.Add(interactable);
    }

    private void RemoveFromDetectedInteractables(GameObject obj)
    {
        var interactable = obj.GetComponent<Interactable>();
        detectedInteractables.Remove(interactable);
    }

    public void Flip(bool value)
    {
        if(value == true)
        {
            transform.localPosition = new Vector3(
                xValue * -1,
                transform.localPosition.y,
                transform.localPosition.z
            );
        } else
        {
            transform.localPosition = new Vector3(
                xValue,
                transform.localPosition.y,
                transform.localPosition.z
            );
        }
    }

    public bool InteractableDetected() => interactableDetected;

    public void Interact()
    {
        if (!interactableDetected)
        {
            print("ERROR");
            return;
        }

        focusedInteractable.Interact();
    }

}
