using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrigger : MonoBehaviour
{

    [SerializeField] private List<GameObject> gameobjectsToActivate;
    [SerializeField] private List<GameObject> gameobjectsToDeActivate;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach (GameObject g in gameobjectsToActivate)
                g.SetActive(true);
            foreach (GameObject g in gameobjectsToDeActivate)
                g.SetActive(false);
        }
    }
}
