using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBox : MonoBehaviour
{
    private BoxCollider EntryBox;
    public GameObject ExitBox;
    
    // Start is called before the first frame update
    void Start()
    {
        EntryBox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {   
        collider.transform.position = ExitBox.transform.position - new Vector3(0,2f,0);
        collider.GetComponentInParent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
