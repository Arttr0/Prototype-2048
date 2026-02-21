using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    public int Value { get; private set; }

    [SerializeField] private float minImpulse = 0.5f; // минимальный импульс для merge
    [SerializeField] private TextMeshPro[] cubetext;     // текст на кубе
    [SerializeField] private Renderer cubeRenderer;    // для цвета
    [SerializeField] private CubeColorData colorData;
    private bool merged = false;

    // Создание куба
    public void Initialize(int value)
    {
        Value = value;
        merged = false;
        for (int i = 0; i < cubetext.Length; i++)
        {
            if (cubetext[i] != null) cubetext[i].text = Value.ToString();
            if (cubeRenderer != null) cubeRenderer.material.color = colorData.GetColor(Value); ;
        }
        //Debug.Log("Initialize Cube");
    }

   //Проверка кубов на соединени
    void OnCollisionEnter(Collision collision)
    {
        if (merged) return;

        if (!collision.gameObject.CompareTag("Cubelaunched")) return;

        Cube other = collision.gameObject.GetComponent<Cube>();
        if (other == null || other.merged) return;
        // Проверка на значение
        if (other.Value != Value) return;
        // Проверка силы столкновения (минимальный импульс)
        float impulse = collision.impulse.magnitude;
        if (impulse < minImpulse) return;

        // Отмечаем как слитые
        merged = true;
        other.merged = true;

        // Получаем позицию для нового куба
        Vector3 spawnPos = (transform.position + other.transform.position) / 2f;

        // Удаляем старые кубы
        Destroy(other.gameObject);
        Destroy(gameObject);
        //Обработчик соеденения куба
        GameController.Instance.MergeCubes(spawnPos, Value);
        //Debug.Log("Create: " + Value*2);
    }
}
