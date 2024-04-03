using UnityEngine;	
using System.Collections;

public class HeroBehavior : MonoBehaviour {

	public float kHeroSpeed = 20f;
    public GameObject mEgg = null;
	private const float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
	// Use this for initialization
	void Start () {
        if (mEgg == null)
            mEgg = Resources.Load("Prefabs/Egg") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        #region motion control
        transform.position += Input.GetAxis ("Vertical")  * transform.up * 
									(kHeroSpeed * Time.smoothDeltaTime);
        transform.Rotate(Vector3.forward, -1f * Input.GetAxis("Horizontal") * 
                                    (kHeroRotateSpeed * Time.smoothDeltaTime));
        #endregion

        GlobalBehavior.sTheGlobalBehavior.ObjectClampToWorldBound(this.transform);

        if (Input.GetKey("space"))  // VS. GetKeyDown <<-- even, one per key press
        { // space bar hit
            GameObject e = Instantiate(mEgg) as GameObject;
            EggBehavior egg = e.GetComponent<EggBehavior>(); // Shows how to get the script from GameObject
            if (null != egg)
            {
                e.transform.position = transform.position;
            }
            GlobalBehavior.sTheGlobalBehavior.IncEggCount();
        }

    }
}
