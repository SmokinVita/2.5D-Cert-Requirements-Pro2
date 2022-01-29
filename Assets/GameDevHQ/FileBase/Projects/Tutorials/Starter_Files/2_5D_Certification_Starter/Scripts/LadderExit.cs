using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null)
            {
                player.PlayerGetOffLadder();
            }
        }
    }
}
