using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TransformList", menuName = "Variables/Lists/Transform")]
public class TransformListSO : ScriptableObject
{
    [NaughtyAttributes.ReorderableList]
    public List<Transform> List = new List<Transform>();

    public static TransformListSO operator+(TransformListSO target, Transform item)
    {
        target.List.Add(item);
        return target;
    }

    public static TransformListSO operator -(TransformListSO target, Transform item)
    {
        target.List.Remove(item);
        return target;
    }

    public void Add(Transform item)
    {
        List.Add(item);
    }

    public void Remove(Transform item)
    {
        List.Remove(item);
    }
}
