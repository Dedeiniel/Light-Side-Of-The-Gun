using UnityEngine;
using TMPro;
using System;

public class CopyText : MonoBehaviour
{
    public TextMeshProUGUI ThisText;
    public TextMeshProUGUI TextToCopy;

    void Update()
    {
        ThisText.text = TextToCopy.text;
    }
}