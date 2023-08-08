using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BoolEventChanel" , menuName = "Events/new BoolEventChanel")]
public class BoolEventChanel : ScriptableObject
{
    public Action<bool> OnEventRaised;
    public void Raise(bool _bool)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(_bool);
            return;
        }
        Debug.Log($"No listen to this event {name}");
    }
}
