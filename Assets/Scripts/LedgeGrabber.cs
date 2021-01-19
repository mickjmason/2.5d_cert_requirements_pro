using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabber : MonoBehaviour
{
    [SerializeField]
    private Vector3 _handPosition;

    [SerializeField]
    private Vector3 _standPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            var player = other.GetComponentInParent<Player>();
            if(player == null)
            {
                Debug.LogError("Player is null");
            }
            player.GrabLedge(_handPosition, _standPosition);
        }
    }
}
