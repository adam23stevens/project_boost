using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_ui : MonoBehaviour
{
    public Rocket Rocket;

    public GameObject arrow_left;
    public GameObject arrow_right;
    public GameObject arrow_up;
    [SerializeField]
    private Vector3 MAX_VECTOR_SIZE;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Rocket.IsPlayingBack)
        {
            SwitchArrows(true);

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                var increasedScale = UpdateScale(arrow_left.transform.localScale);
                UpdateMaterial(arrow_left);
                arrow_left.transform.localScale = increasedScale;
            }
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                var increasedScale = UpdateScale(arrow_right.transform.localScale);
                UpdateMaterial(arrow_right);
                arrow_right.transform.localScale = increasedScale;
            }
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                var increasedScale = UpdateScale(arrow_up.transform.localScale);
                UpdateMaterial(arrow_up);
                arrow_up.transform.localScale = increasedScale;
            }
        }
        if (Rocket.IsPlayingBack)
        {
            SwitchArrows(false);
        }
    }

    private Vector3 UpdateScale(Vector3 initialScale)
    {
        var newVector3 = new Vector3(initialScale.x * 1.02f, initialScale.y * 1.02f, 1);

        return (newVector3.x < MAX_VECTOR_SIZE.x && newVector3.y < MAX_VECTOR_SIZE.y)
                ? newVector3
                : initialScale;
    }

    private void UpdateMaterial(GameObject gObject)
    {
        var colour = gObject.GetComponentInChildren<MeshRenderer>().material.color;
        print(colour);
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

    private void SwitchArrows(bool active)
    {
        //arrow_left.SetActive(active);
        //arrow_right.SetActive(active);
        //arrow_up.SetActive(active);
    }
}
