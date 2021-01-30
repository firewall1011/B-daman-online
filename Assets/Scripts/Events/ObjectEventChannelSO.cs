
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have object as argument
/// </summary>

[CreateAssetMenu(menuName = "Events/ObjectEvent Channel")]
public class ObjectEventChannelSO : ScriptableObject
{
    public UnityAction<object> OnEventRaised;
    
    public void RaiseEvent(object value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
