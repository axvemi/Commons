using System.Numerics;

namespace Axvemi.Commons.Meshes;

public static class VoxelData
{
	/*
	 *VERTEX NUMBER/NAMES                      
	 *                                                    
	 *                       7              6
	 *                       *--------------*
	 *                      /|             /|
	 *   y               3 / |           2/ |
	 *   |                *--------------*  |  
	 *   |                |  |           |  |
	 *   |_____ x         |  |           |  |    
	 *   /                |  |4          |  |
	 *  /                 |  *-----------|--*5
	 *   -z               | /            | /    
	 *                    |/             |/        
	 *                   0*--------------*1
	 *              
	 */

	public const float Width = 1;
	public const float Height = .5f;
	public const float Depth = 1;

	public const int FaceNegativeZ = 0;
	public const int FaceZ = 1;
	public const int FaceNegativeY = 2;
	public const int FaceY = 3;
	public const int FaceNegativeX = 4;
	public const int FaceX = 5;

	/// <summary>
	/// Positions of each vertice.
	/// </summary>
	public static readonly Vector3[] Verts =
	{
		new(0f, 0f, 0f), //0
		new(Width, 0f, 0f), //1
		new(Width, Height, 0f), //2
		new(0f, Height, 0f), //3
		new(0f, 0f, Depth), //4
		new(Width, 0f, Depth), //5
		new(Width, Height, Depth), //6
		new(0f, Height, Depth), //7
	};

	//Store which vertices belong to each face
	public static readonly int[,] FaceVertices =
	{
		{ 0, 1, 2, 3 }, //NZ Face
		{ 5, 4, 7, 6 }, //Z Face
		{ 1, 0, 4, 5 }, //NY Face
		{ 3, 2, 6, 7 }, //Y Face
		{ 4, 0, 3, 7 }, //NX Face
		{ 1, 5, 6, 2 }, //X Face
	};

	/// <summary>
	/// In which order the face vertices go (by position in the array). Each 3 it's a triangle.
	/// </summary>
	public static readonly int[] IndiceVertexOrderCc = { 0, 1, 2, 0, 2, 3 };

	/// <summary>
	/// Vertex normal direction. One entry per face
	/// </summary>
	public static readonly Vector3[] Normals =
	{
		new(0, 0, -1), //NZ Face
		new(0, 0, 1), //Z Face
		new(0, -1, 0), //NY Face
		new(0, 1, 0), //Y Face
		new(-1, 0, 0), //NX Face
		new(1, 0, 0), //X Face
	};

	//All faces are mapped the same way. Store the uv point for each vertex in the face
	public static readonly Vector2[] Uvs =
	{
		new(0, 0),
		new(1, 0),
		new(1, 1),
		new(0, 1),
	};

	public static readonly Vector2[] UvsVFlip =
	{
		new(0, 1),
		new(1, 1),
		new(1, 0),
		new(0, 0),
	};
}