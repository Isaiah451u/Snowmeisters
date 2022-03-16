using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    public GameObject follow;
    private float length, startPosition;
    public float parralax;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = follow.transform.position.x * (1 - parralax);
        float dist = follow.transform.position.x * parralax;

        transform.position = new Vector3(startPosition + dist, follow.transform.position.y + .5f, transform.position.z);

        if (temp > startPosition + length) { startPosition += length; }
        else if (temp < startPosition - length) { startPosition -= length; }
    }
}
