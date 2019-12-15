using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_ui : MonoBehaviour
{
    public Rocket Rocket;
    public Transform Arrow_Recording_Transform;
    public GameObject arrow_left;
    public GameObject arrow_right;
    public GameObject arrow_up;

    [SerializeField]
    private Vector3 MAX_VECTOR_SIZE;

    private Vector3 arrow_up_scale_starting;
    private Vector3 arrow_left_scale_starting;
    private Vector3 arrow_right_scale_starting;

    private Color arrow_up_colour_starting;
    private Color arrow_left_colour_starting;
    private Color arrow_right_colour_starting;

    private GameObject arrow_recording;

    // Start is called before the first frame update
    void Start()
    {
        arrow_up_scale_starting = arrow_up.transform.localScale;
        arrow_left_scale_starting = arrow_left.transform.localScale;
        arrow_right_scale_starting = arrow_right.transform.localScale;

        arrow_up_colour_starting = arrow_up.GetComponentInChildren<MeshRenderer>().material.color;
        arrow_left_colour_starting = arrow_left.GetComponentInChildren<MeshRenderer>().material.color;
        arrow_right_colour_starting = arrow_right.GetComponentInChildren<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Rocket.IsPlayingBack)
        { 
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                var increasedScale = UpdateScale(arrow_left.transform.localScale);
                UpdateMaterial(arrow_left);

                arrow_left.transform.localScale = increasedScale;
                arrow_up.transform.localScale = arrow_up_scale_starting;
                arrow_right.transform.localScale = arrow_right_scale_starting;

                transform.localScale = new Vector3(1, 1, 1);
                arrow_recording = arrow_up;
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                var increasedScale = UpdateScale(arrow_right.transform.localScale);
                UpdateMaterial(arrow_right);

                arrow_right.transform.localScale = increasedScale;
                arrow_up.transform.localScale = arrow_up_scale_starting;
                arrow_left.transform.localScale = arrow_left_scale_starting;

                transform.localScale = new Vector3(1, 1, 1);
                arrow_recording = arrow_right;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                var increasedScale = UpdateScale(arrow_up.transform.localScale);
                UpdateMaterial(arrow_up);

                arrow_up.transform.localScale = increasedScale;
                arrow_left.transform.localScale = arrow_left_scale_starting;
                arrow_right.transform.localScale = arrow_right_scale_starting;

                transform.localScale = new Vector3(1, 1, 1);
                arrow_recording = arrow_up;
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                print($"recording {arrow_recording.name}");
                

            }
            else
            {
                ResetAll();
            }
        }
        if (Rocket.IsPlayingBack)
        {

        }
    }

    private Vector3 UpdateScale(Vector3 initialScale)
    {
        if (System.Math.Abs(initialScale.x) < Mathf.Epsilon)
        {
            initialScale.x = 1;
        }
        if (System.Math.Abs(initialScale.y) < Mathf.Epsilon)
        {
            initialScale.y = 1;
        }
        var newVector3 = new Vector3(initialScale.x * 1.02f, initialScale.y * 1.02f, 1);

        return (newVector3.x < MAX_VECTOR_SIZE.x && newVector3.y < MAX_VECTOR_SIZE.y)
                ? newVector3
                : initialScale;
    }

    private void ResetAll()
    {
        //transform.localScale = new Vector3(0, 0, 0);
        arrow_up.transform.localScale = arrow_up_scale_starting;
        arrow_left.transform.localScale = arrow_left_scale_starting;
        arrow_right.transform.localScale = arrow_right_scale_starting;

        arrow_up.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", arrow_up_colour_starting);
        arrow_left.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", arrow_left_colour_starting);
        arrow_right.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", arrow_right_colour_starting);

    }

    private void UpdateMaterial(GameObject gObject)
    {
        var colour = gObject.GetComponentInChildren<MeshRenderer>().material.color;

        if (colour.r < 1.01f)
        {
            colour.r += 0.01f;
        }
        if (colour.g > 0.01f)
        {
            colour.g -= 0.01f;
        }
        if (colour.b > 0.01f)
        {
            colour.b -= 0.01f;
        }

        gObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", colour);
    }

   
}
