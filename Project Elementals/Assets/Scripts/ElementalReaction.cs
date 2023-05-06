using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalReaction : MonoBehaviour
{
    public enum Status
    {
        idle = 0, // defualt
        high_gravity = 1, // Increase (Increases Gravity)
        low_gravity = 2, // Decrease (Decreases Gravity)
        gravity_3 = 3, // Temp: Neutron Star (Pulls Enemies Together)
        updrafted = 4, // Updraft (Sends Enemies Upward)
        pushed = 5, // Push (Pushes Enemies Away)
        revolved = 6, // Whirlwind (Revolves Enemies Around a Point)
        updraft_increase = 7,//Updraft + high grav

    }

    public Status currentStatus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int ChangeStatus(Status newStatus)
    {
        switch (currentStatus)
        {
            case Status.idle:
                Debug.Log("changed to new");
                return (int)newStatus;
            case Status.updrafted:
                if(newStatus == Status.high_gravity)
                {
                    Debug.Log("Reaction: 7");
                    return (int)Status.updraft_increase;
                }
                Debug.Log("changed to new");
                return (int)newStatus;
            case Status.high_gravity:
                Debug.Log("changed to new");
                return (int)newStatus;
        }

        Debug.Log("Unexpected Status");
        return 99;
    }
}
