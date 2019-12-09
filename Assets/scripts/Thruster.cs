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

    private bool _isBoostThrust = false;
    
 
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
        _isBoostThrust = Input.GetKey(KeyCode.LeftShift);
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
        print("thrusting");
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        var thrustSpeed = _isBoostThrust ? 80 : ThrustSpeed;
        var thrustVelocity = new Vector3(0, thrustSpeed, 0);
        rigidbody.AddRelativeForce(thrustVelocity);
    }

    private void StopThrust()
    {
        audioSource.Stop();
    }

   

}