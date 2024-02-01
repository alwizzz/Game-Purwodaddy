using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InitialEvents : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent initialEvents;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delay);
        initialEvents.Invoke();
    }

}
