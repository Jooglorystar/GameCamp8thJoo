using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiWaveTimer : MonoBehaviour
{
    [SerializeField] private Image _waveTimerImage;
    [SerializeField] private TextMeshProUGUI _waveTimerText;

    private void Awake()
    {
        _waveTimerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateTimerUI(float p_fillAmount, float p_time)
    {
        _waveTimerImage.fillAmount = p_fillAmount;
        _waveTimerText.text = Mathf.CeilToInt(p_time).ToString();
    }
}
