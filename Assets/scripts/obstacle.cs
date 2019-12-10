using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    float MovementOffset;
    [SerializeField]
    bool isOffKey;
    [SerializeField]
    float Period = 2f;
    [SerializeField]
    Vector3 MovementVector;
    private Vector3 StartPosition;
    private float multiplyOffKey => isOffKey ? 1f : -1f;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var cycles = Time.time / Period;
        const float Tau = Mathf.PI * 2;

        float sign = Mathf.Sin(cycles * Tau);

        MovementOffset = sign / 2f + 0.5f;
        MovementOffset *= multiplyOffKey;
        var offset = MovementVector * MovementOffset;

        transform.position = StartPosition + offset;
    }
}
