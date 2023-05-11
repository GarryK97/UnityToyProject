using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{   
    Rigidbody rb;
    Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        updateAnimation(xMove, zMove);
    }
enum States
    {
        forward = 8,
        forwardLeft = 7,
        forwardRight = 9,
        left = 4,
        sprint = 5,
        right = 6,
        backwardLeft = 1,
        backward = 2,
        backwardRight = 3,
        idle = 0
    }

    private void updateAnimation(float xThrust, float zThrust)
    {   
        if (PlayerState.isFalling)
        {        
            _Animator.SetBool("isFalling", true);
            return;
        }

        if (xThrust == 0 && zThrust == 0)
        {
            _Animator.SetInteger("walkState", (int)States.idle);
            return;
        }

        if (!PlayerState.isRunning)
        {
            if (zThrust > 0) 
            {
                _Animator.SetInteger("walkState", (int)States.forward);

                if (xThrust > 0)
                {
                    _Animator.SetInteger("walkState", (int)States.forwardRight);
                }
                else if (xThrust < 0)
                {
                    _Animator.SetInteger("walkState", (int)States.forwardLeft);
                }
            }
            else if (zThrust < 0)
            {
                _Animator.SetInteger("walkState", (int)States.backward);

                if (xThrust > 0)
                {
                    _Animator.SetInteger("walkState", (int)States.backwardRight);
                }
                else if (xThrust < 0)
                {
                    _Animator.SetInteger("walkState", (int)States.backwardLeft);
                }
            }
            else if (zThrust == 0)
            {
                if (xThrust > 0)
                {
                    _Animator.SetInteger("walkState", (int)States.right);
                }
                else if (xThrust < 0)
                {
                    _Animator.SetInteger("walkState", (int)States.left);
                }
            }
        } 
        else 
        {   
            _Animator.SetInteger("walkState", (int)States.sprint);
        }
    }
}
