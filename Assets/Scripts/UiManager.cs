using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI recordText;
    public GameObject gameOverPanel;
    [SerializeField] private ScoreManager scoreManager;
    public event System.Action OnRestartPressed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        scoreManager.OnScoreChanged += UpdateUI;
        UpdateUI(scoreManager.Score, scoreManager.Record);
    }
    private void OnDisable()
    {
        scoreManager.OnScoreChanged -= UpdateUI;
    }
    public void UpdateUI(int score, int record) {
        scoreText.text = "Score: " + score.ToString();
        recordText.text = "Record: " + record.ToString();
    }

    public void GameOverPanelShow() {
        gameOverPanel.SetActive(true);
    }
    public void OnPressedRestartButton()
    {
        OnRestartPressed?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
