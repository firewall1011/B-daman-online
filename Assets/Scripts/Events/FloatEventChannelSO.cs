
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have float as argument
/// </summary>

[CreateAssetMenu(menuName = "Events/FloatEvent Channel")]
public class FloatEventChannelSO : ScriptableObject
{
    public UnityAction<float> OnEventRaised;
    
    public void RaiseEvent(float value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
