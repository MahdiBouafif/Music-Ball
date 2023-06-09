using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongFormatter : MonoBehaviour
{
    // Start is called before the first frame update
    public List<float> notetime = new List<float>();
    void Start()
    {
        string strCmdText;
        strCmdText = "C:\\Users\\mahdi\\Downloads\\125.mid  C:\\Users\\mahdi\\MusicBall\\Assets\\Resources\\mm.csv";
       // Debug.Log(strCmdText);
        System.Diagnostics.Process.Start(@"C:\Users\mahdi\Midicsv.exe", strCmdText); //Start cmd process

        List<Dictionary<string, object>> data = CSVReader.Read("mm");
        float tempo=0, pqn=0;
        
        for (int i = 0; i < data.Count; i++)
        {
           // Debug.Log(data[i]["name"].ToString());
            if (data[i]["name"].ToString() == " Tempo")
                tempo = int.Parse(data[i]["chaine"].ToString());
            if(data[i]["name"].ToString()==" Header")
                pqn= int.Parse(data[i]["velocity"].ToString());
        }
        float ticks_per_quarter = pqn;
        float 탎_per_quarter = tempo;
        float 탎_per_tick = 탎_per_quarter / ticks_per_quarter;
        float seconds_per_tick = 탎_per_tick / 1000000;
        float seconds = 5 * seconds_per_tick;
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["name"].ToString() == " Note_on_c")
                notetime.Add(int.Parse(data[i]["time"].ToString()) * seconds_per_tick);
        }
        notetime.Sort();
        GameObject ball = Resources.Load("ball") as GameObject;
        int nbballs = 15;
        for (int i = 0; i < nbballs; i++)
        {

            GameObject go = Instantiate(ball);
            go.transform.position = new Vector3(0, 0, 1);
            go.gameObject.name =(i+1).ToString();
            List<float> notes= new List<float>();
            
            for(int j = i; j < notetime.Count; j += nbballs)
            {
                notes.Add(notetime[j]);
            }
           // Debug.Log(notes[0]);
            go.GetComponent<ball>().NoteList = notes;
            go.GetComponent<ball>().startTime = Time.time;
        }
        GameObject.Find("musicPlayer").GetComponent<DemoMPTK.TheSimplestMidiPlayer>().started = true;

        // Debug.Log(notetime[2]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
