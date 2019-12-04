using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float ThrustSpeed;
    public float RotateSpeed;
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
        if (Input.GetKey(KeyCode.Space))
        {
            Thrust();
        }
        else
        {
            StopThrust();
        }
        if (Input.GetKey(KeyCode.A))
        {
            Rotate(Rotation.Left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Rotate(Rotation.Right);
        }


    }

    private void Rotate(Rotation rotation)
    {
        var rotateVelocity = new Vector3(0, 0, RotateSpeed * (rotation == Rotation.Left ? 1 : -1) * Time.deltaTime);
        transform.Rotate(rotateVelocity);
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

enum Rotation { Left, Right };