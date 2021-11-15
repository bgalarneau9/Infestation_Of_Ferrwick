using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    private int numberOfPlayerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        numberOfPlayerPrefabs = GameObject.FindGameObjectsWithTag("Player").Length;
        if (numberOfPlayerPrefabs == 1)
        {
            transform.position = new Vector3(player.position.x + offset.x, Mathf.Clamp(player.position.y + offset.y, -0.32f, 100), -10);
        }
    }
}
