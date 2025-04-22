using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    int segmentCount = 50;

    Vector2[] segments;

    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        segments = new Vector2[segmentCount];
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 startPos = transform.position;
        segments[0] = startPos;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, new Vector2(transform.position.x, -4.05f));
    }
}
