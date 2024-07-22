using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBroadcaster : MonoBehaviour
{
    private static readonly HashSet<PauseBroadcaster> _instances = new HashSet<PauseBroadcaster>();

    private void Awake()
    {
        //Debug.Log("PB added");
        _instances.Add(this);
    }

    private void OnDestroy()
    {
        _instances.Remove(this);
    }

    public void BroadcastPause()
    {
        SendMessage("OnGamePaused", SendMessageOptions.DontRequireReceiver);
    }

    public void BroadcastResume()
    {
        SendMessage("OnGameResumed", SendMessageOptions.DontRequireReceiver);
    }

    public static HashSet<PauseBroadcaster> GetInstances()
    {
        return new HashSet<PauseBroadcaster>(_instances);
    }
}
