using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMetr : MonoBehaviour
{
    private float minScale = 0;
    private float maxScale = 1f; //1.74f
    public RectTransform strip;

    public void changePointMetrScale(float point, float minPoint)
    {
        if (strip != null)
        {
            strip.localScale = new Vector3(1, Mathf.Lerp(minScale, maxScale, point / minPoint), 1);
            // print(strip.localScale);
            // strip.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minScale, maxScale, point / minPoint));
        }
    }

}
