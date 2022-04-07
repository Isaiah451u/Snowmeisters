using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    public GameObject follow;
    private float length, startPosition;
    public float parralax;
    public float offsetX;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = follow.transform.position.x * (1 - parralax);
        float dist = follow.transform.position.x * parralax;

        transform.position = new Vector3(startPosition + dist + offsetX, follow.transform.position.y + offsetY, transform.position.z);

        if (temp > startPosition + length) { startPosition += length; }
        else if (temp < startPosition - length) { startPosition -= length; }
    }
}
