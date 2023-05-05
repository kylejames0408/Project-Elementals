using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Character>().TakeDamage(5);
            Destroy(gameObject);
        }
        if (collision.tag == "Environment")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(gameObject);

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
