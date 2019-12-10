using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Thruster : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem engineParticle;

    public float ThrustSpeed;
    public KeyCode ThrustKey;
    new Rigidbody rigidbody;
    
    public Rocket rocket;

    private bool _isBoostThrust = false;
    
 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (rocket.state == Rocket.State.Alive)
        {
            ProcessInput();
        }
        
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
        rocket.Thrust();

        var thrustSpeed = _isBoostThrust ? 80 : ThrustSpeed;
        
        var thrustVelocity = new Vector3(0, thrustSpeed, 0);
        rigidbody.AddRelativeForce(thrustVelocity);

        engineParticle.Play();
    }

    private void StopThrust()
    {
        rocket.StopThrust();

        engineParticle.Stop();
    }

   

}