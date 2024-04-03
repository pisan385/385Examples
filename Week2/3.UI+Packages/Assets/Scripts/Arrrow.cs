using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrrow : MonoBehaviour
{
    public GameObject MyTarget = null;
    public SliderWithEcho mTurnRate = null;  // multiplied with elapseTime  range: 2 to 60
    private const float MySpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(MyTarget != null);
        Debug.Log("Arrow: Started");
    }

    // Update is called once per frame
    void Update()
    {
        PointAtPosition(MyTarget.transform.position, mTurnRate.value() * Time.smoothDeltaTime);
        transform.position += MySpeed * Time.smoothDeltaTime * transform.up;
    }

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

}
