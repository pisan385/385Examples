using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSupport : MonoBehaviour
{
    private Camera mTheCamera;
    private Bounds mWorldBound;  // Computed bound from the camera
    // Start is called before the first frame update
    void Start()
    {
        mTheCamera = gameObject.GetComponent<Camera>();
        mWorldBound = new Bounds();
        float maxY = mTheCamera.orthographicSize;
        float maxX = mTheCamera.orthographicSize * mTheCamera.aspect;
        float sizeX = 2 * maxX;
        float sizeY = 2 * maxY;
        Vector3 c = mTheCamera.transform.position;
        c.z = 0.0f;
        mWorldBound.center = c;
        mWorldBound.size = new Vector3(sizeX, sizeY, 1f);
        Debug.Log("mWorldBound: " + mWorldBound); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Bounds GetWorldBound() { return mWorldBound; }
    
    public bool isInside(Bounds b1)
    {
        return isInsideBounds(b1, mWorldBound);

    }

    // b1 is inside b2
    public bool isInsideBounds(Bounds b1, Bounds b2)
    {
        return (b1.min.x < b2.max.x) && (b1.max.x > b2.min.x) &&  
               (b1.min.y < b2.max.y) && (b1.max.y > b2.min.y);
    }
}
