using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckBox : MonoBehaviour
{   
    Rigidbody rb;
    private float MidAirTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MidAirTime += Time.deltaTime;
        if (MidAirTime > 0.5f)
        {   
            PlayerState.isFalling = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {   
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            PlayerState.isMidAir = false;
            PlayerState.isFalling = false;
            MidAirTime = 0;

            //Debug.Log("This is Third test");
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayerState.isMidAir = false;
            PlayerState.isFalling = false;
            MidAirTime = 0;
        }
    }

        void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayerState.isMidAir = true;
        }
    }
}
