using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField]
    private int _coinAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null)
            {
                player.AddCoins(_coinAmount);
                Destroy(gameObject);
            }
        }
    }
}
