using System;
using System.Numerics;
using static Axvemi.Commons.Chunk;

namespace Axvemi.Commons;

public static class Chunk
{
	public const int ChunkSize = 5;
	public static readonly Vector3 ObjectsPerSide = new(ChunkSize, 36, ChunkSize); //How many object a chunk has in each side

	public const int AdjacentChunkNegativeX = 0; //-x
	public const int AdjacentChunkY = 1; //+y
	public const int AdjacentChunkX = 2; //+x
	public const int AdjacentChunkNegativeY = 3; //-y
}

public class Chunk<T>
{
	public Grid<Chunk<T>> Grid { get; set; }
	public int X { get; set; }
	public int Y { get; set; }
	public T[,,] ChunkObjectsDimensionalArray { get; set; }

	public Chunk(Grid<Chunk<T>> grid, int x, int y, Func<Chunk<T>, int, int, int, T> createChunkObjectMethod)
	{
		Grid = grid;
		X = x;
		Y = y;

		ChunkObjectsDimensionalArray = new T[(int)ObjectsPerSide.X, (int)ObjectsPerSide.Y, (int)ObjectsPerSide.Z];

		for (int i = 0; i < ObjectsPerSide.X; i++)
		{
			for (int j = 0; j < ObjectsPerSide.Y; j++)
			{
				for (int k = 0; k < ObjectsPerSide.Z; k++)
				{
					ChunkObjectsDimensionalArray[i, j, k] = createChunkObjectMethod(this, i, j, k);
				}
			}
		}
	}

	/// <summary>
	/// Get the 4 adjacent arrays. Use the Chunk.AdjacentChunk to access the one that you want
	/// </summary>
	/// <returns>Neighbour chunks. Default value if it doesn't exist</returns>
	public Chunk<T>[] GetAdjacentChunks()
	{
		Chunk<T>[] adjacentChunks = new Chunk<T>[4];
		adjacentChunks[AdjacentChunkNegativeX] = Grid.GetObjectAtCoordinates(X - 1, Y);
		adjacentChunks[AdjacentChunkY] = Grid.GetObjectAtCoordinates(X, Y + 1);
		adjacentChunks[AdjacentChunkX] = Grid.GetObjectAtCoordinates(X + 1, Y);
		adjacentChunks[AdjacentChunkNegativeY] = Grid.GetObjectAtCoordinates(X, Y - 1);

		return adjacentChunks;
	}

	/// <summary>
	/// Get the object given some coordinates
	/// Default if not found
	/// </summary>
	/// <param name="x">X Coordinate</param>
	/// <param name="y">Y Coordinate</param>
	/// <param name="z">Z Coordinate</param>
	/// <returns></returns>
	public T GetObjectAtLocalCoordinates(int x, int y, int z)
	{
		if (x >= 0 && y >= 0 && z >= 0 && x < ObjectsPerSide.X && y < ObjectsPerSide.Y && z < ObjectsPerSide.Z)
		{
			return ChunkObjectsDimensionalArray[x, y, z];
		}

		return default;
	}
}