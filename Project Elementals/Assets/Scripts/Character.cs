using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;







public class Character : MonoBehaviour 
{
    private Rigidbody2D rgb;



    [SerializeField] private float accel;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float decel;
    public GameObject boxColliderPrefab;
    public GameObject circleColliderPrefab;
    [SerializeField]private bool isWind = true;
    private int equippedWindItem = 1;
    private int equippedGravItem = 2;
    private float rangeTimer = 0;
    private float switchTimer = 0;
    [SerializeField]private float switchCooldown = 0;
    private bool startTimer = false;
    private GameObject activatedCollider;
    [SerializeField] private GameObject otherChar;
    [SerializeField]private bool controlled;
    [SerializeField]private Sprite windSprite;
    [SerializeField]private Sprite gravSprite;
    [SerializeField]public List<Ability> abilities = new List<Ability>();
    
    // Start is called before the first frame update
    void Start()
    {
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
            if (Mathf.Abs(rgb.velocity.magnitude) <= maxVelocity)
                rgb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * accel, ForceMode2D.Force);
            else
                rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel*Time.deltaTime);

            if (Input.GetAxis("Vertical") == 0 && rgb.velocity.y != 0)
            {
                rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(rgb.velocity.x, 0), decel*Time.deltaTime);
            }
           

            if (Input.GetMouseButtonDown(1))//Right Click
            {
                StartAbilityAnim();
            }
            if(Input.GetMouseButtonUp(1))
            {
                UseAbility();
            }
            if (Input.GetMouseButtonDown(0)) //LeftClick
            {
                Debug.Log(0);
            }
            if (Input.GetKeyDown(KeyCode.Q) && switchTimer <= 0)
            {

                Switch();
            }
        }
        if (Input.GetAxis("Horizontal") == 0 && rgb.velocity.x != 0)
        {
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, rgb.velocity.y), decel*Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel*Time.deltaTime);
        }

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

    public void BasicAttack()
    {

    }

    public void SetInControl()
    {
        Debug.Log("Set in control called by " + name);
        switchTimer = switchCooldown;
        controlled = true;
    }
    public void Switch()
    {

        Debug.Log("Switch called by " + name);
        rgb.velocity = Vector2.zero;
        controlled = false;
        otherChar.GetComponent<Character>().SetInControl();    
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

public class BuildColliders:EditorWindow
{


    [MenuItem("Window/Generate Colliders")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BuildColliders));
    }

   
    private void OnGUI()
    {
        if(GUILayout.Button("Run Function"))
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
        foreach(Transform t in player.transform)
        {
            Debug.Log(t.name);
            if (t.name.Contains("Range"))
                DestroyImmediate(t.gameObject);
        }
        foreach (Ability a in abilities)
        {
            if(a.colliderType == 0)
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
