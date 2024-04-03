using UnityEngine;
using System.Collections;

public partial class EnemyBehavior : MonoBehaviour {
	
	public float mSpeed = 10f;
    private float mTurnRate = 0.5f;    // needs to compute rate * elapseTime
    private float mWorldRegion = 0.9f; // For colliding with the world bound: 0 to 1
    private float mCenterRegion = 40f; // For computing where to turn back to: 20 to +80
  		
	// Use this for initialization
	void Start () {
		NewDirection();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateFSM();
    }

    public void SetEnemyStates(float worldBound, float centerRegion, float turnRate)
    {
        mWorldRegion = worldBound;
        mCenterRegion = centerRegion;
        mTurnRate = turnRate;
    }

	// New direction will be something completely random!
	private void NewDirection() {
        Vector3 c = Camera.main.transform.position;
        c.x += Random.Range(-mCenterRegion, mCenterRegion);
        c.y += Random.Range(-mCenterRegion, mCenterRegion);
        c.z = 0f;
        PointAtPosition(c, mTurnRate * Time.smoothDeltaTime);
	}

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerCheck(collision.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (mState == EnemyState.ePatrolState)
        {
            mState = EnemyState.eEnlargeState;
            mStateFrameTick = 0;
            GetComponent<SpriteRenderer>().color = kChaseColor;
        }
    }

}
