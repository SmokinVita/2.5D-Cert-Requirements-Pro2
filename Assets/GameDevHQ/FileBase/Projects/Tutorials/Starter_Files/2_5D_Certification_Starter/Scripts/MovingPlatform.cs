using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private Transform _targetA, _targetB, _currentTarget;
    [SerializeField]
    private float _speed = 1f;

    void Start()
    {
        _currentTarget = _targetA;
    }

    void FixedUpdate()
    {
        if(transform.position == _targetA.position)
        {
            _currentTarget = _targetB;
        }
        else if(transform.position == _targetB.position)
        {
            _currentTarget = _targetA;
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = transform;
            Debug.Log("Player is on Platform!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = null;
            Debug.Log("Player is off of the platform!");
        }
    }
}
