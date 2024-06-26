using System;
using System.Numerics;
using static Axvemi.Commons.Tile;

namespace Axvemi.Commons;

public static class Tile
{
	public const float Width = 1;
	public const float Height = .5f;
	public const float Depth = 1;

	public const int AdjacentTileNegativeX = 0; //-x
	public const int AdjacentTileX = 1; //+x
	public const int AdjacentTileY = 2; //+y
	public const int AdjacentTileNegativeY = 3; //-y
	public const int AdjacentTileZ = 4; //+z
	public const int AdjacentTileNegativeZ = 5; //-z
}

/// <summary>
/// Tile in the chunk.
/// </summary>
public class Tile<T>
{
	public Chunk<Tile<T>> TileChunk { get; set; }

	public T TileObject { get; set; }

	//Local Positions
	public int X { get; set; }
	public int Y { get; set; }
	public int Z { get; set; }

	public Tile(Chunk<Tile<T>> chunk, int x, int y, int z, Func<Tile<T>, T> createTileObjectMethod)
	{
		TileChunk = chunk;
		X = x;
		Y = y;
		Z = z;

		TileObject = createTileObjectMethod(this);
	}

	/// <summary>
	/// Get the 6 adjacent tiles. Use the Tile.AdjacentTile to access the one that you want
	/// </summary>
	/// <returns>Neighbour tiles. Null entry if it doesn't exist</returns>
	public Tile<T>[] GetAdjacentTiles()
	{
		Tile<T>[] adjacentTiles = new Tile<T>[6];
		//Left
		if (X == 0)
		{
			Chunk<Tile<T>> leftChunk = TileChunk.GetAdjacentChunks()[Chunk.AdjacentChunkNegativeX];
			if (leftChunk != null)
			{
				adjacentTiles[AdjacentTileNegativeX] = leftChunk.GetObjectAtLocalCoordinates((int)(Chunk.ObjectsPerSide.X - 1), Y, Z);
			}
		}
		else
		{
			adjacentTiles[AdjacentTileNegativeX] = TileChunk.GetObjectAtLocalCoordinates(X - 1, Y, Z);
		}

		//Right
		if (X == (int)Chunk.ObjectsPerSide.X - 1)
		{
			Chunk<Tile<T>> rightChunk = TileChunk.GetAdjacentChunks()[Chunk.AdjacentChunkX];
			if (rightChunk != null)
			{
				adjacentTiles[AdjacentTileX] = rightChunk.GetObjectAtLocalCoordinates(0, Y, Z);
			}
		}
		else
		{
			adjacentTiles[AdjacentTileX] = TileChunk.GetObjectAtLocalCoordinates(X + 1, Y, Z);
		}

		adjacentTiles[AdjacentTileY] = TileChunk.GetObjectAtLocalCoordinates(X, Y + 1, Z);
		adjacentTiles[AdjacentTileNegativeY] = TileChunk.GetObjectAtLocalCoordinates(X, Y - 1, Z);

		//"Up from top"
		if (Z == (int)Chunk.ObjectsPerSide.Z - 1)
		{
			Chunk<Tile<T>> upChunk = TileChunk.GetAdjacentChunks()[Chunk.AdjacentChunkY];
			if (upChunk != null)
			{
				adjacentTiles[AdjacentTileZ] = upChunk.GetObjectAtLocalCoordinates(X, Y, 0);
			}
		}
		else
		{
			adjacentTiles[AdjacentTileZ] = TileChunk.GetObjectAtLocalCoordinates(X, Y, Z + 1);
		}

		//"Bottom from top"
		if (Z == 0)
		{
			Chunk<Tile<T>> bottomChunk = TileChunk.GetAdjacentChunks()[Chunk.AdjacentChunkNegativeY];
			if (bottomChunk != null)
			{
				adjacentTiles[AdjacentTileNegativeZ] = bottomChunk.GetObjectAtLocalCoordinates(X, Y, (int)Chunk.ObjectsPerSide.Z - 1);
			}
		}
		else
		{
			adjacentTiles[AdjacentTileNegativeZ] = TileChunk.GetObjectAtLocalCoordinates(X, Y, Z - 1);
		}

		return adjacentTiles;
	}

	/// <summary>
	/// Get the tile world position
	/// </summary>
	/// <returns></returns>
	public Vector3 GetWorldPosition()
	{
		Vector2 chunkPosition = TileChunk.Grid.GetWorldPositionFromCoordinates(TileChunk.X * Chunk.ChunkSize, TileChunk.Y * Chunk.ChunkSize);
		return new Vector3(chunkPosition.X, 0, chunkPosition.Y) + new Vector3(X * Width, Y * Height, Z * Depth);
	}

	public override string ToString()
	{
		return "X: " + X + ", Y:" + Y + ", Z:" + Z;
	}
}