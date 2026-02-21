using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    //Тут можно было бы добавить Enum с состояниями игры и управлять отсюда, например: win, pause и тд.
    // Подписчики реагируют через события
    public bool IsGameOver { get; private set; }

    public event System.Action OnGameOver;

    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;
        OnGameOver?.Invoke();
    }
}
