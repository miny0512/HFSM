
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(CubeGenerator))]
public class CubeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Separator();

        CubeGenerator generator = target as CubeGenerator;
        generator.GeneratedObjectName = EditorGUILayout.TextField("GeneratedObject Name", generator.GeneratedObjectName);
        generator.CubeName = EditorGUILayout.TextField("Cube Name", generator.CubeName);
        generator.Pivot = EditorGUILayout.Vector3Field("Generate Pivot", generator.Pivot);
        var size = EditorGUILayout.Vector3Field("Cube Size", generator.Size);
        size.x  = EditorGUILayout.Slider("X",size.x, 1, 100);
        size.y  = EditorGUILayout.Slider("Y",size.y, 1, 100);
        size.z  = EditorGUILayout.Slider("Z",size.z, 1, 100);
        
        generator.Size = new Vector3Int((int)size.x, (int)size.y, (int)size.z);

        EditorGUILayout.Separator();
        if (GUILayout.Button("Generate"))
        {
            generator.ResetButton();
            var origin = generator.Prefab;
            var parent = generator.Parent;
            var gameObject = new GameObject();
            var cubeList = new List<GameObject>();
            int count = generator.Size.x * generator.Size.y * generator.Size.z;
            
            // 생성
            gameObject.name = generator.GeneratedObjectName;

            for(int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int z = 0; z < size.z; ++z)
                    {
                        Vector3 pos = new Vector3(x, y, z);
                        var inst = Instantiate(origin, gameObject.transform);
                        inst.transform.localPosition = pos;
                        inst.gameObject.name = $"{generator.CubeName}_{x+y+z}";
                        cubeList.Add(inst);
                    }
                }
            }
            gameObject.transform.position = generator.Pivot;
            generator.GeneratedGameObject = gameObject;
            generator.GeneratedObjects = cubeList;
            // 정렬   
            gameObject.transform.SetParent(parent);
        }

        if(GUILayout.Button("Reset"))
        {
            generator.ResetButton();
        }
    }

    
}
