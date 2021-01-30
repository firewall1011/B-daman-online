
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have void as argument
/// </summary>

[CreateAssetMenu(menuName = "Events/VoidEvent Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;
    
    public void RaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}
