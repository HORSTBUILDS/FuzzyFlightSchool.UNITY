using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public float smoothTime = .5F;
    public float Offset = 3F;
    private Vector3 Velocity = Vector3.zero;
    public GameObject Cube;
    public float ZSpeed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Cube.transform.position += new Vector3(0, 0, ZSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, Cube.transform.position.z + Offset), ref Velocity, smoothTime);
    }
}
