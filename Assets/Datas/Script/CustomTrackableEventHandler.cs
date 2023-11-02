using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour trackableBehaviour;
    public MonoBehaviour scriptToEnable; // 활성화할 스크립트

    void Start()
    {
        trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (trackableBehaviour)
        {
            trackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        if (scriptToEnable != null)
        {
            scriptToEnable.enabled = true; // 스크립트 활성화
        }
    }

    private void OnTrackingLost()
    {
        if (scriptToEnable != null)
        {
            scriptToEnable.enabled = false; // 스크립트 비활성화
        }
    }
}