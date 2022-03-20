using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class NoteReader : MonoBehaviour
{
    public TextAsset noteChart;

    public AudioClip songToPlay;
    AudioSource aSource;

    StreamReader reader;

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
        aSource = GetComponent<AudioSource>();
        aSource.clip = songToPlay;

        bar = new int[6];
        reader = new StreamReader(AssetDatabase.GetAssetPath(noteChart));
    }

    //Grabs the speed of the song and starts the iteration
    public void BeginRead()
    {
        Debug.Log(reader.ReadLine());
        BPM = float.Parse(reader.ReadLine());
        NPS = (1 / (BPM / 60));
        startDelay = (float.Parse(reader.ReadLine()) / 1000);
        Debug.Log(BPM.ToString());

        aSource.Play();
        StartCoroutine(StartDelay());
    }

    public void FadeOut(float time)
    {
        StartCoroutine(FadeOutCoroutine(time));
    }

    private IEnumerator FadeOutCoroutine(float time)
    {
        float startVolume = aSource.volume;
        while (aSource.volume > 0)
        {
            aSource.volume -= startVolume * (Time.deltaTime / time);
            yield return null;
        }
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
        for (int i = 0; i < 6; i++)
        {
            if (int.Parse(projectileData[i].ToString()) != 0)
            {
                if (int.Parse(projectileData[i].ToString()) == 1)
                    pSpawner.Spawn(pSpawner.origin.position, pillars.GetPillar(i).position, false);
                else
                    pSpawner.Spawn(pSpawner.origin.position, pillars.GetPillar(i).position, true);
            }
        }

        yield return new WaitForSeconds(0);
    }
}
