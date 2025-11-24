using UnityEngine;
using UnityEngine.UI;

public class UiHealthBar : MonoBehaviour
{
    private Image _healthFill;
    private int _maxValue;

    private void Awake()
    {
        _healthFill = GetComponent<Image>();
    }

    public void SetValue(int p_value)
    {
        _maxValue = p_value;
    }

    public void RefreshHealthBar(int p_curValue)
    {
        _healthFill.fillAmount = (float)p_curValue / (float)_maxValue;
    }
}
