using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    public float _swerveSpeed = 1f;
    private Rigidbody rb;
    private Animator anim;
    private bool isStart = false;
    private bool isGrounded = true;
    public GameObject start;
    public GameObject finishPlatform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStart = true;
            anim.SetBool("isRunning", true);
        }
        if (isStart)
        {
            MoveForward();
            RayCasting();
        }
    }


    void RayCasting()
    {
        RaycastHit hit;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.065f, transform.position.z);

        if (Physics.SphereCast(pos,0.2f,transform.forward,out hit, 0.5f))
        {
            
            Debug.DrawLine(pos, hit.point, Color.red);

            if (hit.collider.tag == "Obstacle" && hit.collider.transform.position.x < 0)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed, Space.World);
                if (isGrounded)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.46f, 0.46f), transform.position.y, transform.position.z);
                }
            }
            else if (hit.collider.tag == "Obstacle" && hit.collider.transform.position.x >= 0)
            {
                transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed, Space.World);
                if (isGrounded)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.46f, 0.46f), transform.position.y, transform.position.z);
                }
            }
            else if (hit.collider.tag == "Rotator" && hit.collider.transform.position.x < 0)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed, Space.World);
                if (isGrounded)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.46f, 0.46f), transform.position.y, transform.position.z);
                }
            }
            else if (hit.collider.tag == "Rotator" && hit.collider.transform.position.x >= 0)
            {
                transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed, Space.World);
                if (isGrounded)
                {
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.46f, 0.46f), transform.position.y, transform.position.z);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Rotate" && transform.position.x <-0.08)
        {
            rb.AddForce(Random.Range(100f,80f), 0f, 0f);
        }
        else if (other.name == "Rotate" && transform.position.x >= 0.08)
        {
            rb.AddForce(Random.Range(-80f,-100f), 0f, 0f);
        }
        if(other.name == "Finish")
        {
            anim.SetBool("isRunning", false);
            isStart = false;
            transform.position = finishPlatform.transform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Equals("RotatingPlatformLeft"))
        {
            rb.AddForce(-6f, 0, 0);
            isGrounded = false;
        }
        else if (collision.gameObject.tag.Equals("RotatingPlatformRight"))
        {
            rb.AddForce(6f, 0, 0);
            isGrounded = false;
        }
        else if (collision.gameObject.tag.Equals("ground"))
        {
            isGrounded = true;
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            transform.position = start.transform.position;
        } 
    }


}
