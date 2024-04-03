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
        // Vertical: Up/Down-Arrow, or WS-keys
		transform.position += Input.GetAxis ("Vertical")  * transform.up * 
									(kHeroSpeed * Time.smoothDeltaTime);
        // Horizontal: Left/Right-Arrow, or AD-Keys
		transform.position += Input.GetAxis("Horizontal") * transform.right * 
									(kHeroSpeed * Time.smoothDeltaTime);
                                   
		// Fire1 is left-ctrl or LMB
		// Fire2 is left-Alt or RMB
		float angle = Input.GetAxis("Fire1") * (kHeroRotateSpeed * Time.smoothDeltaTime); 
		angle -= Input.GetAxis("Fire2") * (kHeroRotateSpeed * Time.smoothDeltaTime); 
		                   
		transform.Rotate(transform.forward, angle);

	}
}
