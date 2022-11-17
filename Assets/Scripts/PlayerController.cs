using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float bounceForce = 20.0f;
    public float jumpForce = 20.0f;
    public float horizontalForce = 1;
    public float horizontalInput;
    public float jumpInput;
    public bool isOnGround = false;
    public float xRange = 19.5f;
    [SerializeField] private bool jumpCooldown;
    public float jumpCooldownTime = 1;

    public bool jumpTrigger;
    public GameObject JumpTriggerGO;
    private JumpTrigger jumpTriggerScript;
    private Rigidbody playerRB;
    public bool playerWin = false;

    [SerializeField] private AudioClip jumpSound;
    private AudioSource playerAudio;
    [SerializeField] private float volumenAudio = 1.0f;

    private GameManager gameManager; 
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        jumpTriggerScript = JumpTriggerGO.GetComponent<JumpTrigger>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {        
        jumpTrigger = jumpTriggerScript.isTriggerUp;            
        PlayerXBounds();        
        JumpCooldown();
    }
    private void FixedUpdate()
    {
        if (gameManager.timerOn)
        {
            PlayerInputs();
            InOnGroundDetection();
            PlayerJump();
        }        
    }
    IEnumerator JumpCountdownRoutine()
    {
        yield return new WaitForSeconds(jumpCooldownTime);
        jumpCooldown = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            playerWin = true;
        }
    }
    private void PlayerXBounds()
    {
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
    }
    private void InOnGroundDetection()
    {
        if (isOnGround)
        {
            UpwardForce(bounceForce);
            SideForce(horizontalForce);
            isOnGround = false;
        }
    }
    private void UpwardForce(float force)
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);
        playerRB.AddForce(Vector3.up * force, ForceMode.Impulse);
        playerAudio.PlayOneShot(jumpSound, volumenAudio);
    }
    private void SideForce(float force)
    {
        playerRB.velocity = new Vector3(0 , playerRB.velocity.y, playerRB.velocity.z);
        playerRB.AddForce(Vector3.right * force * horizontalInput, ForceMode.Impulse);
    }
    private void JumpCooldown()
    {
        if (jumpInput == 1)
        {
            Debug.Log("Entro al cooldown");
            jumpInput = 0;
            jumpCooldown = true;
            StartCoroutine(JumpCountdownRoutine());
        }
    }
    private void PlayerInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (!jumpCooldown)
        {
            jumpInput = Input.GetAxis("Jump");
        }
        
    }
    private void PlayerJump()
    {
        if (jumpInput == 1 && jumpTrigger)
        {
            SideForce(horizontalForce);
            UpwardForce(jumpForce);
        }
    }
}
