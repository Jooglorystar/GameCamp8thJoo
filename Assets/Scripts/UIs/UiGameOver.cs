using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;

    private void SetLabel(string p_text)
    {
        _label.text = p_text;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        GameManager.Instance.StartGame();
    }

    public void GameOver()
    {
        gameObject.SetActive(true);
        SetLabel("Game Over");
    }

    public void GameClear()
    {
        gameObject.SetActive(true);
        SetLabel("Game Clear");
    }
}
