using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rgb;


    //Movement and Patrol
    [SerializeField] private float accel;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float decel;
    [SerializeField] private float sojournTime;
    [SerializeField] private GameObject[] patrolPos;
    private bool isPatrol;
    private int patrolOrder;
    private float tempAccel;
    private float tempSojourn;
    //Define status of enemy
    [SerializeField] private float HP;


    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();

        //Patrol and sojourn time
        patrolOrder = 0;
        if(accel <= 0)
        {
            isPatrol = false;
        }
        else
        {
            isPatrol = true;
            
        }
        tempSojourn = sojournTime;
        tempAccel = accel;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (tempSojourn > 0 && !isPatrol)
        {
            accel = 0;
            tempSojourn -= Time.deltaTime;
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), 0.02f);
            //Debug.Log("!isPatrol");
        }
        else
        {
            accel = tempAccel;
            tempSojourn = sojournTime;
            isPatrol = true;
        }

        if (Mathf.Abs(rgb.velocity.magnitude) <= maxVelocity)
            rgb.AddForce(Vector3.Normalize(patrolPos[patrolOrder].transform.position - transform.position)* accel, ForceMode2D.Force);
        else
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel);


    }

    private void OnTriggerEnter2D(Collider2D others)
    {
        if (others.name == patrolPos[patrolOrder].gameObject.name)
        {
            if (patrolOrder + 1 == patrolPos.Length)
            {
                patrolOrder = 0;
                isPatrol = false;
            }
            else
            {
                patrolOrder += 1;
                isPatrol = false;
                //Debug.Log("Go to next pos" +patrolOrder);
            }
        }
    }
}
