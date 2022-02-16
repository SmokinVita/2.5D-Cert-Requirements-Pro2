using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePad : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _eventToCall;

    [SerializeField]
    private Renderer _padDisplay;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Moveable"))
        {
            float distance = Vector3.Distance(other.transform.position, transform.position);
            Debug.Log(distance);
            if(distance <= 1f)
            {
                other.attachedRigidbody.isKinematic = true;
                _padDisplay.material.color = Color.green;
                transform.position = new Vector3(transform.position.x, transform.position.y - .081f, transform.position.z);
                _eventToCall.Invoke();
            }
        }
    }
}
