using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private string reactWithBadTag;

    private Vector3 startingPosition;
    private Quaternion startingRotation;

    // Start is called before the first frame update
    void Start()
    {  
        startingPosition = gameObject.transform.position;
        startingRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void OnCollisionEnter(Collision collision)
    {
        string colliderTag = collision.gameObject.tag;
        print(colliderTag);

        if (reactWithBadTag == colliderTag)
        {
            SceneManager.LoadScene(0);
            gameObject.transform.rotation = startingRotation;
            gameObject.transform.position = startingPosition;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if (colliderTag == "Landing")
        {
            SceneManager.LoadScene(1);
        }
    }
}