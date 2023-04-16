using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCollider : MonoBehaviour
{

    [SerializeField] private int abilityReferenceNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected");
        if (collision.gameObject.tag == "Enemy")
            transform.parent.gameObject.GetComponent<Character>().AbilityHit(abilityReferenceNumber,collision.gameObject);
    }
}
