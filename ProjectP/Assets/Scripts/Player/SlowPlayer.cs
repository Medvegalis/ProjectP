using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour
{

    [SerializeField] PlayerControler playerControler;

    private bool slow = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slow)
        {
            playerControler.ReduceSpeed(2);
        }
        else
        {
            playerControler.ReduceSpeed();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.transform.tag == "Player")
            slow = true;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.transform.tag == "Player")
            slow = false;
    }
}
