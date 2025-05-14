using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePanelToggle : MonoBehaviour
{
    public GameObject scorePanel;

    private bool isVisible = false;

    public void ToggleScorePanel()
    {
        isVisible = !isVisible;
        scorePanel.SetActive(isVisible);
    }
    public void HideScorePanel() //Ã¢ ´Ý±â.
    {
        isVisible = false;
        if (scorePanel != null)
            scorePanel.SetActive(false);
    }
}
