using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreViewer : MonoBehaviour
{
    public TMP_Text scoreText; // ������ �ؽ�Ʈ ������Ʈ

    public void ShowScores()
    {
        List<int> scores = ScoreManager.LoadTopScores();
        scoreText.text = "\n<b>�� ���� ��ŷ</b>\n";
        for (int i = 0; i < scores.Count; i++)
        {
            Debug.Log($"[ScoreViewer] {i + 1}�� ����: {scores[i]}");
            scoreText.text += $"{i + 1}. {scores[i]}\n";
        }
    }
}
