using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiButtonMinerBuy : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI p_mineralCost;

    private void Start()
    {
        RefreshCostText();
    }
    public void BuyMiner()
    {
        GameManager.Instance.MineralManager.BuyMiner();
        RefreshCostText();
    }

    private void RefreshCostText()
    {
        p_mineralCost.text = GameManager.Instance.MineralManager.FinalMinerCost.ToString();
    }
}
