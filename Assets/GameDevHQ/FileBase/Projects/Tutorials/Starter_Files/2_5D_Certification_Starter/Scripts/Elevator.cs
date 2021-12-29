using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private List<Transform> _elevatorLevels = new List<Transform>();
    [SerializeField]
    private int _currentTarget;

    private bool _moveDown, _moveUp, _isMoving;

    void Start()
    {
        Debug.Log(transform.position);
        if (transform.position == _elevatorLevels[_currentTarget].position)
        {
            StartCoroutine(ElevatorDelayRoutine());
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _elevatorLevels[_currentTarget].position, _speed * Time.deltaTime);

        if (transform.position == _elevatorLevels[_currentTarget].position && _isMoving == true)
        {
            _isMoving = false;
            StartCoroutine(ElevatorDelayRoutine());
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

    private IEnumerator ElevatorDelayRoutine()
    {
        yield return new WaitForSeconds(5f);

        if(_currentTarget == _elevatorLevels.Count -1)
        {
            if(transform.position.y > _elevatorLevels[0].position.y)
            {
                _currentTarget = 0;
            }
            else
            {
                _currentTarget--;
            }
        }
        else
        {
            _currentTarget++;
        }

        _isMoving = true;
    }
}
