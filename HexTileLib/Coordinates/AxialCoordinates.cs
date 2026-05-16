namespace HexTileLib.Coordinates;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DoubledCoordinates;
using Silk.NET.Maths;

public readonly struct AxialCoordinates<T>(T q, T r) : IEquatable<AxialCoordinates<T>>, ICoordinates<T>
    where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
    public T Q
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Coords.X;
        }
    }

    public T R
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Coords.Y;
        }
    }
    
    public T S
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return Scalar.Subtract(Scalar.Negate(Coords.X), Coords.Y);
        }
    }

    public readonly Vector2D<T> Coords = new Vector2D<T>(q, r);

    public static explicit operator Vector2D<T>(AxialCoordinates<T> left)
    {
        return left.Coords;
    }
    public override string ToString()
    {
        return Coords.ToString();
    }
    public bool Equals(AxialCoordinates<T> other)
    {
        return Coords == other.Coords;
    }

    public AxialCoordinates<T> ToAxialCoordinates()
    {
        return this;
    }
    public DoubleHeightCoordinates<T> ToDoubleCoordinates()
    {
        var col = Q;
        var row = Scalar.Add(Scalar.Multiply(Scalar<T>.Two, R), Q);
        return new DoubleHeightCoordinates<T>(row, col);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<AxialCoordinates<T>> RingsCalculation(T range, AxialCoordinates<T> center)
    {
        var point = center.Neighbor((CoordinateDirectionFlat)4);
        for (var i = Scalar<T>.One; Scalar.LessThan(i, range); i = Scalar.Add(i, Scalar<T>.One))
        {
            point = point.Neighbor((CoordinateDirectionFlat)4);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            yield return point;
            point = point.Neighbor((CoordinateDirectionFlat)0);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            yield return point;
            point = point.Neighbor((CoordinateDirectionFlat)1);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            yield return point;
            point = point.Neighbor((CoordinateDirectionFlat)2);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            yield return point;
            point = point.Neighbor((CoordinateDirectionFlat)3);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            yield return point;
            point = point.Neighbor((CoordinateDirectionFlat)4);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            yield return point;
            point = point.Neighbor((CoordinateDirectionFlat)5);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Ring1(Span<AxialCoordinates<T>> output)
    {
        output[0] = Neighbor(CoordinateDirectionFlat.Up);
        output[1] = Neighbor(CoordinateDirectionFlat.RightUp);
        output[2] = Neighbor(CoordinateDirectionFlat.RightDown);
        output[3] = Neighbor(CoordinateDirectionFlat.Down);
        output[4] = Neighbor(CoordinateDirectionFlat.LeftDown);
        output[5] = Neighbor(CoordinateDirectionFlat.LeftUp);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RingsCalculation(T range, AxialCoordinates<T> center, Queue<AxialCoordinates<T>> output)
    {
        var point = center.Neighbor((CoordinateDirectionFlat)4);
        for (var i = Scalar<T>.One; Scalar.LessThan(i, range); i = Scalar.Add(i, Scalar<T>.One))
        {
            point = point.Neighbor((CoordinateDirectionFlat)4);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            output.Enqueue(point);
            point = point.Neighbor((CoordinateDirectionFlat)0);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            output.Enqueue(point);
            point = point.Neighbor((CoordinateDirectionFlat)1);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            output.Enqueue(point);
            point = point.Neighbor((CoordinateDirectionFlat)2);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            output.Enqueue(point);
            point = point.Neighbor((CoordinateDirectionFlat)3);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            output.Enqueue(point);
            point = point.Neighbor((CoordinateDirectionFlat)4);
        }
        for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
        {
            output.Enqueue(point);
            point = point.Neighbor((CoordinateDirectionFlat)5);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<AxialCoordinates<T>> PlanesCalculation(T range)
    {
        return PlanesCalculation(range, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<AxialCoordinates<T>> PlanesCalculation(T range, AxialCoordinates<T> center)
    {
        yield return center;
        for (var k = Scalar<T>.One; Scalar.LessThan(k, range); k = Scalar.Add(k, Scalar<T>.One))
        {
            var point = NeighborCalculation(center, CoordinateDirectionFlat.Up, k);
            for (var j = Scalar<T>.Zero; Scalar.LessThan(j, k); j = Scalar.Add(j, Scalar<T>.One))
            {
                yield return point;
                point = point.Neighbor((CoordinateDirectionFlat)0);
            }
            for (var j = Scalar<T>.Zero; Scalar.LessThan(j, k); j = Scalar.Add(j, Scalar<T>.One))
            {
                yield return point;
                point = point.Neighbor((CoordinateDirectionFlat)1);
            }
            for (var j = Scalar<T>.Zero; Scalar.LessThan(j, k); j = Scalar.Add(j, Scalar<T>.One))
            {
                yield return point;
                point = point.Neighbor((CoordinateDirectionFlat)2);
            }
            for (var j = Scalar<T>.Zero; Scalar.LessThan(j, k); j = Scalar.Add(j, Scalar<T>.One))
            {
                yield return point;
                point = point.Neighbor((CoordinateDirectionFlat)3);
            }
            for (var j = Scalar<T>.Zero; Scalar.LessThan(j, k); j = Scalar.Add(j, Scalar<T>.One))
            {
                yield return point;
                point = point.Neighbor((CoordinateDirectionFlat)4);
            }
            for (var j = Scalar<T>.Zero; Scalar.LessThan(j, k); j = Scalar.Add(j, Scalar<T>.One))
            {
                yield return point;
                point = point.Neighbor((CoordinateDirectionFlat)5);
            }
        }
    }
    
    public static readonly AxialCoordinates<T>[] Directions =
    [
        new AxialCoordinates<T>(Scalar<T>.One, Scalar<T>.Zero),  // RightDown,
        new AxialCoordinates<T>(Scalar<T>.Zero, Scalar<T>.One),  // Down,
        new AxialCoordinates<T>(Scalar<T>.MinusOne, Scalar<T>.One), // LeftDown,
        new AxialCoordinates<T>(Scalar<T>.MinusOne, Scalar<T>.Zero),  // LeftUp,
        new AxialCoordinates<T>(Scalar<T>.Zero, Scalar<T>.MinusOne),     // Up,
        new AxialCoordinates<T>(Scalar<T>.One, Scalar<T>.MinusOne)       // RightUp
    ];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AxialCoordinates<T> Direction(int direction)
    {
        return Directions[direction];
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AxialCoordinates<T> NeighborCalculation(AxialCoordinates<T> coordinates, int direction)
    {
        var vec = coordinates.Coords + Direction(direction).Coords;
        return new AxialCoordinates<T>(vec.X, vec.Y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static AxialCoordinates<T> NeighborCalculation(AxialCoordinates<T> coordinates, CoordinateDirectionFlat direction, T length)
    {
        var vec = coordinates.Coords + (Direction((int)direction).Coords * length);
        return new AxialCoordinates<T>(vec.X, vec.Y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public AxialCoordinates<T> Neighbor(CoordinateDirectionFlat coordinateDirectionFlat)
    {
        return NeighborCalculation(this, (int)coordinateDirectionFlat);
    }
    
    public override bool Equals(object obj)
    {
        return obj is AxialCoordinates<T> other && Equals(other);
    }
#if NETCOREAPP || NETSTANDARD2_1
    public override int GetHashCode()
    {
        return HashCode.Combine(Q, R);
    }
#elif NETSTANDARD2_0
    public override int GetHashCode()
    {
        return 17 + Q.GetHashCode() * 23 + R.GetHashCode() * 31;
    }
#endif
    public static bool operator ==(AxialCoordinates<T> left, AxialCoordinates<T> right)
    {
        return left.Equals(right);
    }
    
    public static AxialCoordinates<T> operator -(AxialCoordinates<T> left, AxialCoordinates<T> right)
    {
        var coords = left.Coords - right.Coords;
        return new AxialCoordinates<T>(coords.X, coords.Y);
    }

    public static bool operator !=(AxialCoordinates<T> left, AxialCoordinates<T> right)
    {
        return !left.Equals(right);
    }

    private static T PositiveMod(T a, T b)
    {
        return Scalar.As<double, T>(Scalar.As<T, double>(a) - Scalar.As<T, double>(b) * Math.Floor(Scalar.As<T, double>(a) / Scalar.As<T, double>(b)));
    }

    public AxialCoordinates<T> PositiveMod<TN>(TN b)
    {
        return this % Scalar.As<TN, T>(b);
    }
    
    public AxialCoordinates<T> PositiveMod(T b)
    {
        return this % b;
    }
    
    public static AxialCoordinates<T> operator %(AxialCoordinates<T> left, T right)
    {
        return new AxialCoordinates<T>(PositiveMod(left.Q, right), PositiveMod(left.R, right));
    }
    
    public void Deconstruct(out T q, out T r, out T s)
    {
        q = Q;
        r = R;
        s = S;
    }
    public IEnumerable<AxialCoordinates<T>> Rings(T i)
    {
        return RingsCalculation(i, this);
    }
}