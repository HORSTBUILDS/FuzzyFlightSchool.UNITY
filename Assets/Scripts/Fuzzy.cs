using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Fuzzy : MonoBehaviour
{
    public GameObject LeftEye;
    public GameObject RightEye;
    public GameObject Needle1;
    public GameObject Needle2;
    [Header("Trails")]
    public bool IsMoving = false;
    public float moveSpeed =1F;
    public Vector3 NewP1;
    public Vector3 NewP2;
    private Vector3 target;
    private Vector3 P1;
    private Vector3 P2;
    private Vector3 Pos;
    [Header("TrailsVisualized")]
    public GameObject TrailLine;
    public GameObject TrailSphere;
    void Start()
    {
        
        // Animates The Eyes
        StartCoroutine(ScaleLeftBig());
        Invoke("StartRightEye", .2F);
        // Trail Logic
        Pos = this.transform.position;
        P1 = Pos + NewP1;
        P2 = Pos + NewP2;
        target = P1;

        //Trail Visual representation Rendering
        if (IsMoving)
        {
          TrailVisual();
        }
        
    }
    private void Update()
    {
       
        // Trail Logic
        if (IsMoving)
        {
            
            TrailMovement();
            
            
        }
    }
    IEnumerator ScaleLeftBig()
    {
        yield return null;
        LeftEye.transform.DOScale(1.2F, .2F).OnComplete(()=>
        {
            StartCoroutine(ScaleLeftSmall());
        });
        //Needle1
        Needle1.transform.DOScale(1.1F,.1F);
        //Shake Fuzzy generally
        this.gameObject.transform.DOShakeScale(.35F, .5F, 1);

    }
    IEnumerator ScaleLeftSmall()
    {
        yield return null;
        LeftEye.transform.DOScale(.8F, .2F).OnComplete(() =>
        {
            StartCoroutine(ScaleLeftBig());
        });
        //Needle1
        Needle1.transform.DOScale(.85F, .1F);

    }
    void  StartRightEye()
    {
        
        StartCoroutine(ScaleRightBig());
    }
    IEnumerator ScaleRightBig()
    {
         
        yield return null;
        RightEye.transform.DOScale(1.2F, .2F).OnComplete(() =>
        {
            StartCoroutine(ScaleRightSmall());
        });
        //Needle2
        Needle2.transform.DOScale(1.1F, .05F);

    }
    IEnumerator ScaleRightSmall()
    {
        yield return null;
        RightEye.transform.DOScale(.8F, .2F).OnComplete(() =>
        {
            StartCoroutine(ScaleRightBig());
        });
        // Needle2
        Needle2.transform.DOScale(.95F, .05F);
    }
    void TrailMovement()
    {
      
       

       
        if(Vector3.Distance(transform.position,P1)<.1F)
        {
            target = P2;
        }

        if (Vector3.Distance(transform.position, P2) < .1F)
        {
            target = P1;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    void TrailVisual()
    {

        
        //Right Scale
        float value = Vector3.Distance(P1, P2);
        value = value / 2;
     
        TrailLine.transform.localScale = new Vector3 (.3F,value,.3F);

        // Calculate Positiom
        Vector3 TrailPos = (P2 + P1) / 2;
        TrailLine.transform.position = TrailPos;

        // Calculate Rotation
        Vector3 Direction = (P2 - P1);
       

        Quaternion Rotate = Quaternion.LookRotation(Direction);
        TrailLine.transform.rotation = Rotate;
        TrailLine.transform.rotation *= Quaternion.Euler(90,0,0);
        Instantiate(TrailLine);
         
        // Instantintiate Sphere1
        GameObject Sphere1 = Instantiate(TrailSphere);
        Sphere1.transform.position = P1;
        // Instantiate Sphere2
        GameObject Sphere2 = Instantiate(TrailSphere);
        Sphere2.transform.position = P2;


    }
}
