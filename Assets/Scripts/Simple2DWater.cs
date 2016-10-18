using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
public class Simple2DWater : MonoBehaviour {

	public float waterHeight = 5.0f;
	public float waterWeight = 3.0f;

	float[] xPositions;
	float[] yPositions;
	Mesh waterMesh;

	void Start ()
	{
		SpawnWaterMesh();
	}


	void SpawnWaterMesh()
	{
		int edgeCount = 5;
        int nodeCount = edgeCount + 1;

		xPositions = new float[nodeCount];
        yPositions = new float[nodeCount];

        for (int i = 0; i < nodeCount; i++)
        {
			yPositions[i] = waterHeight;
            xPositions[i] = -waterWeight/2.0f + waterWeight * i / edgeCount;
        }

		List<Vector3> verticesList = new List<Vector3>();
		List<Vector2> uvList = new List<Vector2>();
		List<int> indexList = new List<int>();

		//Setting the meshes now:
        for (int i = 0; i < edgeCount; i++)
        {
            //Create the corners of the mesh
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xPositions[i], yPositions[i], 0);
            Vertices[1] = new Vector3(xPositions[i+1], yPositions[i+1], 0);
            Vertices[2] = new Vector3(xPositions[i], 0, 0);
            Vertices[3] = new Vector3(xPositions[i+1], 0, 0);

			verticesList.Add(Vertices[0]);
			verticesList.Add(Vertices[1]);
			verticesList.Add(Vertices[2]);
			verticesList.Add(Vertices[3]);

			float UVStep = 1.0f/edgeCount;

            //Set the UVs of the texture
            Vector2[] UVs = new Vector2[4];
			UVs[0] = new Vector2(UVStep*i, 1);
			UVs[1] = new Vector2(UVStep*(i+1), 1);
			UVs[2] = new Vector2(UVStep*i, 0);
			UVs[3] = new Vector2(UVStep*(i+1), 0);

			uvList.Add(UVs[0]);
			uvList.Add(UVs[1]);
			uvList.Add(UVs[2]);
			uvList.Add(UVs[3]);

			int[] tris = new int[6] {0+4*i, 1+4*i, 3+4*i, 3+4*i, 2+4*i, 0+4*i};

			indexList.Add(tris[0]);
			indexList.Add(tris[1]);
			indexList.Add(tris[2]);
			indexList.Add(tris[3]);
			indexList.Add(tris[4]);
			indexList.Add(tris[5]);
        }

		waterMesh = new Mesh();

		waterMesh.vertices = verticesList.ToArray();
		waterMesh.uv = uvList.ToArray();
		waterMesh.triangles = indexList.ToArray();

		GetComponent<MeshFilter>().mesh = waterMesh;
	}
}
