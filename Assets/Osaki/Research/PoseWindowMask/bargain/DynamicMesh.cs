using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMesh : MonoBehaviour
{
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] vertices = {
            new Vector3(-1f, -1f, 0),
            new Vector3(-1f,  1f, 0),
            new Vector3( 1f,  1f, 0),
            new Vector3( 1f, -1f, 0)
        };

        int[] triangles = { 0, 1, 2, 0, 2, 3 };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (!meshFilter) meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer) meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = mesh;
        meshRenderer.sharedMaterial = material;
    }
}
