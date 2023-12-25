using UnityEngine;

public static class Vector2Extension
{
    static Vector2 sampleV2;
    static Vector3 sampleV3;
    static public Vector2 centerPos = new Vector2(0, 1.5f);
    static float tempFloat;
    public static Vector2 CreateVector2(Vector2 vector2, float? xToSet = null, float? yToSet = null)
    {
        sampleV2 = vector2;

        if (xToSet != null)
        {
            sampleV2.x = (float)xToSet;
        }

        if (yToSet != null)
        {
            sampleV2.y = (float)yToSet;
        }

        return sampleV2;
    }

    public static Vector2 AroundPos(Vector2 vector2, float radius)
    {
        sampleV2 = vector2;

        tempFloat = Random.Range(0, radius) * ((Random.Range(0, 10) > 5) ? 1 : -1);

        sampleV2.x = vector2.x + tempFloat;
        sampleV2.y = vector2.y + Mathf.Sqrt(radius * radius - tempFloat * tempFloat) * ((Random.Range(0, 10) > 5) ? 1 : -1);

        return sampleV2;
    }
    public static Vector3 CreateVector3(Vector3 vector3, float? xToSet = null, float? yToSet = null)
    {
        sampleV3 = vector3;

        if (xToSet != null)
        {
            sampleV3.x = (float)xToSet;
        }
        else
        {
            sampleV3.x = vector3.x;
        }

        if (yToSet != null)
        {
            sampleV3.y = (float)yToSet;
        }
        else
        {
            sampleV3.y = vector3.y;
        }

        return sampleV3;
    }
}
