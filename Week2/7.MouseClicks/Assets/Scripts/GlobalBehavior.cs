using UnityEngine;
using System.Collections;

public class GlobalBehavior : MonoBehaviour {

    #region CameraBound Control
    private Bounds mWorldBound;  // this is the world bound
	private Vector2 mWorldMin;	// Better support 2D interactions
	private Vector2 mWorldMax;
	private Vector2 mWorldCenter;
	private Camera mMainCamera;
    #endregion 

    private GameObject mPartolEnemy = null;
    public SliderWithEcho mWorldPercentBound = null;
    public SliderWithEcho mCenterRegion = null;
    public SliderWithEcho mTurnRate = null;
	
	// Use this for initialization
	void Start () {
        #region Camera Bound Control
        mMainCamera = Camera.main; // This is the default main camera
		mWorldBound = new Bounds(Vector3.zero, Vector3.one);
		UpdateWorldWindowBound();
        #endregion 

        Debug.Assert(mWorldPercentBound != null);
        Debug.Assert(mCenterRegion != null);
        Debug.Assert(mTurnRate != null);

        mPartolEnemy = Resources.Load<GameObject>("Prefabs/Enemy");
    }
	
	void Update () {

        if (Input.GetMouseButtonDown(0))  // Left Mouse Botton
        {
            Vector3 p = mMainCamera.ScreenToWorldPoint(Input.mousePosition);
            p.z = 0f;
            GameObject newEnemy = Instantiate(mPartolEnemy);
            newEnemy.transform.position = p;
            EnemyBehavior b = newEnemy.GetComponent<EnemyBehavior>();
            b.SetEnemyStates(mWorldPercentBound.value(), mCenterRegion.value(), mTurnRate.value());
            // Debug.Log("Mouse click:" + p);
        }
			
	}
	
	#region Game Window World size bound support
	public enum WorldBoundStatus {
		CollideTop,
		CollideLeft,
		CollideRight,
		CollideBottom,
		Outside,
		Inside
	};
	
	/// <summary>
	/// This function must be called anytime the MainCamera is moved, or changed in size
	/// </summary>
	public void UpdateWorldWindowBound()
	{
		// get the main 
		if (null != mMainCamera) {
			float maxY = mMainCamera.orthographicSize;
			float maxX = mMainCamera.orthographicSize * mMainCamera.aspect;
			float sizeX = 2 * maxX;
			float sizeY = 2 * maxY;
			float sizeZ = Mathf.Abs(mMainCamera.farClipPlane - mMainCamera.nearClipPlane);
			
			// Make sure z-component is always zero
			Vector3 c = mMainCamera.transform.position;
			c.z = 0.0f;
			mWorldBound.center = c;
			mWorldBound.size = new Vector3(sizeX, sizeY, sizeZ);

			mWorldCenter = new Vector2(c.x, c.y);
			mWorldMin = new Vector2(mWorldBound.min.x, mWorldBound.min.y);
			mWorldMax = new Vector2(mWorldBound.max.x, mWorldBound.max.y);
		}
	}

	public Vector2 WorldCenter { get { return mWorldCenter; } }
	public Vector2 WorldMin { get { return mWorldMin; }} 
	public Vector2 WorldMax { get { return mWorldMax; }}
	
	public WorldBoundStatus ObjectCollideWorldBound(Bounds objBound, float region = 1.0f)
	{
		WorldBoundStatus status = WorldBoundStatus.Inside;
        Vector3 max = region * mWorldBound.max;
        Vector3 min = region * mWorldBound.min;

		if (mWorldBound.Intersects (objBound)) {
			if (objBound.max.x > max.x)
				status = WorldBoundStatus.CollideRight;
			else if (objBound.min.x < min.x)
				status = WorldBoundStatus.CollideLeft;
			else if (objBound.max.y > max.y)
				status = WorldBoundStatus.CollideTop;
			else if (objBound.min.y < min.y)
				status = WorldBoundStatus.CollideBottom;
			else if ((objBound.min.z < mWorldBound.min.z) || (objBound.max.z > mWorldBound.max.z))
				status = WorldBoundStatus.Outside;
		} else 
			status = WorldBoundStatus.Outside;

		return status;
	}
	#endregion 
	
}
