using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event System.Action OnRelease;
    public event System.Action<Vector2> OnHold;

    void Update()
    {
        Vector2 inputPos = Input.mousePosition; // Позиция мыши
        // Для сенсоров
        if (Input.touchCount > 0)
            inputPos = Input.GetTouch(0).position;

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
            OnHold?.Invoke(inputPos);

        if (Input.GetMouseButtonUp(0))
            OnRelease?.Invoke();
    }
}