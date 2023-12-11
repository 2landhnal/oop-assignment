using UnityEngine;

public static class TransformExtension 
{
    static Vector3 sampleV3;
    static float rot_z;
    static Vector3 diff;

    /// <summary>
    /// Return true if transform.rotation.z is less than 90 or bigger than 270.
    /// </summary>
    public static bool CheckFlip(this Transform t)
    {
        return (t.rotation.eulerAngles.z < 90 || t.rotation.eulerAngles.z > 270);
    }

    public static bool CheckDown(this Transform t)
    {
        return (t.rotation.eulerAngles.z < 360 && t.rotation.eulerAngles.z > 180);
    }
    public static bool CheckUp(this Transform t)
    {
        return (t.rotation.eulerAngles.z < 180 && t.rotation.eulerAngles.z > 0);
    }

    /// <summary>
    /// Return 1 if transform.rotation.z is less than 90 or bigger than 270, else is -1.
    /// </summary>
    public static int CheckFaceRight(this Transform t)
    {
        rot_z = t.rotation.eulerAngles.z;
        while (rot_z < 0)
        {
            rot_z += 360;
        }
        return (rot_z < 90 || rot_z > 270) ? 1 : -1;
    }

    public static void FlipToObj(this Transform t, float xObj)
    {
        /*if (xObj >= transform.position.x)
        {
            //Debug.Log(1);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (xObj < transform.position.x)
        {
            //Debug.Log(2);
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }*/
        if (xObj >= t.position.x)
        {
            t.Rot(0);
        }
        else if (xObj < t.position.x)
        {
            t.Rot(180);
        }
    }
    public static int CheckFaceUp(this Transform t)
    {
        rot_z = t.rotation.eulerAngles.z;
        while (rot_z < 0)
        {
            rot_z += 360;
        }
        return rot_z < 180 ? 1 : -1;
    }

    /// <summary>
    /// Using Euler angle(degree).
    /// </summary>
    public static void Rot(this Transform t, float angle)
    {
        while(angle < 0)
        {
            angle += 360;
        }
        angle = Mathf.Clamp(angle, 0, 360);
        t.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        t.FixLocalScale(angle);
    }
    public static void FollowEnemyRotZ(this Transform trans, Transform target)
    {
        diff = target.position - trans.position;
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        while (rot_z < 0)
        {
            rot_z += 360;
        }
        trans.rotation = Quaternion.Euler(0f, 0f, rot_z);
        trans.FixLocalScale(rot_z);
    }
    public static void FlipToEnemy(this Transform trans, Transform target)
    {
        diff = target.position - trans.position;
        if(diff.x >= 0)
        {
            trans.Rot(0);
        }
        else
        {
            trans.Rot(180);
        }
    }
    public static void FixLocalScale(this Transform trans, float rot)
    {
        if (rot < 90 || rot > 270)
        {
            trans.localScale = Vector2Extension.CreateVector2(trans.localScale, xToSet: Mathf.Abs(trans.localScale.x), yToSet: Mathf.Abs(trans.localScale.y) * (trans.parent != null ? (trans.parent.lossyScale.y > 0 ? 1 : -1) : 1));
        }
        else
        {
            trans.localScale = Vector2Extension.CreateVector2(trans.localScale, xToSet: Mathf.Abs(trans.localScale.x), yToSet: -Mathf.Abs(trans.localScale.y) * (trans.parent != null ? (trans.parent.lossyScale.y > 0 ? 1 : -1) : 1));
        }
        //Debug.Log(trans.name + " " + trans.localScale);
    }

    public static void FreeFixLocalScale(this Transform t)
    {
        rot_z = t.rotation.eulerAngles.z;
        while (rot_z < 0)
        {
            rot_z += 360;
        }

        if (rot_z < 90 || rot_z > 270)
        {
            t.localScale = Vector2Extension.CreateVector2(t.localScale, xToSet: Mathf.Abs(t.localScale.x), yToSet: Mathf.Abs(t.localScale.y));
        }
        else
        {
            t.localScale = Vector2Extension.CreateVector2(t.localScale, xToSet: Mathf.Abs(t.localScale.x), yToSet: -Mathf.Abs(t.localScale.y));
        }
    }

    public static bool CheckObjIsForward(this Transform trans, Vector2 obj)
    {
        return trans.CheckFaceRight() * (obj.x - trans.position.x) > 0;
    }
    public static float GetRotZFollowTargetByVector3(this Transform trans, Vector3 target, bool useCenter)
    {
        if (useCenter)
        {
            diff = target - (Vector3)trans.GetComponent<Creature>().GetCenterPos();
        }
        else
        {
            diff = target - trans.position;
        }
        if (useCenter)
        {
            diff = target - trans.position;
        }
        else
        {
            diff = target - trans.position;
        }
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        while (rot_z < 0)
        {
            rot_z += 360;
        }
        return rot_z;
    }
    public static float GetRotZFollowTargetByTransform(this Transform trans, Transform target, bool useCenter)
    {
        if (useCenter)
        {
            diff = target.position - (Vector3)trans.GetComponent<Creature>().GetCenterPos();
        }
        else
        {
            diff = target.position - trans.position;
        }
        diff.Normalize();

        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        while (rot_z < 0)
        {
            rot_z += 360;
        }
        return rot_z;
    }

    public static void FlipFollowXVel(this Transform transform, Rigidbody2D rb)
    {
        /*if(rb.velocity.x > .1)
        {
            transform.localScale = cbProps.CreateVector2(transform.localScale, xToSet:1);
        }
        else if (rb.velocity.x < -.1)
        {
            transform.localScale = cbProps.CreateVector2(transform.localScale, xToSet: -1);
        }*/
        if (rb.velocity.x > .1)
        {
            transform.Rot(0);
        }
        else if (rb.velocity.x < -.1)
        {
            transform.Rot(180);
        }
    }

    public static void FlipRight(this Transform transform, bool tf)
    {
        /*if(rb.velocity.x > .1)
        {
            transform.localScale = cbProps.CreateVector2(transform.localScale, xToSet:1);
        }
        else if (rb.velocity.x < -.1)
        {
            transform.localScale = cbProps.CreateVector2(transform.localScale, xToSet: -1);
        }*/
        if (tf)
        {
            transform.Rot(0);
        }
        else
        {
            transform.Rot(180);
        }
    }

    public static void ChangeLocalScale(this Transform t, bool absX, bool absY)
    {
        sampleV3 = t.localScale;

        if (absX)
        {
            sampleV3.x = Mathf.Abs(sampleV3.x);
        }
        else
        {
            sampleV3.x = -Mathf.Abs(sampleV3.x);
        }

        if (absY)
        {
            sampleV3.y = Mathf.Abs(sampleV3.y);
        }
        else
        {
            sampleV3.y = -Mathf.Abs(sampleV3.y);
        }

        t.localScale = sampleV3;
    }
    public static void ChangeLocalScaleOnlyX(this Transform t, bool absX)
    {
        Vector3 sampleV = t.localScale;

        if (absX)
        {
            sampleV.x = Mathf.Abs(sampleV.x);
        }
        else
        {
            sampleV.x = -Mathf.Abs(sampleV.x);
        }

        t.localScale = sampleV;
    }
}
