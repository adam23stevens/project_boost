using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    KeyCode DEBUG_SKIP_LEVEL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Debug.isDebugBuild && Input.GetKeyUp(DEBUG_SKIP_LEVEL))
        {
            SceneManager.LoadScene(1);
        }
    }
}
