using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiTextGold : MonoBehaviour
{
    private TextMeshProUGUI _goldText;

    private void Awake()
    {
        _goldText = GetComponent<TextMeshProUGUI>();
    }
    public void RefreshText(int p_value)
    {
        _goldText.text = p_value.ToString();
    }
}
