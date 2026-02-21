using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos; // Старт позиция (после тряски сюда вернёмся)

    void Awake()
    {
        originalPos = transform.localPosition; // Сохраняем старт позицию
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Случайное смещение с учётом магнитуды
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null; // Ждём следующий кадр
        }
        // Возврат камеры
        transform.localPosition = originalPos;
    }
}
