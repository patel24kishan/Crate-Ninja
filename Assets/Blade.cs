using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCamera; 
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;
    private bool slicing;
    public float minBladeVelocity=0.01f;
    public Vector3 direction { get; private set; }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Awake()
    {
        mainCamera = Camera.main;
        bladeCollider =GetComponent<Collider>();
        bladeTrail=GetComponentInChildren<TrailRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }else if(Input.GetMouseButtonUp(0)) {
            StopSlicing();
        }
        else if(slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 bladePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        bladePosition.z = 0f;
    
        transform.position = bladePosition;

        slicing = true;
        bladeCollider.enabled= true;
        bladeTrail.Clear();
    }
    private void StopSlicing()
    {
        slicing= false;
        bladeCollider.enabled= false;
        bladeTrail.Clear();
    }

    private void ContinueSlicing()
    {
        Vector3 bladePosition=mainCamera.ScreenToWorldPoint(Input.mousePosition);
        bladePosition.z = 0f;

        direction= bladePosition - transform.position;

        float velocity=direction.magnitude/Time.deltaTime;
        bladeCollider.enabled = velocity > minBladeVelocity;

        transform.position= bladePosition;


    }
}
