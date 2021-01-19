using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabber : MonoBehaviour
{
    [SerializeField]
    private Transform _handPosition;

    [SerializeField]
    private Transform _standPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            var player = other.GetComponentInParent<Player>();
            if(player == null)
            {
                Debug.LogError("Player is null");
            }
            player.GrabLedge(_handPosition.position, _standPosition.position);
        }
    }
}
