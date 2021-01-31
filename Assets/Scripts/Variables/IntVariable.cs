using UnityEngine;
[CreateAssetMenu(fileName = "Int", menuName = "Variables/Primitive/int")]
public class IntVariable : ScriptableObject
{
    public int Value = default;

    public static implicit operator int(IntVariable target) => target.Value;
}
