using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float bounceForce = 10.0f;
    public float horizontalForce = 1;
    public float horizontalInput;
    public float jumpInput;
    public bool isOnGround = false;
    public float xRange = 19.5f;
    public bool jumpCooldown;

    public bool jumpTrigger;
    public GameObject JumpTriggerGO;
    private JumpTrigger jumpTriggerScript;

    public float timer = 0;
    private Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        jumpTriggerScript = JumpTriggerGO.GetComponent<JumpTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");
        jumpTrigger = jumpTriggerScript.isTriggerUp;
        if (isOnGround)
        {
            playerRB.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            playerRB.AddForce(Vector3.right * horizontalForce * horizontalInput, ForceMode.Impulse);
            isOnGround = false;
        }
        if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if(jumpInput == 1 && jumpTrigger)
        {
            playerRB.AddForce(Vector3.up * 15, ForceMode.Impulse);
        }
        if(jumpInput == 1)
        {
            jumpCooldown = true;
            StartCoroutine(JumpCountdownRoutine());
        }
        Debug.Log(timer);
        timer = 0;
    }

    IEnumerator JumpCountdownRoutine()
    {
        Debug.Log("entro a la rutina");
        yield return new WaitForSeconds(1);
        jumpCooldown = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            timer += Time.deltaTime;
        }
    }
}
