using UnityEngine;
using System.Collections;

public class ShowOriginCorner : MonoBehaviour {

	private float mInitSizeX = 0f;
    private float mInitSizeY = 0f;

    private GameObject mIndicator = null;

    // Use this for initialization
	void Start () {
        Bounds b = GetComponent<SpriteRenderer>().bounds;
        mInitSizeX = b.size.x / transform.localScale.x;
        mInitSizeY = b.size.y / transform.localScale.y;

        if (mIndicator == null)
        {
            GameObject g = Resources.Load("Prefabs/Target") as GameObject;
            mIndicator = Instantiate(g) as GameObject;
        }

	}
	
	// Update is called once per frame
	void Update () {
        mIndicator.transform.position =
            transform.position
            - mInitSizeX * 0.5f * transform.localScale.x * transform.right
            - mInitSizeY * 0.5f * transform.localScale.y * transform.up;

        /*
        Debug.Log("Position: " + transform.position);
        Debug.Log("  target: " + mIndicator.transform.position);
        Debug.Log(" ");                
         */
	}
}
