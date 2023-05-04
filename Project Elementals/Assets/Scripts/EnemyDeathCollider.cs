using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().TakeDamage(1000000);
    }
}
