using UnityEngine;
using UnityEngine.UIElements;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;
    [SerializeField] private Transform spawnPoint;

    public Cube Spawn(int value)
    {
        Cube cube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);
        cube.Initialize(value);
        cube.gameObject.tag = "Cube";
        return cube;
    }

    public Cube SpawnMerged(Vector3 pos, int value, float mergeLaunchForce, GameObject mergeParticlePrefab)
    {
        // Создание куба, в позиции pos, без поворота
        Cube cube = Instantiate(cubePrefab, pos, Quaternion.identity);

        cube.Initialize(value);
        cube.gameObject.tag = "Cubelaunched"; // Куб уже в игре
        // Чем больше куб тем сильнее тряска камеры
        float intensity = Mathf.Log(value, 2) * 0.02f;
        Camera.main.GetComponent<CameraShake>().Shake(0.1f, intensity);

        if (mergeParticlePrefab != null)
        {
            //Debug.Log("CreateParticle");
            Instantiate(mergeParticlePrefab, pos, Quaternion.identity);
        }

        // Небольшой подлёт
        cube.GetComponent<Rigidbody>()
            .AddForce(Vector3.up * mergeLaunchForce, ForceMode.Impulse);
        return cube;
    }
}