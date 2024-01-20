namespace HexTileLib.Coordinates;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Silk.NET.Maths;

public readonly struct CubeCoordinates<T> : IEquatable<AxialCoordinates<T>>, ICoordinates<T>, IEquatable<CubeCoordinates<T>> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
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
            return Coords.Z;
        }
    }

    public Vector3D<T> Coords
    {
        get;
    }

    public CubeCoordinates(T q, T r, T s)
    {
        if (Scalar.NotEqual(Scalar.Add(Scalar.Add(q, r), s), Scalar<T>.Zero))
        {
            throw new ArgumentException($"{nameof(CubeCoordinates<T>)}: {nameof(q)} + {nameof(r)} + {nameof(s)} != 0");
        }
        Coords = new Vector3D<T>(q, r, s);
    }


    public T Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return LengthCalculation(this);
        }
    }


    public CubeCoordinates<T> RotateLeft
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return RotateLeftCalculation(this);
        }
    }


    public CubeCoordinates<T> RotateRight
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return RotateRightCalculation(this);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Distance(CubeCoordinates<T> coordinates)
    {
        return DistanceCalculation(this, coordinates);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CubeCoordinates<T> Neighbor(CoordinateDirectionFlat direction)
    {
        return NeighborCalculation(this, (int)direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CubeCoordinates<T> Neighbor(CoordinateDirectionPointy direction)
    {
        return NeighborCalculation(this, (int)direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CubeCoordinates<T> DiagonalNeighbor(CoordinateDirectionFlatDiagonal direction)
    {
        return DiagonalNeighborCalculation(this, (int)direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CubeCoordinates<T> DiagonalNeighbor(CoordinateDirectionPointyDiagonal direction)
    {
        return DiagonalNeighborCalculation(this, (int)direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T LengthCalculation(CubeCoordinates<T> hex)
    {
        return hex.Coords.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T DistanceCalculation(CubeCoordinates<T> a, CubeCoordinates<T> b)
    {
        return Vector3D.Distance(a.Coords, b.Coords);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CubeCoordinates<T> RotateLeftCalculation(CubeCoordinates<T> coordinates)
    {
        return new CubeCoordinates<T>(Scalar.Negate(coordinates.S), Scalar.Negate(coordinates.Q), Scalar.Negate(coordinates.R));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CubeCoordinates<T> RotateRightCalculation(CubeCoordinates<T> coordinates)
    {
        return new CubeCoordinates<T>(Scalar.Negate(coordinates.R), Scalar.Negate(coordinates.S), Scalar.Negate(coordinates.Q));
    }

    public static List<CubeCoordinates<T>> directions = new List<CubeCoordinates<T>>
    {
        new CubeCoordinates<T>(Scalar<T>.One, Scalar<T>.MinusOne, Scalar<T>.Zero),
        new CubeCoordinates<T>(Scalar<T>.Zero, Scalar<T>.MinusOne, Scalar<T>.One),
        new CubeCoordinates<T>(Scalar<T>.MinusOne, Scalar<T>.Zero, Scalar<T>.One),
        new CubeCoordinates<T>(Scalar<T>.MinusOne, Scalar<T>.One, Scalar<T>.Zero),
        new CubeCoordinates<T>(Scalar<T>.Zero, Scalar<T>.One, Scalar<T>.MinusOne),
        new CubeCoordinates<T>(Scalar<T>.One, Scalar<T>.Zero, Scalar<T>.MinusOne)
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CubeCoordinates<T> Direction(int direction)
    {
        return directions[direction];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CubeCoordinates<T> NeighborCalculation(CubeCoordinates<T> coordinates, int direction)
    {
        return coordinates + Direction(direction);
    }

    public static readonly List<CubeCoordinates<T>> diagonals = new List<CubeCoordinates<T>>
    {
        new CubeCoordinates<T>(Scalar<T>.Two, Scalar<T>.MinusOne, Scalar<T>.MinusOne),
        new CubeCoordinates<T>(Scalar<T>.One, Scalar<T>.MinusTwo, Scalar<T>.One),
        new CubeCoordinates<T>(Scalar<T>.MinusOne, Scalar<T>.MinusOne, Scalar<T>.Two),
        new CubeCoordinates<T>(Scalar<T>.MinusTwo, Scalar<T>.One, Scalar<T>.One),
        new CubeCoordinates<T>(Scalar<T>.MinusOne, Scalar<T>.Two, Scalar<T>.MinusOne),
        new CubeCoordinates<T>(Scalar<T>.One, Scalar<T>.One, Scalar<T>.MinusTwo)
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CubeCoordinates<T> Diagonals(int direction)
    {
        return diagonals[direction];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static CubeCoordinates<T> DiagonalNeighborCalculation(CubeCoordinates<T> coordinates, int direction)
    {
        return coordinates + Diagonals(direction);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static List<CubeCoordinates<T>> CubicDistanceCalculation(T range, CubeCoordinates<T> center)
    {
        var results = new List<CubeCoordinates<T>>();
        var (x1, y1, z1) = center;

        for (var x = Scalar.Add(Scalar.Negate(range), x1);
             Scalar.LessThanOrEqual(x, Scalar.Add(range, x1));
             x = Scalar.Add(x, Scalar<T>.One))
        {
            for (var y = Scalar.Add(Scalar.Negate(range), y1);
                 Scalar.LessThanOrEqual(y, Scalar.Add(range, y1));
                 y = Scalar.Add(y, Scalar<T>.One))
            {
                for (var z = Scalar.Add(Scalar.Negate(range), z1);
                     Scalar.LessThanOrEqual(z, Scalar.Add(range, z1));
                     z = Scalar.Add(z, Scalar<T>.One))
                {
                    if (Scalar.NotEqual(Scalar.Add(Scalar.Add(x, y), z), Scalar<T>.Zero))
                        continue;
                    results.Add(new CubeCoordinates<T>(x, y, z));
                }
            }
        }

        return results;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public List<CubeCoordinates<T>> Rings(T range)
    {
        return RingsCalculation(range, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public List<CubeCoordinates<T>> CubicDistance(T range)
    {
        return CubicDistanceCalculation(range, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static List<CubeCoordinates<T>> RingsCalculation(T range, CubeCoordinates<T> center)
    {
        var results = new List<CubeCoordinates<T>>();
        var point = center.Neighbor((CoordinateDirectionFlat)4);

        for (var i = Scalar<T>.One; Scalar.LessThan(i, range); i = Scalar.Add(i, Scalar<T>.One))
        {
            point = point.Neighbor((CoordinateDirectionFlat)4);
        }
        for (int i = 0; i < 6; i++)
        {
            for (var j = Scalar<T>.Zero; Scalar.LessThan(j, range); j = Scalar.Add(j, Scalar<T>.One))
            {
                results.Add(point);
                point = point.Neighbor((CoordinateDirectionFlat)i);
            }
        }
        return results;
    }

#region C#Specific
    public void Deconstruct(out T q, out T r, out T s)
    {
        q = this.Q;
        r = this.R;
        s = this.S;
    }
    private CubeCoordinates(Vector3D<T> left)
    {
        Coords = left;
    }
    public static explicit operator CubeCoordinates<T>(Vector3D<T> left)
    {
        return new CubeCoordinates<T>(left);
    }
    public static explicit operator Vector3D<T>(CubeCoordinates<T> left)
    {
        return left.Coords;
    }
    public static explicit operator AxialCoordinates<T>(CubeCoordinates<T> left)
    {
        return new AxialCoordinates<T>(left.Q, left.S);
    }
    public static explicit operator CubeCoordinates<T>(AxialCoordinates<T> left)
    {
        return new CubeCoordinates<T>(left.Q, left.S, left.R);
    }
    public bool Equals(CubeCoordinates<T> other)
    {
        return Coords.Equals(other.Coords);
    }
    public bool Equals(AxialCoordinates<T> other)
    {
        return Equals((CubeCoordinates<T>)other);
    }
    public override bool Equals(object obj)
    {
        return obj is CubeCoordinates<T> other && Equals(other);
    }
    public override int GetHashCode()
    {
        return Coords.GetHashCode();
    }
    public static bool operator ==(CubeCoordinates<T> left, CubeCoordinates<T> right)
    {
        return left.Equals(right);
    }
    public static bool operator !=(CubeCoordinates<T> left, CubeCoordinates<T> right)
    {
        return !left.Equals(right);
    }
    public static CubeCoordinates<T> operator +(CubeCoordinates<T> left, CubeCoordinates<T> right)
    {
        return (CubeCoordinates<T>)(left.Coords + right.Coords);
    }
    public static CubeCoordinates<T> operator -(CubeCoordinates<T> left, CubeCoordinates<T> right)
    {
        return (CubeCoordinates<T>)(left.Coords - right.Coords);
    }
    public static CubeCoordinates<T> operator *(CubeCoordinates<T> left, T right)
    {
        return (CubeCoordinates<T>)(left.Coords * right);
    }
    public static CubeCoordinates<T> operator /(CubeCoordinates<T> left, T right)
    {
        return (CubeCoordinates<T>)(left.Coords / right);
    }
    public override string ToString()
    {
        return Coords.ToString();
    }
#endregion
}