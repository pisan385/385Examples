using UnityEngine;	
using System.Collections;

public class CollideWithTexture : MonoBehaviour {
	
	public GameObject mLargeTexObject = null;

	private GameObject mTarget = null;

    private CollidableTexture mMyCollidableTex = null;
            // assume we have such a component!
	private CollidableTexture mOtherCollidableTex = null;
	
	
	// Use this for initialization
	void Start () {
        mMyCollidableTex = GetComponent<CollidableTexture>();
		mOtherCollidableTex = mLargeTexObject.GetComponent<CollidableTexture>();
		if ((null == mMyCollidableTex) || (null == mOtherCollidableTex))
			Debug.Log("Both GameObjects must define CollidableTexture component!");
		if (mTarget == null)
        {
            GameObject g = Resources.Load("Prefabs/Target") as GameObject;
            mTarget = Instantiate(g) as GameObject;
            mTarget.GetComponent<Renderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
			
		if (null != mLargeTexObject) {
			Vector3 pos = Vector3.zero;
			if (mMyCollidableTex.CollideTextures(mOtherCollidableTex, 
			                                    mLargeTexObject.GetComponent<Renderer>().bounds, out pos)) {
				mTarget.GetComponent<Renderer>().enabled = true;
				mTarget.transform.position = pos;
			} else {
				mTarget.GetComponent<Renderer>().enabled = false;
			}
		}
		
	}
}
