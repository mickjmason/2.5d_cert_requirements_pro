using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent = null;
        }
    }
    IEnumerator SwitchWaypoints()
    {
        _canMove = false;
        yield return new WaitForSeconds(0.1f);
        _currentTargetIndex = 1 - _currentTargetIndex;
        _canMove = true;
    }

}
