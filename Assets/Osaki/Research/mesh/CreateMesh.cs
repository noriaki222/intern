using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMesh : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh myMesh;
    private Vector3[] myVertices = new Vector3[4];
    private int[] myTriangles = new int[6];
    private float width = 2;
    private float hight = 2;

    void Awake()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        myMesh = new Mesh();

        myVertices[0] = new Vector3(0, 0, 0);
        myVertices[1] = new Vector3(width, 0, 0);
        myVertices[2] = new Vector3(0, hight, 0);
        myVertices[3] = new Vector3(width, hight, 0);

        myMesh.SetVertices(myVertices);

        myTriangles[0] = 0;
        myTriangles[1] = 2;
        myTriangles[2] = 1;
        myTriangles[3] = 2;
        myTriangles[4] = 3;
        myTriangles[5] = 1;

        myMesh.SetTriangles(myTriangles, 0);

        //MeshFilter‚Ö‚ÌŠ„‚è“–‚Ä
        meshFilter.mesh = myMesh;
    }
}
