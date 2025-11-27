using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTextMineral : MonoBehaviour
{
    private TextMeshProUGUI _mineralText;

    private void Awake()
    {
        _mineralText = GetComponent<TextMeshProUGUI>();
    }
    public void RefreshText(int p_value)
    {
        _mineralText.text = p_value.ToString();
    }
}
