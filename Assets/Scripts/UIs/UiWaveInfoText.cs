using TMPro;
using UnityEngine;

public class UiWaveInfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _allWavesText;
    [SerializeField] private TextMeshProUGUI _curWavesText;

    public void Init(int p_curWave, int p_allWaves)
    {
        _allWavesText.text = p_allWaves.ToString();
        RefreshWaveInfo(p_curWave);
    }

    public void RefreshWaveInfo(int p_curWave)
    {
        _curWavesText.text = (p_curWave + 1).ToString();
    }
}
