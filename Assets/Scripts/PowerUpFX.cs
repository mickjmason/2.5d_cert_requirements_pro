using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFX : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CleanUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region coroutines
    IEnumerator CleanUp()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    #endregion
}
