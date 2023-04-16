using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.Rendering;

public class Manager : MonoBehaviour
{
    public Transform Player;

    public float spawnInterval = .1F;
    public float spawnDistance = 100F;
    private float nextSpawnTime = .1F;


    //public Transform TunnelEnd;
    public GameObject CurrentTunnel;
    public List<GameObject> StageList;
    //GameObject NewTunnel;
    private Vector3 SpawnPosition;
    private GameObject Tunnel;
    void Start()
    {
        SpawnPosition = CurrentTunnel.transform.position;
        Tunnel = CurrentTunnel;
    }

     
    void Update()
    {
     if(Time.time >= nextSpawnTime)
        {
            SpawnTunnel();
        }
    }
    void SpawnTunnel()
    {
        // Calculate the distance on the Z Pos
        if (Tunnel.layer == 6)
        { spawnDistance = 50; }
        else if (Tunnel.layer == 7)
        { spawnDistance = 100; }
        else if (Tunnel.layer == 8)
        { spawnDistance = 150; }
        else if (Tunnel.layer == 9)
        { spawnDistance = 200; }

        int RandomIndex = Random.Range(0, StageList.Count);
        Tunnel =   Instantiate(StageList[RandomIndex]);
      

        // Increase Z Position
        SpawnPosition.z += spawnDistance;
        Tunnel.transform.position = SpawnPosition;
        

        nextSpawnTime = spawnInterval + Time.time;
        //Destroy Tunnel
        StartCoroutine(DestroyObj(Tunnel));
    }
  
    IEnumerator DestroyObj(GameObject Object)
    {
        yield return new WaitForSeconds(20F);
        if(Object != null)
        {
          Destroy(Object);
        }
        

    }

    
}