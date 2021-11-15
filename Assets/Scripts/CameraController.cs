using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransfrom;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerTransfrom.position.x + offset.x, playerTransfrom.position.y + offset.y, offset.z); // Camera follows the player with specified offset position   
    }
}
