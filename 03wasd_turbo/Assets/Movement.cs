 using System.Collections;
 using System.Collections.Generic;
 using System.Reflection;
 using System.Linq;
 using UnityEngine;
 using System;


// Shift - doubles speed while pressed
// LeftControl - increases speed to 40 for 2 seconds, checks time itself
// Tab - increases speed to 50, starts coroutine to change it back
// 1 -increases speed to 30, uses Invoke to change it back


public class Movement : MonoBehaviour
{
    public KeyCode jumpKey;

    public float speed = 10f;

    private float turboTimeDuration = 2.0f;
    private float nextTurbo = 0.0f;

    bool turboMode = false;

    void resetSpeed()
    {
        speed = 10;
        Debug.Log("End of resetSpeed, setting speed to " + speed);
    }

    // must be called with StartCoroutine
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    
        // Code to execute after the delay
        speed = 10;
        Debug.Log("End of coroutine, setting speed to " + speed);
    }

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            speed = 35;
            Debug.Log("Activating 1 based speedup" + speed);
            Invoke("resetSpeed", 5.0f);
        }

        if (Input.GetKeyDown(KeyCode.Tab)) {
            speed = 50;
            Debug.Log("Activating TAB based speedup" + speed);
            StartCoroutine(ExecuteAfterTime(3));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > nextTurbo) {
            Debug.Log("Activating 2 sec turbo mode " + speed);
            nextTurbo = Time.time + turboTimeDuration;
            turboMode = true;
            speed = 40;
        }

        if (turboMode && Time.time > nextTurbo) {
            speed = 10;
            turboMode = false;
            Debug.Log("Ending 2 sec turbo mode " + speed);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            speed *= 2;
            Debug.Log("Speed increased to " + speed);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed /= 2;
            Debug.Log("Speed decreased to " + speed);
        }
        // "w" can be replaced with any key
        // this section moves the character up
        if (Input.GetKey(KeyCode.W))
        {
            pos.y += speed * Time.deltaTime;
        }

        // "s" can be replaced with any key
        // this section moves the character down
        if (Input.GetKey("s"))
        {
            pos.y -= speed * Time.deltaTime;
        }

        // "d" can be replaced with any key
        // this section moves the character right
        if (Input.GetKey("d"))
        {
            pos.x += speed * Time.deltaTime;
        }

        // "a" can be replaced with any key
        // this section moves the character left
        if (Input.GetKey("a"))
        {
            pos.x -= speed * Time.deltaTime;
        }


        transform.position = pos;
    }
}
