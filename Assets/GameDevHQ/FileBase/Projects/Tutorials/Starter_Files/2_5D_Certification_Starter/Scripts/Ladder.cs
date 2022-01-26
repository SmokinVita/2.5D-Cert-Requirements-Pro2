using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    [SerializeField]
    private Transform _bottonLadderPOS, _topLadderPOS;

    private bool _onLadder;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    player.PlayerOnLadder(transform, _bottonLadderPOS, _topLadderPOS);
                }
            }
        }
    }
}
