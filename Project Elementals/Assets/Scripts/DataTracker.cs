using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataTracker : MonoBehaviour
{


    private TrackingData currentSessionData;
    private string stringData;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Application data path is " + Application.dataPath);
        currentSessionData = new TrackingData();
        JsonUtility.FromJsonOverwrite(File.ReadAllText(Application.dataPath + "/DataTrackingJSON.txt"), currentSessionData);
    }

    // Update is called once per frame
    

    public void UpdraftIncrement()
    {
        currentSessionData.updraftCounter++;
        File.WriteAllText(Application.dataPath + "/DataTrackingJSON.txt",JsonUtility.ToJson(currentSessionData));
    }

    public void OppressIncrement()
    {
        currentSessionData.opressCounter++;
        File.WriteAllText(Application.dataPath + "/DataTrackingJSON.txt", JsonUtility.ToJson(currentSessionData));
    }

    public void SlamIncrement()
    {
        currentSessionData.slamCounter++;
        File.WriteAllText(Application.dataPath + "/DataTrackingJSON.txt", JsonUtility.ToJson(currentSessionData));
    }

    public void KillIncrement()
    {
        currentSessionData.killCounter++;
        File.WriteAllText(Application.dataPath + "/DataTrackingJSON.txt", JsonUtility.ToJson(currentSessionData));
    }
}

[System.Serializable]
public class TrackingData
{
    public int updraftCounter;
    public int opressCounter;
    public int slamCounter;
    public int killCounter;


}


