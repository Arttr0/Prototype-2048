using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.PlayerLoop.EarlyUpdate;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("References")]
    public Cube cubePrefab;
    public GameObject mergeParticlePrefab;

    [Header("Settings")]
    public float moveSpeed = 0.02f;
    public float launchForce = 10f;
    public float mergeLaunchForce = 3f; // как куб подлетает при merge
    [SerializeField] private float shootCooldown = 0.3f;

    private Cube currentCube;
    [Header("Managers")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private InputHandler input;
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private GameStateManager stateManager;

    private bool canShoot = true;

    
    // Подписки/отписки на события
    private void OnEnable()
    {
        input.OnHold += MoveCube;
        input.OnRelease += Shoot;
        stateManager.OnGameOver += GameOver;
        uiManager.OnRestartPressed += RestartGame;
    }
    private void OnDisable()
    {
        input.OnHold -= MoveCube;
        input.OnRelease -= Shoot;
        stateManager.OnGameOver -= GameOver;
        uiManager.OnRestartPressed -= RestartGame;
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        MusicManager.Instance.UnPauseMusic();
        Time.timeScale = 1f;
        SpawnCube();
    }

    public void ScoreUpdate(int value)
    {
        scoreManager.AddScore(value);
    }
    //Да, можно было бы сделать через события, но я решил показать и такую реализацию
    public void GameOver()
    {
        AudioManager.Instance.PlayWin();
        MusicManager.Instance.PauseMusic();
        uiManager.GameOverPanelShow();
        Time.timeScale = 0f;
    }
    //Запускаем куротину через события
    void Shoot()
    {
        if (currentCube == null || !canShoot) return;
        StartCoroutine(ShootRoutine());
    }
    //Двигаем куб с помощью райкаста
    void MoveCube(Vector2 screenPos)
    {
        // Нет куба или ещё нельзя кидать кубик выходим     
        if (currentCube == null || !canShoot) return;

        Camera cam = Camera.main;// Основная камера
        if (cam == null) return;
        
        // Преобразуем координаты экрана как бы в лазер
        Ray ray = cam.ScreenPointToRay(screenPos);
        // Создание плоскости (куда падает лазер)
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        // Проверяем пересечение лазера и плоскости 
        if (plane.Raycast(ray, out float distance)) 
        {
            Vector3 worldPos = ray.GetPoint(distance); // Точка пересичения
            Vector3 pos = currentCube.transform.position; // Текущая позиция куба

            pos.x = Mathf.Clamp(worldPos.x, -1.5f, 1.5f); // Ограничиваем по Х
            currentCube.transform.position = pos; // Двигаем куб
        }
    }
    //Куротина кидает кубик
    IEnumerator ShootRoutine()
    {
        canShoot = false; // Не кидаем кубик, пока выполняется куротина

        currentCube.GetComponent<Rigidbody>()
            .AddForce(Vector3.forward * launchForce, ForceMode.Impulse); // Применяем импульс чтоб толкнуть кубик

        AudioManager.Instance.PlayLaunch(); // Звук запуска
 
        yield return new WaitForSeconds(shootCooldown);

        // Меняем тег что-бы куб считался запущенным (проверяем на столкновения только запущенные кубов)
        if (currentCube != null)
            currentCube.gameObject.tag = "Cubelaunched"; 

        currentCube = null;

        SpawnCube(); // Спавним новый куб
        canShoot = true;
    }

    void SpawnCube()
    {
        int value = Random.value <= 0.75f ? 2 : 4;
        currentCube = cubeSpawner.Spawn(value);
    }
    public void MergeCubes(Vector3 position, int value)
    {
        ScoreUpdate(value / 2);
        AudioManager.Instance.PlayMerge();
        SpawnMergedCube(position, value * 2);
    }
    // Спавн нового куба после merge
    public void SpawnMergedCube(Vector3 position, int value)
    {
        cubeSpawner.SpawnMerged(position, value, mergeLaunchForce, mergeParticlePrefab);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}