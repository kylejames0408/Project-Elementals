using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : ElementalReaction
{
    private Rigidbody2D rgb;

    //UI
    [SerializeField] private Scrollbar[] scrollbars;

    //Movement and Patrol
    [SerializeField] private float accel;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float decel;
    [SerializeField] private float sojournTime;
    [SerializeField] private GameObject[] patrolPos;
    private bool isPatrol;
    private int patrolOrder;
    private float tempAccel;
    private float tempSojourn;
    private DataTracker dataTracker;//MERGE
    //Define cambat status of enemy
    [SerializeField] private float HP;
    private float maxHP, Intensity_wind, Intensity_grav, Intensity_control;


    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        dataTracker = GameObject.Find("DataTracker").GetComponent<DataTracker>();//MERGE
        //Patrol and sojourn time
        patrolOrder = 0;
        if(accel <= 0)
        {
            isPatrol = false;
        }
        else
        {
            isPatrol = true;
            
        }
        tempSojourn = sojournTime;
        tempAccel = accel;

        currentStatus = Status.idle;
        Intensity_wind = 0.0f;
        Intensity_grav = 0.0f;
        maxHP = HP;
        //int i = ChangeStatus();
        //Debug.Log("changed to " + i);
        //currentStatus = Status.updrafted;
        //takeDamage(0, 0, Status.high_gravity);
    }

    // Update is called once per frame
    void Update()
    {
        //UI update
        scrollbars[0].size = HP / maxHP;
        scrollbars[1].size = Intensity_wind / 10;
        scrollbars[2].size = Intensity_grav / 10;

        //Update Status with elemental remain
        /*
        if (Intensity_grav > 0)
            Intensity_grav -= Time.deltaTime;
        else
            Intensity_grav = 0;

        if (Intensity_wind > 0)
            Intensity_wind -= Time.deltaTime;
        else
            Intensity_wind = 0;

        if (Intensity_control > 0)
            Intensity_control -= Time.deltaTime;
        else
            Intensity_control = 0;

        if(Intensity_control<=0 && Intensity_grav<=0 && Intensity_wind <= 0)
        {
            currentStatus = Status.idle;
        }
        */
        StatusInflunce();

    }

    private void Move(float weaken)
    {
        if (tempSojourn > 0 && !isPatrol)
        {
            accel = 0;
            tempSojourn -= Time.deltaTime;
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), 0.02f);
            //Debug.Log("!isPatrol");
        }
        else
        {
            accel = tempAccel;
            tempSojourn = sojournTime;
            isPatrol = true;
        }

        if (Mathf.Abs(rgb.velocity.magnitude) <= maxVelocity && currentStatus!=Status.pushed) //MERGE
            rgb.AddForce(Vector3.Normalize(patrolPos[patrolOrder].transform.position - transform.position)* accel * weaken, ForceMode2D.Force);
        else
            rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel);


    }
    


    private void OnTriggerEnter2D(Collider2D others)
    {
        if (others.name == patrolPos[patrolOrder].gameObject.name)
        {
            if (patrolOrder + 1 == patrolPos.Length)
            {
                patrolOrder = 0;
                isPatrol = false;
            }
            else
            {
                patrolOrder += 1;
                isPatrol = false;
                //Debug.Log("Go to next pos" +patrolOrder);
            }
        }
        else if (others.name == "Wind_Attack_1")
        {
            TakeDamage(5); // change this to player status
        }
        else if(others.name == "Grav_Attack_1")
        {
            TakeDamage(5); //// change this to player status
        }
    }

    public void TakeDamage(float damage)
    {

        /*currentStatus = (Status)ChangeStatus(attackEffect);
        Debug.Log("new status " + currentStatus + " Intnsity is " + effectIntensity);
        if((int)currentStatus < 4 && (int)currentStatus > 0)
        {
            Intensity_grav = effectIntensity;
            Intensity_wind = Intensity_control = 0;
            HP -= damage;
            Debug.Log("current HP is " + HP);
        }
        else if (currentStatus == Status.idle)
        {
            HP -= damage;
            Debug.Log("current HP is " + HP);
        }
        else if(currentStatus == Status.updraft_increase)
        {
            Intensity_control = 0.0f; // based on the effect
            Intensity_wind = 0.0f;
            Intensity_grav = 0.0f;
            HP -= 2 * damage;
            Debug.Log("current HP is " + HP);
        }
        else // change this later
        {
            Intensity_wind = effectIntensity;
            Intensity_grav = Intensity_control = 0;
            HP -= damage;
            Debug.Log("current HP is " + HP);
        }*/
        HP -= damage;
    }

    private void StatusInflunce()
    {
        switch (currentStatus)
        {
            case Status.idle:
                GetComponentInChildren<Animator>().SetInteger("Status", (int)currentStatus);
               // GetComponent<BoxCollider2D>().enabled = true;
                Move(1);
                break;
            case Status.high_gravity:
                Move(0.05f);
                GetComponentInChildren<Animator>().SetInteger("Status", (int)currentStatus);
                rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), decel);
                break;
            case Status.updrafted:
                Move(0);
                //GetComponent<BoxCollider2D>().enabled = false;
                GetComponentInChildren<Animator>().SetInteger("Status", (int)currentStatus);
                rgb.velocity = Vector2.Lerp(rgb.velocity, new Vector2(0, 0), 0.02f);
                break;
            case Status.updraft_increase:
                Move(0);
                GetComponentInChildren<Animator>().SetInteger("Status", (int)currentStatus);
                break;
            case Status.pushed://MERGE
                Move(0);
                if (rgb.velocity == Vector2.zero)
                    currentStatus = Status.idle;
                break;

        }

        if (HP <= 0)
        {
            currentStatus = Status.idle;
            GetComponentInChildren<Animator>().SetTrigger("Died");
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void HitByAbility(int abilityRefNum)
    {
        Debug.Log("Enemy " + name + " was hit with abililty number " + abilityRefNum);
        switch(abilityRefNum)
        {
            case 1:
                if (currentStatus == Status.idle)
                {//MERGE
                    currentStatus = Status.updrafted;
                    dataTracker.UpdraftIncrement();//MERGE
                }//MERGE
                break;
            case 2:
                if (currentStatus == Status.idle)
                {//MERGE
                    currentStatus = Status.high_gravity;
                    dataTracker.OppressIncrement();//MERGE
                }//MERGE
                else if (currentStatus == Status.updrafted)
                {//MERGE
                    currentStatus = Status.updraft_increase;
                    dataTracker.SlamIncrement();//MERGE
                }//MERGE
                break;
            case 3:
                if (currentStatus == Status.idle)
                {
                    currentStatus = Status.pushed;
                    rgb.velocity = Vector2.zero;
                    float xAxisSignToPush = Mathf.Sign(transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x);
                    rgb.AddForce(new Vector2(1500*xAxisSignToPush, 0), ForceMode2D.Force);
                }
                break;
            
        }
    }
}
