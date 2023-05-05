using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathCollider : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision tag is " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1000000);
    }
}
