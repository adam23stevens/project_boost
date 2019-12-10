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
    private ParticleSystem failureParticle;
    [SerializeField]
    private ParticleSystem successParticle;

    public enum State {Alive, Dying, Transcending };
    public State state = State.Alive;

    private bool isPlayingSound;
    private bool isDebugCollision;
    private AudioSource audioSource;
    private string isDebugCollisionString => isDebugCollision && Debug.isDebugBuild ? "ON" : "OFF";
    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyUp(DEBUG_SWITCH_COLLISION))
        {
            isDebugCollision = !isDebugCollision;
            print($"Debug Collision {isDebugCollisionString}");
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || isDebugCollision) return;

        string colliderTag = collision.gameObject.tag;
        
        if (colliderTag == reactWithBadTag)
        {
            ProcessDeath();
        }

        if (colliderTag == "Landing")
        {
            ProcessSuccess();
        }
    }

    public void Thrust() 
    {
        if (!isPlayingSound)
        {
            audioSource.PlayOneShot(engineClip);
            isPlayingSound = true;
        }
     }
    
    public void StopThrust()
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
