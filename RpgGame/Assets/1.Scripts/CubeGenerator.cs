using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [Header("References")]
    public Transform Parent;
    public GameObject Prefab;
    [HideInInspector] public string GeneratedObjectName;
    [HideInInspector] public string CubeName;
    [HideInInspector] public Vector3 Pivot;
    [HideInInspector] public Vector3Int Size;

    [HideInInspector] public GameObject GeneratedGameObject;
    [HideInInspector] public List<GameObject> GeneratedObjects;

    public void ResetButton()
    {
        if (GeneratedObjects != null && GeneratedObjects.Count != 0)
        {
            for (int i = GeneratedObjects.Count - 1; i >= 0; --i)
            {
                var obj = GeneratedObjects[i];
                GeneratedObjects.RemoveAt(i);
                DestroyImmediate(obj.gameObject);
            }

            GeneratedObjects.Clear();
            DestroyImmediate(GeneratedGameObject);
        }
    }

}
