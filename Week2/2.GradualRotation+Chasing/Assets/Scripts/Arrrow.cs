using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrrow : MonoBehaviour
{
    public GameObject MyTarget = null;
    private const float MySpeed = 5f;
    private const float kInitRate = 0.5f;
    private const float kDeltaRate = 0.1f;
    private float Rate = kInitRate;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(MyTarget != null);
        Debug.Log("Arrow: Started");
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Immediate Targetting");
            Rate = 60f; // probably close to 1 per frame
        } else if (Input.GetKeyDown(KeyCode.J))
        {
            Rate -= kDeltaRate;
            Rate = Mathf.Max(0f, Rate); // Clampped at 0f
            Debug.Log("Decrease Targetting Rate to:" + Rate);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Rate += kDeltaRate;
            Rate = Mathf.Min(60f, Rate); // Clampped at 60f
            Debug.Log("Increase Targetting Rate to:" + Rate);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Rate = kInitRate;
            Debug.Log("Initialize Targetting Rate to:" + Rate);
        }

        PointAtPosition(MyTarget.transform.position, Rate * Time.smoothDeltaTime);

        transform.position += MySpeed * Time.smoothDeltaTime * transform.up;
    }

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

}
