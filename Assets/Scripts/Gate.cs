using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Gate : MonoBehaviour
{
    GameObject Player;
    GameObject Player2;
    public float Distance;
    public Transform DoorR;
    public Transform DoorL;
   
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocity1 = Vector3.zero;
    public float speed;
   
    private void Start()
    {
        Player = GameObject.Find("Ship");
        Player2 = GameObject.Find("Ship2");
    }
    void Update()
    {
        if(this.transform.position.z - Player.transform.position.z < Distance || this.transform.position.z - Player2.transform.position.z < Distance)
        {
            if (Time.time >= 2)
            {
                OpenGate();
            }
             
        }
    }
    public void OpenGate()
    {   //Door R
        float y = DoorR.transform.position.y;
        float z = DoorR.transform.position.z;
        Vector3 Pos = new Vector3(68.3F, y, z);
        DoorR.transform.position = Vector3.SmoothDamp(DoorR.transform.position, Pos, ref velocity, .01F, speed);

        // Door L
        float yL = DoorL.transform.position.y;
        float zL= DoorL.transform.position.z;
        Vector3 PosL = new Vector3(38.3F, yL, zL);
        DoorL.transform.position = Vector3.SmoothDamp(DoorL.transform.position, PosL, ref velocity1, .01F, speed);
         
    }
   
}
