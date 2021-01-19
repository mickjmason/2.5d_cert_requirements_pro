using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [SerializeField]
    private Transform[] _waypoints = new Transform[2];

    [SerializeField]
    private float _speed;


    private int _currentTargetIndex;
    void Start()
    {
        _currentTargetIndex = 0;
    }


    void FixedUpdate()
    {
            ProcessMovement();
    }

    void ProcessMovement()
    {
        var distance = Vector3.Distance(transform.position, _waypoints[_currentTargetIndex].position);
        if (distance > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentTargetIndex].position, _speed * Time.deltaTime);
        }
        else
        {
            
            StartCoroutine(SwitchWaypoints());
        }

    }

    IEnumerator SwitchWaypoints()
    {
        yield return new WaitForSeconds(5f);
        _currentTargetIndex = 1 - _currentTargetIndex;
    }


}
