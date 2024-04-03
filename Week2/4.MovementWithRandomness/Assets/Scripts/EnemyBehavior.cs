using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	public float mSpeed = 20f;
    public SliderWithEcho mTurnRate;  // needs to compute rate * elapseTime
    public SliderWithEcho mWorldRegion; // For colliding with the world bound: 0 to 1
    public SliderWithEcho mCenterRegion; // For computing where to turn back to: 20 to +80
  		
	// Use this for initialization
	void Start () {
        Debug.Assert(mTurnRate != null);
        Debug.Assert(mWorldRegion != null);
        Debug.Assert(mCenterRegion != null);
		NewDirection();
	}
	
	// Update is called once per frame
	void Update () {
        GlobalBehavior globalBehavior = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
        GlobalBehavior.WorldBoundStatus status =
			globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds, mWorldRegion.value());
			
		if (status != GlobalBehavior.WorldBoundStatus.Inside) {
			Debug.Log("collided position: " + this.transform.position);
			NewDirection();
		}

        transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
    }

	// New direction will be something completely random!
	private void NewDirection() {
        Vector3 c = Camera.main.transform.position;
        float offset = mCenterRegion.value();
        c.x += Random.Range(-offset, offset);
        c.y += Random.Range(-offset, offset);
        c.z = 0f;
        PointAtPosition(c, mTurnRate.value() * Time.smoothDeltaTime);
	}

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

}
