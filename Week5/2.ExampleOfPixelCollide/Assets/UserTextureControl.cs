using UnityEngine;	
using System.Collections;

public class UserTextureControl : MonoBehaviour {
	
	private float kMoveSpeed = 2f;
	private float kRotateSpeed = -45; // 
		
	// Update is called once per frame
	void Update () {
		transform.position += Input.GetAxis ("Vertical")  * transform.up * (kMoveSpeed * Time.smoothDeltaTime);
		transform.Rotate(Vector3.forward, Input.GetAxis("Horizontal") * (kRotateSpeed * Time.smoothDeltaTime));
        // Debug.Log("User Input");	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision Happened!");
    }
}
