using Mirror;

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have NetworkConnection as argument
/// </summary>

[CreateAssetMenu(menuName = "Events/NetworkConnectionEvent Channel")]
public class NetworkConnectionEventChannelSO : ScriptableObject
{
    public UnityAction<NetworkConnection> OnEventRaised;
    
    public void RaiseEvent(NetworkConnection value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
