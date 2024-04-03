using UnityEngine;	
using System.Collections;

public class InteractiveControl : MonoBehaviour {

	public float kHeroSpeed = 20f;
	private float kHeroRotateSpeed = 90f/2f; // 90-degrees in 2 seconds
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Input.GetAxis ("Vertical")  * transform.up * 
									(kHeroSpeed * Time.smoothDeltaTime);
		
		float angle = Input.GetAxis("Horizontal") * (kHeroRotateSpeed * Time.smoothDeltaTime);                    
		transform.Rotate(transform.forward, -angle);
	}
}
