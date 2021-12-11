using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{

    [SerializeField]
    private Transform _handPos, _standPos;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ledge_Grab_Checking"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if(player != null)
            {
                player.GrabLedge(_handPos.position, this);
            }
        }
    }

    public Vector3 GetStandPos()
    {
        return _standPos.position;
    }
}
