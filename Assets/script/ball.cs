using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBodyComponent;
    private Rigidbody2D rigid;
    public float startTime;
    private float realtime;
    public List<float> NoteList;
    private Vector3 lastpos;
    RaycastHit hit;
    private int noteCounter;
    bool changeDir;
    private LayerMask circle;
    private Vector2 olddir;
    float time,counter,oldtime;
    Vector2 direc;
    void Start()
    {
        realtime = Time.time;
        noteCounter = 0;
        changeDir = false;
        rigidBodyComponent = this.GetComponent<Rigidbody2D>();
        rigid = rigidBodyComponent;
        time = Time.time;
        oldtime = Time.time;
        lastpos = transform.position;
        olddir = transform.position;
        Vector3 p = new Vector3(Random.Range(-100.0f,100.0f), Random.Range(-100.0f, 100.0f), 0);
        Physics2D.queriesStartInColliders = false;
        moveCharacter(p,10f);
        circle = LayerMask.GetMask("circle");
        //Debug.Log(circle);
    }
    void Update()
    {
        realtime += Time.deltaTime;
        if (Time.time-0.08> counter)
        {
            olddir = direc;
            counter = Time.time;
            lastpos = transform.position;
        }
        else
            direc = (transform.position - lastpos).normalized;
        if (changeDir == true && counter == Time.time)
        {

           // Debug.Log(NoteList[0]);
            changeDir = false;
            // Debug.Log(direc);
            //Debug.Log(oldtime - Time.time);
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direc,Mathf.Infinity,circle);
            
            Vector2 postr = new Vector2(transform.position.x, transform.position.y);
            /*if ((hit.point - postr).magnitude > 1f)
            {*/
                float speed = (hit.point - postr).magnitude / (NoteList[noteCounter] + oldtime - realtime);

                Debug.Log(this.name);
                Debug.Log(hit.point);
                Debug.Log(NoteList[noteCounter] + oldtime - realtime);
                Debug.Log(speed);
                //Debug.Log(direc);*/
                moveCharacter(direc, speed);
            /*}
            else
            {
                Vector3 pl= new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), 0);
                while ((hit.point - postr).magnitude<1f)
                {
                    pl= new Vector3(Random.Range(-100.0f, 100.0f), Random.Range(-100.0f, 100.0f), 0);
                    hit = Physics2D.Raycast(transform.position, pl, Mathf.Infinity, circle);
                }
                float acc = (hit.point - postr).magnitude / ((NoteList[noteCounter] + oldtime - realtime)* (NoteList[noteCounter] + oldtime - realtime));
                StartCoroutine(acceleration2d(Time.time, acc * pl / pl.magnitude, hit.point, NoteList[noteCounter] + oldtime)) ;
            }
            */
            noteCounter++;
           // Time.
        }
        //Debug.Log(rigidBodyComponent.velocity);
        //Debug.Log(hit.point.ToString("F4"));
       // Debug.Log(direc);
    }
    /*IEnumerator acceleration2d(float time,Vector3 acc,Vector3 hit,float hittime)
    {
        if (hittime - realtime > 0.1)
        {
            rigidBodyComponent.velocity = acc * (realtime - time);
        }
        else if (hittime - realtime > 0)
            moveCharacter(acc, (hit - transform.position).magnitude / hittime - realtime);
        yield return null;
    }*/
    void moveCharacter(Vector3 direction,float speed)
    {
        // Debug.Log(direction.magnitude);
        //Debug.Log(direction * speed / direction.magnitude);
        
        //rigidBodyComponent.AddForce()
        rigidBodyComponent.velocity = direction * speed /direction.magnitude;
       // Debug.Log(rigidBodyComponent.velocity.magnitude);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        changeDir=true;

        counter = Time.time;
        Debug.Log(this.name);
        Debug.Log(realtime - oldtime);
        //GetComponent<Image>().color = Color.red;
       StartCoroutine( FadeAlphaToZero(0.5f));
        //Debug.Log(Time.time - oldtime);

    }
    IEnumerator FadeAlphaToZero(float duration)
    {
        List<Color> Colors = new List<Color>();
        Colors.Add(new Color(185f/255f, 210f/255f, 214f/255f));
        Colors.Add(new Color(248f / 255f, 177f / 255f, 149f / 255f));
        Colors.Add(new Color(246f / 255f, 114f / 255f, 128f / 255f));
        Colors.Add(new Color(192f / 255f, 108f / 255f, 132f / 255f));
        Colors.Add(new Color(53f / 255f, 92f / 255f, 125f / 255f));

        float time2 = 0f;
        Color startColor = Colors[Random.Range(0,5)];
        Color endColor = Color.white;
        //Debug.Log(startColor)
       // float time = 0;
        while (time2 < duration)
        {
            time2 += Time.deltaTime;
            
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor, endColor, time2 / duration);
            yield return null;
        }
    }
}
    /*float t;
    Vector3 startPosition;
    Vector3 target;
    float timeToReachTarget;
    void Start()
    {
        startPosition = target = transform.position;
        Vector3 p = new Vector3(2,2,2);
        SetDestination(p, 1);
    }
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, target, t);
        //Debug.Log(transform);
    }
    public void SetDestination(Vector3 destination, float time)
    {
        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        target = destination;
    }
}
    */