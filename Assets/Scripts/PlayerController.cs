using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields

    public float thrust = 1000;
    public float turnSpeed = 100;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 zeroVector = new Vector3(0, 0, 0);
    public float boost = 1f;
    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        #region Directions
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * thrust * Time.deltaTime);
            //animator.SetBool("Running", true);
        };

        if (rb.velocity == zeroVector)
        {
            //animator.SetBool("Running", false);
        }

        if (Input.GetKey(KeyCode.S))
        {   
            rb.AddForce(-transform.forward * thrust * Time.deltaTime);
            Debug.Log(rb.velocity);
        };

        if (Input.GetKey(KeyCode.A))
        {
            transform.forward = zeroVector;
            rb.AddForce(-transform.right * thrust * Time.deltaTime);
            Debug.Log(rb.velocity);
        };

        if (Input.GetKey(KeyCode.D))
        {
            transform.forward = zeroVector;
            rb.AddForce(transform.right * thrust * Time.deltaTime);
            Debug.Log(rb.velocity);
        };
        #endregion

        #region Flying

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * thrust * boost * Time.deltaTime, 0);
            Debug.Log(rb.velocity);
        };

        if (Input.GetKey(KeyCode.Mouse1))
        {
            rb.AddForce(-transform.up * thrust * boost * Time.deltaTime);
            Debug.Log(rb.velocity);
        };

        if (Input.GetKey(KeyCode.LeftShift))
        {
            boost = 4f;
        }
        else
        {
            boost = 1f;
        }
        #endregion

        #region Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(-transform.up * turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(transform.up * turnSpeed * Time.deltaTime);
        }
        #endregion

        #region AnimationBools
        var localVelocity = transform.InverseTransformDirection(rb.velocity);
        var forwardSpeed = localVelocity.z;
        var backSpeed = -localVelocity.z;
        var leftSpeed = -localVelocity.x;
        var rightSpeed = localVelocity.x;
        var upSpeed = localVelocity.y;
        Debug.Log(leftSpeed);

        if (forwardSpeed >= .2 & leftSpeed <= 1 & rightSpeed <= 1 & rb.position.y < 1.1)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (leftSpeed > .1 & rb.position.y < 1.1)
        {
            animator.SetBool("lStrafe", true);
        }
        else
        {
            animator.SetBool("lStrafe", false);
        }

        if (rightSpeed > .1 & rb.position.y < 1.1)
        {
            animator.SetBool("rStrafe", true);
        }
        else
        {
            animator.SetBool("rStrafe", false);
        }

        if (backSpeed > .1 & leftSpeed <= 1 & rightSpeed <= 1 & rb.position.y < 1.1)
        {
            animator.SetBool("Back", true);
        }
        else
        {
            animator.SetBool("Back", false);
        }

        if (rb.position.y > 1.1)
        {
            animator.SetBool("Hover", true);
        }
        else
        {
            animator.SetBool("Hover", false);
        }
        #endregion
    }
}
