using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveTrigger : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameobjectsToActivate;
    [SerializeField] private List<GameObject> gameobjectsToDeActivate;

    [SerializeField] private bool isBattleTrigger;
    private int diedEnemyCount;

    [SerializeField] private GameObject[] enemiesToActivate;
    [SerializeField] private GameObject[] wallsToActivate;
    [SerializeField] private GameObject[] itemsToActivate;

    [SerializeField]private GameObject[] players;
    // Start is called before the first frame update
    private void Start()
    {
        diedEnemyCount = 0;
    }

    private void Update()
    {
        if (isBattleTrigger)
        {
            
            foreach (GameObject g in enemiesToActivate)
            {
                if (g.GetComponent<BoxCollider2D>().enabled == false)
                {
                    diedEnemyCount += 1;
                }
            }
                if (enemiesToActivate.Length == diedEnemyCount)
            {
                foreach (GameObject g in wallsToActivate)
                    g.SetActive(false);
                foreach (GameObject g in itemsToActivate)
                    g.SetActive(true);

                foreach(GameObject c in players)
                {
                    c.GetComponent<Character>().playerHealth = c.GetComponent<Character>().playerMaxHealth;
                    //c.playerHealth = c.playerMaxHealth;
                }

                isBattleTrigger = false;
            }
        }

        diedEnemyCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach (GameObject g in gameobjectsToActivate)
                g.SetActive(true);
            foreach (GameObject g in gameobjectsToDeActivate)
                g.SetActive(false);

            if (isBattleTrigger)
            {
                foreach (GameObject g in enemiesToActivate)
                    g.SetActive(true);
                foreach (GameObject g in wallsToActivate)
                    g.SetActive(true);
            }
        }
    }
}
