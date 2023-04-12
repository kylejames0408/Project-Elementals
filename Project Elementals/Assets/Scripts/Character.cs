using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody2D rgb;


    [SerializeField] private float accel;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float decel;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(rgb.velocity.magnitude) <= maxVelocity)
        rgb.AddForce(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")) * accel, ForceMode2D.Force);
        else
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel);

        if (Input.GetAxis("Vertical") == 0 && rgb.velocity.y!=0)
        {
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(rgb.velocity.x, 0), decel);
        }
        if(Input.GetAxis("Horizontal") == 0 && rgb.velocity.x!=0)
        {
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, rgb.velocity.y), decel);
        }
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel);
        }
    }
}
