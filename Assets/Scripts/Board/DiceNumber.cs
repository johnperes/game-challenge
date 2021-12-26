using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiceNumber : MonoBehaviour
{
    [SerializeField]
    TMP_Text labelValue;

    int value;

    public void SetValue(int param)
    {
        value = param;
        labelValue.text = param.ToString();
    }
    public int GetValue()
    {
        return value;
    }
}
