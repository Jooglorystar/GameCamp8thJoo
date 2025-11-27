using TMPro;
using UnityEngine;

public class UiMinerCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _curMinerCountText;
    [SerializeField] private TextMeshProUGUI _maxMinerCountText;

    public void InitInfo(int p_count, int p_maxCount)
    {
        _maxMinerCountText.text = p_maxCount.ToString();
        RefreshCount(p_count);
    }

    public void RefreshCount(int p_count)
    {
        _curMinerCountText.text = p_count.ToString();
    }
}
