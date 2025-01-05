using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class DelayFunction : MonoBehaviour
{
    [SerializeField] private UnityEvent delayFunction;

    public void StartDelay()
    {
        Delay();
    }

    private async Task Delay()
    {
        await Task.Delay(2000);
        delayFunction?.Invoke();
    }
}