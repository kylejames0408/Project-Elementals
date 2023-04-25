using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerProjectile : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(5);

            GetComponent<Animator>().SetTrigger("HitCollider");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        if(collision.tag == "Environment")
        {
            GetComponent<Animator>().SetTrigger("HitCollider");
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
