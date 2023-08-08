using System;
using UnityEngine;
[CreateAssetMenu(fileName = "VoidEventChanel", menuName = "Events/new VoidEventChanel")]
public class VoidEventChanel : ScriptableObject
{
    public Action OnEventRaised;
    public void Raise()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke();
            return;
        }
        Debug.Log($"No listen to this event {name}");
    }
}
