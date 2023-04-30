using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public static Vector2 UPboundary, LBound;
    private float halfCamHt, halfCamWt;

    // Start is called before the first frame update
    void Start()
    {
        halfCamHt = Camera.main.orthographicSize;
        halfCamWt = halfCamHt * Camera.main.aspect;      
    }

    // Update is called once per frame
    void Update()
    { 
        UPboundary = new Vector2( halfCamWt + transform.position.x,
                                halfCamHt + transform.position.y);
        LBound = new Vector2(transform.position.x - halfCamWt,
                             transform.position.y - halfCamHt);
    }
}
