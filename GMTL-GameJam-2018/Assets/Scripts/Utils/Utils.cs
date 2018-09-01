using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils  {




    public static float Duration(this AnimationCurve curve)
    {
        return curve.keys[curve.length - 1].time;
    }
}
