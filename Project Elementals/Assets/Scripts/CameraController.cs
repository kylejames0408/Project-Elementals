using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;       //to store a reference to the player game object 
    public float smootheningSpeed;
    public Vector3 offset;         //distance between camera and player

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Target vector is " + (player.transform.position + offset));
        transform.position = (player.transform.position + offset);
        //transform.position = Vector2.Lerp(transform.position, (player.transform.position + offset),Time.deltaTime*smootheningSpeed);
    }

    public void Switch(GameObject target)
    {
        player = target;
    }
}
