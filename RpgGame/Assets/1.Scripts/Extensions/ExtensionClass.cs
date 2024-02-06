using UnityEngine;

public static class ExtensionClass
{
    public static bool IsSameLayer(this GameObject go, LayerMask compareLayer)
    {
        int a = 1 << go.layer;
        if ((a & compareLayer) != 0) return true;
        return false;
    }

    public static bool IsSameLayer(this GameObject go, int compareLayerIndex)
    {
        int a = 1 << go.layer;
        int b = 1 << compareLayerIndex;
        if ((a & b) != 0) return true;
        return false;
    }

    public static bool IsSameLayer(this LayerMask lm, GameObject go)
    {
        return go.IsSameLayer(lm);
    }
}
