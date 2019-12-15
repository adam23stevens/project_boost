using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    KeyCode DEBUG_SWITCH_COLLISION;
    [SerializeField]
    private string reactWithBadTag;
    [SerializeField]
    private AudioClip engineClip;
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private AudioClip successClip;
    [SerializeField]
    private float thrustSpeed;
    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private ParticleSystem failureParticle;
    [SerializeField]
    private ParticleSystem successParticle;
    [SerializeField]
    private ParticleSystem thrustParticle_left;
    [SerializeField]
    private ParticleSystem thrustParticle_right;

    public enum State {Alive, Dying, Transcending };
    public State state = State.Alive;

    private bool isPlayingSound;
    private bool isPlayingMoves;
    private bool isDebugCollision;
    private AudioSource audioSource;
    private new Rigidbody rigidbody;
    private string isDebugCollisionString => isDebugCollision && Debug.isDebugBuild ? "ON" : "OFF";
    
    private bool isThrustLeft =>
        Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D);

    private bool isThrustRight =>
        Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A);

    private bool isThrustingUp =>
        Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D);

    public bool IsPlayingBack;

    // Start is called before the first frame update
    void Start()
    { 
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(DEBUG_SWITCH_COLLISION))
        {
            isDebugCollision = !isDebugCollision;
            print($"Debug Collision {isDebugCollisionString}");
        }

        if (isThrustLeft)
        {
            IsPlayingBack = false;
            Invoke("RotateLeft", 5f);
        }
        else if (isThrustRight)
        {
            IsPlayingBack = false;
            Invoke("RotateRight", 5f);
        }
        else if (isThrustingUp)
        {
            IsPlayingBack = false;
            Invoke("Thrust", 5f);
        }
        else if (!IsPlayingBack)
        {
            thrustParticle_left.Stop();
            thrustParticle_right.Stop();
            StopSoundThrust();
        }
        
    }

    private void RotateLeft()
    {
        IsPlayingBack = true;
        transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        SoundThrust();
        thrustParticle_left.Play();
    }
    private void RotateRight()
    {
        IsPlayingBack = true;
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        SoundThrust();
        thrustParticle_right.Play();
    }
    private void Thrust()
    {
        IsPlayingBack = true;
        rigidbody.velocity += transform.up * Time.deltaTime * thrustSpeed;
        SoundThrust();
        thrustParticle_left.Play();
        thrustParticle_right.Play();
    }

    private void StopPlayback()
    {
        isPlayingMoves = false;
        CancelInvoke();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || isDebugCollision) return;

        string colliderTag = collision.gameObject.tag;
        
        if (colliderTag == reactWithBadTag)
        {
            ProcessDeath();
        }

        if (colliderTag == "Target")
        {
            rigidbody.useGravity = false;
        }

        if (colliderTag == "Landing")
        {
            ProcessSuccess();
        }
    }

    public void SoundThrust() 
    {
        if (!isPlayingSound)
        {
            audioSource.PlayOneShot(engineClip);
            isPlayingSound = true;
        }
     }
    
    public void StopSoundThrust()
    {
        audioSource.Stop();
        isPlayingSound = false;
    }

    private void ProcessDeath()
    {
        state = State.Dying;
        Invoke("ReloadScene", 1f);

        audioSource.Stop();
        audioSource.PlayOneShot(deathClip);

        failureParticle.Play();
    }


    private void ProcessSuccess()
    {
        state = State.Transcending;
        Invoke("LoadNextScene", 1f);

        audioSource.Stop();
        audioSource.PlayOneShot(successClip);

        successParticle.Play();
    }

    private void ReloadScene()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private void LoadNextScene()
    {
        var activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(1);
        
        SceneManager.LoadScene(0);
     }
}
