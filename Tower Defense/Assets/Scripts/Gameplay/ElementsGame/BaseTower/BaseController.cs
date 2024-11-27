using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private bool isEmpty;

    public bool CheckEmptySlot()
    {
        return isEmpty;
    }

    public void UseSlot()
    {
        isEmpty = false;
    }
}
