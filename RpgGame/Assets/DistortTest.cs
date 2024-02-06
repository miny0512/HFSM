using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DistortTest : MonoBehaviour
{
    public Material mat;
    private Material[] origin;
    SkinnedMeshRenderer[] renderers;
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        origin = new Material[renderers.Length];
        for(int i = 0; i < renderers.Length; i++)
        {
            origin[i] = renderers[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            renderers.ToList().ForEach(r => { r.material = mat;});  
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            for(int i = 0; i < renderers.Length;i++)
            {
                renderers[i].material = origin[i];
            }
        }  
    }
}
