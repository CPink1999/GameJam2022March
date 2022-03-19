using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteReader : MonoBehaviour
{
    public string stringpath;
    public StreamReader reader;
    float BPM;

    // Start is called before the first frame update
    void Start()
    {
        reader = new StreamReader(Application.persistentDataPath + stringpath);
        BeginRead();
        
    }

    //Grabs the speed of the song and starts the iteration
    void BeginRead()
    {
        Debug.Log(reader.ReadLine());
        BPM = float.Parse(reader.ReadLine());
        Debug.Log(BPM.ToString());
        ReadBars();
    }

    void ReadBars()
    {
        if (reader.Peek() > -1)
        {
            Debug.Log(reader.ReadLine());
            StartCoroutine(BarDelay());
        }
    }

    public IEnumerator BarDelay()
    {
        yield return new WaitForSecondsRealtime(1 / (BPM / 60));
        ReadBars();
    }
}
