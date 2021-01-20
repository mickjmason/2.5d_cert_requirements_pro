using Assets.Scripts.UtilClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{

    [SerializeField]
    private GameObject _powerUpFx;

    [SerializeField]
    private AudioClip _pickUpClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            if(player == null)
            {
                Utils.LogNulls(player);
            }

            player.BoostSpeed();
            AudioSource.PlayClipAtPoint(_pickUpClip, transform.position);
            Instantiate(_powerUpFx, transform.position, Quaternion.identity);
            StartCoroutine(CleanUp());
            
        }
    }

    #region coroutines
    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    #endregion
}
