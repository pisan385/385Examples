using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyBehavior : MonoBehaviour
{
    private enum EnemyState
    {
        ePatrolState,
        eEnlargeState,
        eCWRotateState,
        eCCWRotateState,
        eShrinkState
    };

    private const float kSizeChangeFrames = 120f;
    private const float kRotateFrames = 80f;
    private const float kScaleRate = 2f / 60f;// around per second rate
    private const float kRotateRate = 90f/60f;  // in degrees, around per second rate
    private Color kNormalColor = new Color(0.3f, 0.3f, 0.3f, 1.0f);
    private Color kChaseColor = new Color(1f, 1f, 1f, 1f);

    private int mStateFrameTick = 0;
    private EnemyState mState = EnemyState.ePatrolState;
    

    private void UpdateFSM()
    {
        switch (mState)
        {
            case EnemyState.eEnlargeState:
                ServiceEnlargeState();
                break;
            case EnemyState.eCWRotateState:
                ServiceCWState();
                break;
            case EnemyState.eCCWRotateState:
                ServiceCCWState();
                break;
            case EnemyState.eShrinkState:
                ServiceShrinkState();
                break;
            case EnemyState.ePatrolState:
                ServicePatrolState();
                break;
        }
    }

    #region Simple FSM services
    private void ServiceEnlargeState()
    {
        if (mStateFrameTick > kSizeChangeFrames)
        {
            // Transite to next state
            mState = EnemyState.eCWRotateState;
            mStateFrameTick = 0;
        }
        else
        {
            mStateFrameTick++;

            // assume scale in X/Y are the same
            float s = transform.localScale.x;
            s += kScaleRate;
            transform.localScale = new Vector3(s, s, 1);
        }
    }

    private void ServiceShrinkState()
    {
        if (mStateFrameTick > kSizeChangeFrames)
        {
            // Transite to next state
            mState = EnemyState.ePatrolState;
            GetComponent<SpriteRenderer>().color = kNormalColor;
            mStateFrameTick = 0;
        }
        else
        {
            mStateFrameTick++;

            // assume scale in X/Y are the same
            float s = transform.localScale.x;
            s -= kScaleRate;
            transform.localScale = new Vector3(s, s, 1);
        }
    }

    private void ServiceCWState()
    {
        if (mStateFrameTick > kRotateFrames)
        {
            // Transite to next state
            mState = EnemyState.eCCWRotateState;
            mStateFrameTick = 0;
        }
        else
        {
            mStateFrameTick++;

            Vector3 angles = transform.rotation.eulerAngles;
            angles.z += kRotateRate;
            transform.rotation = Quaternion.Euler(0, 0, angles.z);
        }
    }

    private void ServiceCCWState()
    {
        if (mStateFrameTick > kRotateFrames)
        {
            // Transite to next state
            mState = EnemyState.eShrinkState;
            mStateFrameTick = 0;
        }
        else
        {
            mStateFrameTick++;

            Vector3 angles = transform.rotation.eulerAngles;
            angles.z -= kRotateRate;
            transform.rotation = Quaternion.Euler(0, 0, angles.z);
        }
    }
    #endregion

    private void ServicePatrolState()
    {
        GlobalBehavior globalBehavior = GameObject.Find("GameManager").GetComponent<GlobalBehavior>();
        GlobalBehavior.WorldBoundStatus status =
            globalBehavior.ObjectCollideWorldBound(GetComponent<Renderer>().bounds, mWorldRegion);

        if (status != GlobalBehavior.WorldBoundStatus.Inside)
        {
            // Debug.Log("collided position: " + this.transform.position);
            NewDirection();
        }

        transform.position += (mSpeed * Time.smoothDeltaTime) * transform.up;
    }
}
