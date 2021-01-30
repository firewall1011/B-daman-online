using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Event Generator")]
public class GenerateEventSO : ScriptableObject
{
    public string type = "";
    public string path = "";

    public void GenerateEventCode()
    {
        string _type = type.Trim();
        string _upperType = _type.ToUpper()[0] + type.Substring(1);

        string result = @"
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have " + _type + @" as argument
/// </summary>

[CreateAssetMenu(menuName = ""Events/" + _upperType+ @"Event Channel"")]
public class " + _upperType + @"EventChannelSO : ScriptableObject
{
    public UnityAction<" + _type + @"> OnEventRaised;
    
    public void RaiseEvent(" + _type + @" value)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(value);
    }
}
";
        string _path = Path.Combine(path, _upperType + "EventChannelSO.cs");
        File.WriteAllText(_path, result);
    }

    public void GenerateListenerCode()
    {
        string _type = type.Trim();
        string _upperType = _type.ToUpper()[0] + type.Substring(1);
        
        string result = @"
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// To use a generic UnityEvent type you must override the generic type.
/// </summary>
[System.Serializable]
public class " + _upperType + @"Event : UnityEvent<" + _type + @">{}

/// <summary>
/// A flexible handler for " + _type + @" events in the form of a MonoBehaviour. Responses can be connected directly from the Unity Inspector.
/// </summary>
public class " + _upperType + @"EventListener : MonoBehaviour
{
	[SerializeField] private " + _upperType + @"EventChannelSO _channel = default;

	public " + _upperType + @"Event OnEventRaised;

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

	private void Respond(" + _type + @" value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
";

        string _path = Path.Combine(path, _upperType + "EventListener.cs");
        File.WriteAllText(_path, result);
    }

#if UNITY_EDITOR
    private List<string> systemTypes = new List<string>(){ "int", "float", "double", "string", "bool", "object", "void"};
    
    [NaughtyAttributes.Button("Generate Event Code")]
    public void GenerateAllCode()
    {
        var typeExists = Type.GetType(type);
        if(typeExists == null && !systemTypes.Contains(type))
        {
            Debug.LogError($"Type {type} does not exist");
            return;
        }

        Task eventCodeTask = Task.Run(GenerateEventCode);
        Task listenerCodeTask = Task.Run(GenerateListenerCode);
        Debug.Log("Writing event code at " + path);
        Debug.Log("Writing listener code at " + path);

        Task.WaitAll(new Task[2] { eventCodeTask, listenerCodeTask });
        Debug.Log($"Code written");

        AssetDatabase.Refresh();
    }
#endif
}
