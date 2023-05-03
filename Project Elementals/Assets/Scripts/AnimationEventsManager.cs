using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsManager : ElementalReaction
{
    private DataTracker dataTracker;
    private void Start()
    {
        dataTracker = GameObject.Find("DataTracker").GetComponent<DataTracker>();
    }
    // Start is called before the first frame update
    public void FellDown()
    {
        transform.parent.gameObject.GetComponent<Enemy>().currentStatus = Status.idle;
    }

    public void SlammedDown()
    {
        transform.parent.gameObject.GetComponent<Enemy>().currentStatus = Status.idle;
        transform.parent.gameObject.GetComponent<Enemy>().TakeDamage(30);
        
    }

    public void Idle()
    {
        transform.parent.gameObject.GetComponent<Enemy>().currentStatus = Status.idle;
    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void DeathAnimFinished()
    {
        Destroy(transform.parent.gameObject);
        dataTracker.KillIncrement();
    }

    public void RockDown()
    {
        GetComponent<Animator>().SetBool("RockUp", false);
    }
}
