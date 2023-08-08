using System;
using UnityEngine;

[CreateAssetMenu(fileName = "VectorEventChanel", menuName = "Events/new VectorEventChanel")]
public class VectorEventChanel : ScriptableObject
{
    public Action<Vector3> OnEventRaised;
    public void Raise(Vector3 _vector3)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(_vector3);
            return;
        }
        Debug.Log($"No listen to this event {name}");
    }
}
