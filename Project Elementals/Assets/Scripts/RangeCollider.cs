using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCollider : MonoBehaviour
{

    [SerializeField] public int abilityReferenceNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with " + collision.gameObject.name);
        if (collision.gameObject.tag == "Enemy")
            transform.parent.gameObject.GetComponent<Character>().AbilityHit(abilityReferenceNumber,collision.gameObject);
        else if(collision.gameObject.tag == "Rock")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("RockUp", true);
        }
    }
}
