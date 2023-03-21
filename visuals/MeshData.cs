using System.Collections.Generic;
using System.Numerics;

namespace Axvemi.Commons.Meshes;

public class MeshData
{
	public string Id { get; }
	public int CurrentIndex { get; set; }
	public List<Vector3> Vertices { get; } = new();
	public List<int> Indices { get; } = new(); //Triangles
	public List<Vector2> Uvs { get; } = new();
	public List<Vector3> Normals { get; } = new();

	public MeshData(){ }
	public MeshData(string id)
	{
		Id = id;
	}
}