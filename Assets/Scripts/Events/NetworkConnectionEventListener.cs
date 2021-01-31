using Mirror;

using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
[System.Serializable]
public class NetworkConnectionEvent : UnityEvent<NetworkConnection>{}

/// <summary>
/// A flexible handler for NetworkConnection events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
/// </summary>
public class NetworkConnectionEventListener : MonoBehaviour
{
	[SerializeField] private NetworkConnectionEventChannelSO _channel = default;

	public NetworkConnectionEvent OnEventRaised;

	private void OnEnable()
	{
		if (_channel != null)
			_channel.OnEventRaised += Respond;
	}

	private void OnDisable()
	{
		if (_channel != null)
			_channel.OnEventRaised -= Respond;
	}

	private void Respond(NetworkConnection value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
