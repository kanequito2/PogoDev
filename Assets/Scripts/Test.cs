using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerController playerController;
    [SerializeField] Slider bounceSlider;
    [SerializeField] TextMeshProUGUI bounceForceText;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        bounceForceText.text = "Bounce force: " + bounceSlider.value;
        playerController.bounceForce = bounceSlider.value;
    }
}
