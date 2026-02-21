using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Cube Color Data")]
public class CubeColorData : ScriptableObject // Scriptabl Object для более удобного управления цветами куба
{
    public CubeColorEntry[] colors;

    private Dictionary<int, Color> colorMap;

    public void Init()
    {
        colorMap = new Dictionary<int, Color>();

        foreach (var entry in colors)
        {
            if (!colorMap.ContainsKey(entry.value))
                colorMap.Add(entry.value, entry.color);
        }
    }

    public Color GetColor(int value)
    {
        if (colorMap == null)
            Init();

        if (colorMap.TryGetValue(value, out Color color))
            return color;

        return Color.white;
    }
}

[System.Serializable]
public class CubeColorEntry
{
    public int value;
    public Color color;
}