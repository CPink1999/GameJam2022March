using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteReader : MonoBehaviour
{
    public string stringpath;
    public StreamReader reader;
    float BPM;
    float NPS;
    float startDelay;
    string barData;
    int[] bar;

    private PillarManager pillars;
    private ProjectileSpawner pSpawner;

    // Start is called before the first frame update
    void Start()
    {
        pillars = GameObject.FindGameObjectWithTag("Pillars").GetComponent<PillarManager>();
        pSpawner = GameObject.FindGameObjectWithTag("ProjectileSpawner").GetComponent<ProjectileSpawner>();

        bar = new int[6];
        reader = new StreamReader(Application.persistentDataPath + stringpath);
        BeginRead();
        
    }

    //Grabs the speed of the song and starts the iteration
    void BeginRead()
    {
        Debug.Log(reader.ReadLine());
        BPM = float.Parse(reader.ReadLine());
        NPS = (1 / (BPM / 60));
        startDelay = (float.Parse(reader.ReadLine()) / 1000);
        Debug.Log(BPM.ToString());

        StartCoroutine(StartDelay());
    }

    void ReadBars()
    {
        if (reader.Peek() > -1)
        {
            //Debug.Log(reader.ReadLine());
            
            StartCoroutine(BarDelay());
            barData = reader.ReadLine();
            StartCoroutine(SpawnProjectiles(barData.ToCharArray()));
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSecondsRealtime(startDelay);
        ReadBars();
    }

    IEnumerator BarDelay()
    {
        yield return new WaitForSecondsRealtime(NPS);
        ReadBars();
    }

    IEnumerator SpawnProjectiles(char[] projectileData)
    {
        //SpawnNotes
        Debug.Log("Attempting to spawn notes");
        for (int i = 0; i < 6; i++)
        {
            Debug.Log(projectileData[i]);
            if(int.Parse(projectileData[i].ToString()) == 1)
            pSpawner.SpawnParryable(pSpawner.origin.position, pillars.GetPillar(i).position);
        }

        yield return new WaitForSeconds(0);
    }
}
