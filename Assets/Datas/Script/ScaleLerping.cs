﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLerping : MonoBehaviour
{
    public Vector2 minScale;
    public Vector2 maxScale;
    public bool repeatFlag;
    public float scalingSpeed;
    public float scalingDuration;

    private IEnumerator Start()
    {
        while (repeatFlag)
        {
            yield return RepeatLerping(minScale, maxScale, scalingDuration);
            yield return RepeatLerping(maxScale, minScale, scalingDuration);
        }
    }

    IEnumerator RepeatLerping(Vector2 startScale, Vector2 endScale, float time)
    {
        float t = 0.0f;
        float rate = (1f / time) * scalingSpeed;
        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            transform.localScale = Vector2.Lerp(startScale, endScale, t);
            yield return null;
        }
    }
}