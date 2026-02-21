using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }
    public int Record { get; private set; }
    public event System.Action<int, int> OnScoreChanged; // Событие для обновления UI <Счёт, Рекорд>
    private GameProgress progress;
    // Start is called before the first frame update
    void Start()
    {
        // Загружаем данные с json
        progress = SaveSystem.Load();
        if (progress != null )
        {
            Record = progress.record;
            //Debug.Log("Record " + Record); использовал для дебага в какой-то момент
        }
        OnScoreChanged?.Invoke(Score, Record);
    }

    public void AddScore(int value) {
        Score += value;
        if (Score > Record)
        {
            Record = Score;
            progress.record = Score;
            SaveSystem.Save(progress);
        }
        OnScoreChanged?.Invoke(Score, Record);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
