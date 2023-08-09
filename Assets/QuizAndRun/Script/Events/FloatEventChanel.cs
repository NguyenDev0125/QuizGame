using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatEventChanel" , menuName = "Events/new FloatEventChanel")]
public class FloatEventChanel : ScriptableObject
{
    public Action<float> RaisedEvent;
    public void Raised(float value)
    {
        if(RaisedEvent == null)
        {
            Debug.Log($"No listen to this event {name}");
        }
        RaisedEvent?.Invoke(value);
    }
}
