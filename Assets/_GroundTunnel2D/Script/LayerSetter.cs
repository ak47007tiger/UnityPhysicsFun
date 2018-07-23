using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LayerSetter : MonoBehaviour
{

    // Use this for initialization
    public string sorttingLayer;
    public int sorttingOrder;

    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (!string.IsNullOrEmpty(sorttingLayer))
        {
            meshRenderer.sortingLayerName = sorttingLayer;
        }
        meshRenderer.sortingOrder = sorttingOrder;
    }
}
