using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    //know the starting and ending pos. Ending pos will be when the character finish getting off ladder
    [SerializeField]
    private Transform _startPos, _endPos;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    player.PlayerOnLadder(_startPos.position, this);
                }
            }
        }
    }

    public Vector3 OffLadderPos()
    {
        return _endPos.position;
    }
}
