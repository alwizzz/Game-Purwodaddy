using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    [SerializeField] private bool interactableDetected;
    [SerializeField] private GameObject interactable;

    private float xValue;

    private void Awake()
    {
        xValue = Mathf.Abs(transform.localPosition.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Interactable")
        {
            SetInteractableDetected(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            SetInteractableDetected(null);
        }
    }

    private void SetInteractableDetected(GameObject obj)
    {
        interactableDetected = (obj == null ? false : true);
        interactable = obj;
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

}
