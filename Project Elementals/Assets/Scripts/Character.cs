using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;







public class Character : MonoBehaviour
{
    private Rigidbody2D rgb;



    [SerializeField] private float accel;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float spedVelocity;
    [SerializeField] private float decel;
    private float originalVel;
    private float playerHealth;
    [SerializeField]private float playerMaxHealth;
    public GameObject boxColliderPrefab;
    public GameObject circleColliderPrefab;
    public bool isWind = true;
    private int equippedWindItem = 0;
    private int equippedGravItem = 0;
    private float rangeTimer = 0;
    private float switchTimer = 0;
    private float autoTimer = 0;
    private float speedUpTimer = 0;
    [SerializeField] private float switchCooldown = 0;
    [SerializeField] private float windAutoCooldown;
    [SerializeField] private float gravAutoCooldown;
    [SerializeField] private float speedUpTime;
    [SerializeField] private float projectileSpeed;
    private bool startTimer = false;
    private GameObject activatedCollider;
    [SerializeField] private GameObject otherChar;
    public bool controlled;
    [SerializeField] private Sprite windSprite;
    [SerializeField] private Sprite gravSprite;
    [SerializeField] private Sprite mugshot;
    [SerializeField] public List<Ability> abilities = new List<Ability>();
    [SerializeField] private GameObject windAutoProjectile;
    [SerializeField] private GameObject gravAuto;
    [SerializeField] private GameObject projectileSpawnLocation;
    private Vector2 natScale;
    [SerializeField] private float distanceToPlayer;
    public GameObject mugshotHolder;
    public GameObject healthBar;

    public bool HasWindUpdraft;
    public bool HasWindPush;
    public bool HasGravityOppress;
    public bool HasGravityDepress;
    public int Armor = 0;

    [SerializeField] private Image _item1UI;
    [SerializeField] private Image _item2UI;
    [SerializeField] private Image _armorUI;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerMaxHealth;
        originalVel = maxVelocity;
        natScale = transform.localScale;
        rgb = GetComponent<Rigidbody2D>();
        foreach (Transform t in transform)
            if (t.name.Contains("Range"))
                t.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (controlled)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                transform.localScale = natScale;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                transform.localScale = new Vector2(-natScale.x, natScale.y);
            }



            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                GetComponentInChildren<Animator>().SetBool("isRunning", true);
                otherChar.GetComponentInChildren<Animator>().SetBool("isRunning", true);
            }
            else
            {
                GetComponentInChildren<Animator>().SetBool("isRunning", false);
                otherChar.GetComponentInChildren<Animator>().SetBool("isRunning", false);
            }

            if (Mathf.Abs(rgb.velocity.magnitude) <= maxVelocity)
                rgb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * accel * (1-((float)Armor/10.0f)), ForceMode2D.Force);
            else
                rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel * Time.deltaTime);

            if (Input.GetAxis("Vertical") == 0 && rgb.velocity.y != 0)
            {
                rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(rgb.velocity.x, 0), decel * Time.deltaTime);
            }


            if (Input.GetMouseButtonDown(1))//Right Click
            {
                StartAbilityAnim();
            }

            if (Input.GetMouseButtonUp(1))
            {
                if (isWind && equippedWindItem != 0)
                    UseAbility();
                else if (!isWind && equippedGravItem != 0)
                    UseAbility();
            }

            if (Input.GetMouseButtonDown(0) && autoTimer <= 0) //LeftClick
            {
                BasicAttack();
            }
            if(Input.GetMouseButtonUp(0))
            {
                if(!isWind)
                {
                    gravAuto.SetActive(false);
                }
            }

            if (Input.GetKeyDown(KeyCode.Q) && switchTimer <= 0)
            {

                Switch();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (isWind && HasWindUpdraft)
                {
                    EquipItem(1);
                }

                if (!isWind && HasGravityOppress)
                {
                    EquipItem(2);
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (isWind && HasWindUpdraft)
                {
                    EquipItem(3);
                }

                if (!isWind && HasGravityDepress)
                {
                    EquipItem(4);
                }
            }

            // UI STUFF
            if (isWind)
            {
                switch (equippedWindItem)
                {
                    case 1:
                        _item1UI.color = new Color32(98, 229, 219, 255);
                        _item2UI.color = new Color32(73, 159, 152, 127);
                        break;
                    case 3:
                        _item1UI.color = new Color32(98, 229, 219, 127);
                        _item2UI.color = new Color32(73, 159, 152, 255);
                        break;
                }

                if (!HasWindUpdraft)
                {
                    _item1UI.color = new Color32(98, 229, 219, 0);
                }
                if (!HasWindPush)
                {
                    _item2UI.color = new Color32(73, 159, 152, 0);
                }
            }

            if (!isWind)
            {
                switch (equippedGravItem)
                {
                    case 2:
                        _item1UI.color = new Color32(147, 29, 188, 255);
                        _item2UI.color = new Color32(159, 73, 114, 127);
                        break;
                    case 4:
                        _item1UI.color = new Color32(147, 29, 188, 127);
                        _item2UI.color = new Color32(159, 73, 114, 255);
                        break;
                }

                if (!HasGravityOppress)
                {
                    _item1UI.color = new Color32(147, 29, 188, 0);
                }
                if (!HasGravityDepress)
                {
                    _item2UI.color = new Color32(159, 73, 114, 0);
                }
            }

            switch (Armor)
            {
                case 0:
                    _armorUI.color = new Color32(0, 0, 0, 0);
                    break;
                case 1:
                    _armorUI.color = new Color32(180, 125, 79, 255);
                    break;
                case 2:
                    _armorUI.color = new Color32(180, 180, 180, 255);
                    break;
                case 3:
                    _armorUI.color = new Color32(255, 190, 52, 255);
                    break;
            }
        }


        if (Input.GetAxis("Horizontal") == 0 && rgb.velocity.x != 0)
        {
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, rgb.velocity.y), decel * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel * Time.deltaTime);
        }

        //Handling cooldowns

        if (startTimer)
        {
            if (rangeTimer > 0)
                rangeTimer -= Time.deltaTime;
            else
            {
                rangeTimer = 0;
                startTimer = false;
                foreach (Transform t in transform)
                    if (t.name.Contains("Range"))
                        t.gameObject.SetActive(false);

            }
        }
        if (switchTimer > 0)
            switchTimer -= Time.deltaTime;
        if (autoTimer > 0)
            autoTimer -= Time.deltaTime;
        if (speedUpTimer > 0)
        {
            speedUpTimer -= Time.deltaTime;
            if (speedUpTimer <= 0)
            {
                maxVelocity = originalVel;
                speedUpTimer = 0;
            }
        }



        distanceToPlayer = Vector3.Distance(otherChar.transform.position, this.transform.position);
        if (!controlled)
        {
            if (distanceToPlayer > 2)
            {
                rgb.AddForce(Vector3.Normalize(otherChar.transform.position - transform.position) * accel, ForceMode2D.Force);
            }
            else
                rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel);
        }

    }

    public void StartAbilityAnim()
    {
        if (isWind)
        {
            transform.Find((abilities[equippedWindItem - 1].name + "Anim")).gameObject.SetActive(true);
        }
        else
        {
            transform.Find((abilities[equippedGravItem - 1].name + "Anim")).gameObject.SetActive(true);
        }
    }

    public void UseAbility()
    {
        rangeTimer = 0.05f;
        startTimer = true;
        if (isWind)
        {
            transform.Find((abilities[equippedWindItem - 1].name + "Anim")).GetComponent<Animator>().SetTrigger("MouseUp");
            transform.Find((abilities[equippedWindItem - 1].name + "Range")).gameObject.SetActive(true);
        }
        else
        {
            transform.Find((abilities[equippedGravItem - 1].name + "Anim")).GetComponent<Animator>().SetTrigger("MouseUp");
            transform.Find((abilities[equippedGravItem - 1].name + "Range")).gameObject.SetActive(true);
        }
    }

    public void AbilityHit(int abilityReferenceNumber, GameObject enemyHit)
    {
        Debug.Log("Ability number " + abilityReferenceNumber + " hit enemy " + enemyHit.name);
        enemyHit.GetComponent<Enemy>().HitByAbility(abilityReferenceNumber);
    }

    public void SpeedUp()
    {
        maxVelocity = spedVelocity;
        if (!isWind)
            otherChar.GetComponent<Character>().SpeedUp();
        speedUpTimer = speedUpTime;
    }

    public void BasicAttack()
    {
        if (isWind)
        {
            GameObject projectile = Instantiate(windAutoProjectile, projectileSpawnLocation.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(transform.localScale.x), 0) * projectileSpeed;
            autoTimer = windAutoCooldown;
        }
        else
        {
            gravAuto.SetActive(true);
        }

    }

    public void EquipItem(int refNumber)
    {
        Debug.Log("Setting item to " + refNumber);
        if (refNumber % 2 == 0)
            equippedGravItem = refNumber;
        else
            equippedWindItem = refNumber;
    }

    public void SetInControl()
    {
        Debug.Log("Set in control called by " + name);
        switchTimer = switchCooldown;
        controlled = true;
        mugshotHolder.GetComponent<Image>().sprite = mugshot;
        healthBar.GetComponent<Image>().fillAmount = playerHealth / playerMaxHealth;
    }

    public void Switch()
    {
        Debug.Log("Switch called by " + name);
        rgb.velocity = Vector2.zero;
        controlled = false;
        otherChar.GetComponent<Character>().SetInControl();
        Camera.main.gameObject.GetComponent<CameraController>().Switch(otherChar);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= (damage - Armor);
        if(playerHealth<=0)
        {
            //Do death things
        }
        else
        {
            healthBar.GetComponent<Image>().fillAmount = playerHealth / playerMaxHealth;
        }
    }

}

[System.Serializable]
public class Ability
{
    public int index;
    public string name;
    public int colliderType; // 0 - Rectangle, anything else - Circular
    public float xOffset;
    public float yOffset;
    public float xSize;
    public float ySize;

    public Ability(int ind, string n, int cType, float xO, float yO, float xS, float yS)
    {
        index = ind;
        name = n;
        colliderType = cType;
        xOffset = xO;
        yOffset = yO;
        xSize = xS;
        ySize = yS;
    }
}

#if UNITY_EDITOR
public class BuildColliders : EditorWindow
{


    [MenuItem("Window/Generate Colliders")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BuildColliders));
    }


    private void OnGUI()
    {
        if (GUILayout.Button("Run Function"))
        {
            CreateColliders();
            Debug.Log("Got here");
        }
    }

    private GameObject player;
    [SerializeField] public List<Ability> abilities;
    public GameObject boxColliderPrefab;
    public GameObject circleColliderPrefab;
    private BoxCollider2D boxColliderReference;
    private CircleCollider2D circleColliderReference;
    private GameObject cloneReference;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxColliderPrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().boxColliderPrefab;
        circleColliderPrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().circleColliderPrefab;
        Debug.Log("Got here tho");
    }

    private void CreateColliders()
    {
        Debug.Log("Got here");
        abilities = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().abilities;
        foreach (Transform t in player.transform)
        {
            Debug.Log(t.name);
            if (t.name.Contains("Range"))
                DestroyImmediate(t.gameObject);
        }
        foreach (Ability a in abilities)
        {
            if (a.colliderType == 0)
            {
                cloneReference = GameObject.Instantiate(boxColliderPrefab, player.transform);
                boxColliderReference = cloneReference.GetComponent<BoxCollider2D>();
                boxColliderReference.size = new Vector2(a.xSize, a.ySize);
                boxColliderReference.offset = new Vector2(a.xOffset, a.yOffset);

            }
            else
            {
                cloneReference = GameObject.Instantiate(circleColliderPrefab, player.transform);
                circleColliderReference = cloneReference.GetComponent<CircleCollider2D>();
                circleColliderReference.radius = a.xSize;
                circleColliderReference.offset = new Vector2(a.xOffset, a.yOffset);
            }
            cloneReference.name = a.name + "Range";
            cloneReference.GetComponent<RangeCollider>().abilityReferenceNumber = a.index;
        }

    }
}
#endif