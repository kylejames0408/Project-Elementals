using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;       //to store a reference to the player game object 
    private Vector3 offset;         //distance between camera and player
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
