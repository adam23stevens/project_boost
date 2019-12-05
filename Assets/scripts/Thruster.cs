using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Thruster : MonoBehaviour
{
    public float ThrustSpeed;
    public KeyCode ThrustKey;
    new Rigidbody rigidbody;
    AudioSource audioSource;
    
 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();   
    }

    private void ProcessInput()
    {
        if (Input.GetKey(ThrustKey))
        {
            Thrust();
        }
        else
        {
            StopThrust();
        }
        

    }

    private void Thrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        } 

        var thrustVelocity = new Vector3(0, ThrustSpeed, 0);
        rigidbody.AddRelativeForce(thrustVelocity);
    }

    private void StopThrust()
    {
        audioSource.Stop();
    }

   

}