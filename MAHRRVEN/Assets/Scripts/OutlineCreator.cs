using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Transform))]
public class OutlineCreator : MonoBehaviour
{
    [Tooltip("Material used for outline (unlit is best).")]
    public Material outlineMaterial;
    [Tooltip("Scale multiplier for the outline meshes (1.01 - 1.1).")]
    public float outlineScale = 1.03f;

    List<GameObject> outlineCopies = new List<GameObject>();

    void Awake()
    {
        CreateOutlineCopies();
        SetOutlineVisible(false);
    }

    void CreateOutlineCopies()
    {
        // Find all MeshRenderers and SkinnedMeshRenderers
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        var skinned = GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var mr in meshRenderers)
            CreateCopyForRenderer(mr);

        foreach (var smr in skinned)
            CreateCopyForSkinned(smr);
    }

    void CreateCopyForRenderer(MeshRenderer mr)
    {
        var filter = mr.GetComponent<MeshFilter>();
        if (filter == null) return;

        GameObject go = new GameObject(mr.gameObject.name + "_outline");
        go.transform.SetParent(mr.transform, false);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one * outlineScale;

        var copyFilter = go.AddComponent<MeshFilter>();
        copyFilter.sharedMesh = filter.sharedMesh;

        var copyRenderer = go.AddComponent<MeshRenderer>();
        copyRenderer.sharedMaterial = outlineMaterial;

        // Make sure it renders on top: set renderQueue if needed (e.g., 3000)
        copyRenderer.sharedMaterial.renderQueue = 3000;

        outlineCopies.Add(go);
    }

    void CreateCopyForSkinned(SkinnedMeshRenderer smr)
    {
        GameObject go = new GameObject(smr.gameObject.name + "_outline");
        go.transform.SetParent(smr.transform, false);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one * outlineScale;

        var copy = go.AddComponent<MeshFilter>();
        var copyRenderer = go.AddComponent<MeshRenderer>();
        // Bake mesh to apply skinning
        Mesh baked = new Mesh();
        smr.BakeMesh(baked);
        copy.sharedMesh = baked;
        copyRenderer.sharedMaterial = outlineMaterial;
        copyRenderer.sharedMaterial.renderQueue = 3000;

        outlineCopies.Add(go);
    }

    /// <summary>Toggle outline visibility.</summary>
    public void SetOutlineVisible(bool visible)
    {
        foreach (var go in outlineCopies)
            if (go) go.SetActive(visible);
    }
}
