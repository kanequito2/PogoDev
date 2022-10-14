using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float bounceForce = 10.0f;
    public float horizontalForce = 1;
    public float horizontalInput;
    public bool isOnGround = false;

    public float timer = 0;
    private Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (isOnGround)
        {
            playerRB.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            playerRB.AddForce(Vector3.right * horizontalForce * horizontalInput, ForceMode.Impulse);
            isOnGround = false;
        }
        Debug.Log(timer);
        timer = 0;
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
