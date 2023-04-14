using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rgb;


    [SerializeField] private float accel;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float decel;
    [SerializeField] private GameObject[] patrolPos;
    private bool isPatrol;
    private int patrolOrder;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();

        patrolOrder = 0;
        Debug.Log("in");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(rgb.velocity.magnitude) <= maxVelocity)
            rgb.AddForce(Vector3.Normalize(patrolPos[patrolOrder].transform.position - transform.position)* accel, ForceMode2D.Force);
        else
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D others)
    {
        Debug.Log("hit");
        if (others.name == patrolPos[patrolOrder].gameObject.name)
        {
            if (patrolOrder + 1 == patrolPos.Length)
            {
                patrolOrder = 0;
            }
            else
            {
                patrolOrder += 1;
                Debug.Log("Go to next pos" +patrolOrder);
            }
        }
    }
}
