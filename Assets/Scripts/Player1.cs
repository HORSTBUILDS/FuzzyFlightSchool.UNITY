using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    [Header("Movement")]
    public float Movespeed = 1F;
    public float ZSpeed;
    public float RotatePower = 2F;
    [Header("Clamp Position")]
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    [Header("Rotor Animation")]
    public Transform Pivot;
    public float RotorSpeed = 1F;

    private float lastUpdateTime;
    private Animation anim;
    private bool IsGameOver;
    [Header("UI")]
     
    public RawImage LP1;
    public RawImage LP2;
    public RawImage LP3;
    public ParticleSystem Smoke;
    int LifePoints = 3;
  
    void Start()
    {
        anim = GetComponent<Animation>();
        
    }


    void Update()
    {

         
        float x = Input.GetAxis("Horizontal1");
        float y = Input.GetAxis("Vertical1");

        Move(x, y);

        transform.position += new Vector3(0, 0, ZSpeed * Time.deltaTime);

        RotorBlade();

        // Check if GameIsOver
        if (LifePoints == 0)
        {
            
            GameOver();
        }
    }

    void Move(float x, float y)
    {
        if(IsGameOver != true)
        {
          transform.position += new Vector3(x, y, 0) * Movespeed * Time.deltaTime;
        }
        
        // Clamp Position on x,y
        transform.position = new Vector3 (Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.y,yMin,yMax), transform.position.z);
        Rotate(x, y);
    }
    void Rotate(float x, float y)
    {
        Vector3 RotateValue = new Vector3(-y * RotatePower, x * RotatePower, 0);
        transform.DORotate(RotateValue, .5F);

    }
    void RotorBlade()
    {
         Pivot.rotation *= Quaternion.Euler(0, 0, 10 * RotorSpeed * Time.deltaTime);   
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Fuzzy")
        {

            Hit();
            UI();
        }
    }
    private void Hit()
    {
        // Approves that you cant be hit several Times by same Fuzzy
        if(Time.time - lastUpdateTime >= .6F)
        {
            
            lastUpdateTime = Time.time;
            anim.Play();
            LifePoints--;
        }
      
    }
    private void UI()
    {
        if(LifePoints == 2)
        {
           LP1.color = Color.gray;
        }
        if (LifePoints == 1)
        {
            LP2.color = Color.gray;
        }
        if (LifePoints == 0)
        {
            LP3.color = Color.gray;
         
        }

    }
    void GameOver()
    {
        Debug.Log("sexy");
        IsGameOver = true;
        ZSpeed = 20;

        transform.DOMoveY(transform.position.y - 6,3.5F);
        Smoke.Play();
         
    }


}
