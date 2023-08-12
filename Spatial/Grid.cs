using System;
using System.Numerics;

namespace Axvemi.Commons.Spatial;

public class Grid<T>
{
	public int Width { get; set; }
	public int Height { get; set; }
	public T[,] GridObjectsDimensionalArray { get; set; }

	private readonly Vector2 _originPosition;

	public Grid(int width, int height, Vector2 originPosition, Func<Grid<T>, int, int, T> createGridObjectMethod)
	{
		Width = width;
		Height = height;
		_originPosition = originPosition;

		GridObjectsDimensionalArray = new T[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				GridObjectsDimensionalArray[x, y] = createGridObjectMethod(this, x, y);
			}
		}
	}

	/// <summary>
	/// </summary>
	/// <param name="x">X Coordinate</param>
	/// <param name="y">Y Coordinate</param>
	/// <returns>Object in that coordinates. Default if not found</returns>
	public T GetObjectAtCoordinates(int x, int y)
	{
		if (x >= 0 && y >= 0 && x < Width && y < Height)
		{
			return GridObjectsDimensionalArray[x, y];
		}

		return default;
	}

	/// <summary>
	/// </summary>
	/// <param name="x">X Coordinate</param>
	/// <param name="y">Y Coordinates</param>
	/// <returns>World position given those coordinates</returns>
	public Vector2 GetWorldPositionFromCoordinates(int x, int y)
	{
		return new Vector2(x, y) + _originPosition;
	}

	public override string ToString()
	{
		return $"Width: {Width}; Height: {Height}; Origin Position: {_originPosition}; GridObjectArrayDimensional Lenght: {GridObjectsDimensionalArray.Length}";
	}
}