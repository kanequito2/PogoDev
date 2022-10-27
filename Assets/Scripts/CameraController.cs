using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{    
    public Vector3 cameraPosition;
    private GameObject player;
    private float yCameraAdjustmentUpward = 18;
    private float yCameraAdjustmentDownward = 0.97f;
    private float yCameraAdjustment = 18;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CameraAjustment();
    }
    void Update()
    {
        Debug.Log(player.name);
       
    }
    private void CameraAjustment()
    {
        if(player.transform.position.y > yCameraAdjustmentUpward)
        {
            cameraPosition = gameObject.transform.position;
            cameraPosition.y += yCameraAdjustment;
            yCameraAdjustmentUpward += yCameraAdjustment;
            yCameraAdjustmentDownward += yCameraAdjustment;
            gameObject.transform.position = cameraPosition;
        }
        if (player.transform.position.y < yCameraAdjustmentDownward)
        {
            cameraPosition = gameObject.transform.position;
            cameraPosition.y -= yCameraAdjustment;
            yCameraAdjustmentUpward -= yCameraAdjustment;
            yCameraAdjustmentDownward -= yCameraAdjustment;
            gameObject.transform.position = cameraPosition;
            Debug.Log(player.transform.position.y);
        }
    }
}
