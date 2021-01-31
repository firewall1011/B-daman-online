using UnityEngine;
[CreateAssetMenu(fileName = "Float", menuName = "Variables/Primitive/float")]
public class FloatVariable : ScriptableObject
{
    public float Value = default;

    public static implicit operator float(FloatVariable target) => target.Value;

}
