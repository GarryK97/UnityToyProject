using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerCamera : MonoBehaviour
{

    public GameObject target;
 
    public float offsetY;
    private float rotationX;
    private float rotationY;
    public float distance;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {   
        rotationX += Input.GetAxis("Mouse X");
        rotationY -= Input.GetAxis("Mouse Y");

        rotationY = Mathf.Max(-20f, rotationY);
        rotationY = Mathf.Min(75, rotationY);

        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0);

        Vector3 reverseDistance = new Vector3(0.0f, 0.0f, distance);
        // 플레이어 기준으로 카메라 회전 (사실상 공식)
        transform.position = target.transform.position - transform.rotation * reverseDistance;
        // offsetY 이용해 높이조절
        transform.position = new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);
    }
}
