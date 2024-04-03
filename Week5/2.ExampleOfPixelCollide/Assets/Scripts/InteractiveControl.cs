using UnityEngine;	
using System.Collections;

public class InteractiveControl : MonoBehaviour {
	private float kHeroSpeed = 2f;
	private float kHeroRotateSpeed = 10f/2f; 
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        #region User Position Control

        float h = 0f, v = 0f, r=0f;

        if (Input.GetKey(KeyCode.W))
            v = kHeroSpeed;
        if (Input.GetKey(KeyCode.S))
            v += -kHeroSpeed;

        if (Input.GetKey(KeyCode.A))
            h = -kHeroSpeed;
        if (Input.GetKey(KeyCode.D))
            h += kHeroSpeed;

        if (Input.GetKey(KeyCode.Q))
            r += kHeroRotateSpeed;
        if (Input.GetKey(KeyCode.E))
            r -= kHeroRotateSpeed;

        transform.position += v * transform.up * (kHeroSpeed * Time.smoothDeltaTime);
        transform.position += h * transform.right * (kHeroSpeed * Time.smoothDeltaTime);

        transform.Rotate(Vector3.forward, r * (kHeroRotateSpeed * Time.smoothDeltaTime));
        #endregion

    }
}
