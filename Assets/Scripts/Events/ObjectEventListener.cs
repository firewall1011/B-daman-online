
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
[System.Serializable]
public class ObjectEvent : UnityEvent<object>{}

/// <summary>
/// A flexible handler for object events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
/// </summary>
public class ObjectEventListener : MonoBehaviour
{
	[SerializeField] private ObjectEventChannelSO _channel = default;

	public ObjectEvent OnEventRaised;

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

	private void Respond(object value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
