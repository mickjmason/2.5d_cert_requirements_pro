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
    private bool _canMove = true;
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
        if (_canMove)
        {
            var distance = Vector3.Distance(transform.position, _waypoints[_currentTargetIndex].position);
            if (distance > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentTargetIndex].position, _speed * Time.deltaTime);
            }
            else
            {

                StartCoroutine(SwitchWaypoints());
            }
        }

    }

    IEnumerator SwitchWaypoints()
    {
        _canMove = false;
        yield return new WaitForSeconds(5f);
        _currentTargetIndex = 1 - _currentTargetIndex;
        float yAdjust;
        if (_currentTargetIndex == 0)
        {
            yAdjust = 0.02f;
        }
        else
        {
            yAdjust = -0.02f;
        }
        transform.position += new Vector3(0f, yAdjust, 0f);
        _canMove = true;
    }


}
