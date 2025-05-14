using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreViewer : MonoBehaviour
{
    public TMP_Text scoreText; // 연결할 텍스트 오브젝트

    public void ShowScores()
    {
        List<int> scores = ScoreManager.LoadTopScores();
        scoreText.text = "\n<b>내 점수 랭킹</b>\n";
        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log($"[ScoreViewer] {i + 1}위 점수: {scores[i]}");
            scoreText.text += $"{i + 1}. {scores[i]}\n";
        }
    }
}
