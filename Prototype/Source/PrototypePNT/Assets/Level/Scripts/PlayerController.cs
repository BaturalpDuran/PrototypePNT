using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Animator anim;
    private SwerveInputSystem _swerveInputSystem;
    private Rigidbody rb;
    public GameObject start;
    public UnityEngine.UI.Text playText;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    public float _moveSpeed = 1.1f;//Just a little cheat to beat bots.
    public bool isStart = false;
    public bool isGrounded = true;
    public bool isWin = false;
    

    private void Awake()
    {
        instance = this;
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isStart = true;
            Destroy(playText);
        }
        if (isStart)
        {
            MoveForward();
            SwerveMechanics();
        }
        if (isGrounded)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.46f, 0.46f), transform.position.y, transform.position.z);
        }
    }

    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name.Equals("RotatingPlatformLeft"))
        {
            rb.AddForce(-5f, 0, 0);
            isGrounded = false;
        }
        else if (collision.gameObject.name.Equals("RotatingPlatformRight"))
        {
            rb.AddForce(5f, 0, 0);
            isGrounded = false;
        }
        else if (collision.gameObject.tag.Equals("ground"))
        {
            isGrounded = true;
        }
    }

    private void SwerveMechanics()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, 0);
    }
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed, Space.World);
        anim.SetBool("isRunning", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Finish")
        {
            if (GameController.instance.boyRank == 1)
            {
                isStart = false;
                isWin = true;
                anim.SetBool("isWin", true);
                anim.SetBool("isRunning", false);
                StartCoroutine(GoWinScene());
            }
            else
            {
                isStart = false;
                anim.SetBool("isWin", false);
                anim.SetBool("isRunning", false);
                SceneManager.LoadScene("LoseScene");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            transform.position = start.transform.position;
        }

    }

    private IEnumerator GoWinScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("WinScene");
    }

}
