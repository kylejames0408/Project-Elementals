using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCollider : MonoBehaviour
{

    [SerializeField] public int abilityReferenceNumber;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with " + collision.gameObject.name);
        if (abilityReferenceNumber == 4)
        {
            transform.parent.gameObject.GetComponent<Character>().SpeedUp();
        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
                transform.parent.gameObject.GetComponent<Character>().AbilityHit(abilityReferenceNumber, collision.gameObject);
            else if (collision.gameObject.tag == "Rock" && abilityReferenceNumber == 1)
            {
                collision.gameObject.GetComponent<Animator>().SetBool("RockUp", true);
            }
            else if(collision.gameObject.tag == "Bridge" && abilityReferenceNumber == 2)
            {
                collision.gameObject.GetComponent<Animator>().enabled = true;
            }
        }
    }
}
