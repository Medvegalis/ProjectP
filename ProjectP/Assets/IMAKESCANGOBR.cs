using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMAKESCANGOBR : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(LateStart(1f));

    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<AstarPath>().Scan();
    }

}
