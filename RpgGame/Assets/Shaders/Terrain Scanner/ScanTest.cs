using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScanTest : MonoBehaviour
{
    private void Awake()
    {
        var p = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;
        //p.GetRenderer(0).    
    }
}
