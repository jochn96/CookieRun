using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour

{
    public static void SaveScore(int score)
    {
        // 기존 저장된 점수 불러오기
        List<int> scores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetInt($"Score{i}", 0));
        }

        // 새 점수 추가 후 정렬
        scores.Add(score);
        scores = scores.OrderByDescending(s => s).Take(5).ToList();

        // 다시 저장
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt($"Score{i}", scores[i]);
        }

        PlayerPrefs.Save();
    }

    public static List<int> LoadTopScores()
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetInt($"Score{i}", 0));
        }
        return scores;
    }
}
