
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have int as argument
/// </summary>

[CreateAssetMenu(menuName = "Events/IntEvent Channel")]
public class IntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;
    
    public void RaiseEvent(int value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
