using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite _lightArmor;
    [SerializeField] private Sprite _mediumArmor;
    [SerializeField] private Sprite _heavyArmor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name.Equals("WindUpdraftItem"))
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach(GameObject p in players)
                {
                    Character c = p.GetComponent<Character>();
                    if (c.isWind)
                    {
                        c.HasWindUpdraft = true;
                    }
                }
                Destroy(gameObject);
            }

            if (gameObject.name.Equals("WindPushItem"))
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject p in players)
                {
                    Character c = p.GetComponent<Character>();
                    if (c.isWind)
                    {
                        c.HasWindPush = true;
                    }
                }
                Destroy(gameObject);
            }

            if (gameObject.name.Equals("GravityOppressItem"))
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject p in players)
                {
                    Character c = p.GetComponent<Character>();
                    if (!c.isWind)
                    {
                        c.HasGravityOppress = true;
                    }
                }
                Destroy(gameObject);
            }

            if (gameObject.name.Equals("GravityDepressItem"))
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject p in players)
                {
                    Character c = p.GetComponent<Character>();
                    if (!c.isWind)
                    {
                        c.HasGravityDepress = true;
                    }
                }
                Destroy(gameObject);
            }

            if (gameObject.name.Equals("LightArmorItem"))
            {
                Character c = collision.GetComponent<Character>();
                
                switch (c.Armor)
                {
                    case 0:
                        c.Armor = 1;
                        Destroy(gameObject);
                        break;
                    case 1:
                        break;
                    case 2:
                        c.Armor = 1;
                        gameObject.name = "MediumArmorItem";
                        gameObject.GetComponent<SpriteRenderer>().color = new Color32(180, 180, 180, 255);
                        break;
                    case 3:
                        c.Armor = 1;
                        gameObject.name = "HeavyArmorItem";
                        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 190, 52, 255);
                        break;
                }
            }

            if (gameObject.name.Equals("MediumArmorItem"))
            {
                Character c = collision.GetComponent<Character>();

                switch (c.Armor)
                {
                    case 0:
                        c.Armor = 2;
                        Destroy(gameObject);
                        break;
                    case 1:
                        c.Armor = 2;
                        gameObject.name = "LightArmorItem";
                        gameObject.GetComponent<SpriteRenderer>().color = new Color32(180, 125, 79, 255);
                        break;
                    case 2:
                        break;
                    case 3:
                        c.Armor = 2;
                        gameObject.name = "HeavyArmorItem";
                        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 190, 52, 255);
                        break;
                }
            }

            if (gameObject.name.Equals("HeavyArmorItem"))
            {
                Character c = collision.GetComponent<Character>();

                switch (c.Armor)
                {
                    case 0:
                        c.Armor = 3;
                        Destroy(gameObject);
                        break;
                    case 1:
                        c.Armor = 3;
                        gameObject.name = "LightArmorItem";
                        gameObject.GetComponent<SpriteRenderer>().color = new Color32(180, 125, 79, 255);
                        break;
                    case 2:
                        c.Armor = 3;
                        gameObject.name = "MediumArmorItem";
                        gameObject.GetComponent<SpriteRenderer>().color = new Color32(180, 180, 180, 255);
                        break;
                    case 3:
                        break;
                }
            }
        }
    }
}
