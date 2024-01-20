namespace HexTileLib.Coordinates.OffsetCoordinates;

using System;
using Silk.NET.Maths;

public readonly struct OddPointy<T>(T row, T col) : ICoordinates<T>, IEquatable<OddPointy<T>> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
{
#region Base
    private readonly Vector2D<T> _vector2 = new Vector2D<T>(row, col);

    public T Row
    {
        get
        {
            return _vector2.X;
        }
    }
    public T Col
    {
        get
        {
            return _vector2.Y;
        }
    }
    
    public T R
    {
        get
        {
            return Row;
        }
    }

    public T Q
    {
        get
        {
            return Col;
        }
    }
    public bool Equals(OddPointy<T> other)
    {
        return _vector2.Equals(other._vector2);
    }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (obj.GetType() != GetType())
            return false;
        return Equals((OddPointy<T>)obj);
    }
    public override int GetHashCode()
    {
        return _vector2.GetHashCode();
    }
    public static bool operator ==(OddPointy<T> left, OddPointy<T> right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(OddPointy<T> left, OddPointy<T> right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return _vector2.ToString();
    }
#endregion

    public static explicit operator CubeCoordinates<T>(OddPointy<T> evenColumn)
    {
        return OffsetToCube(evenColumn);
    }
    public static explicit operator OddPointy<T>(CubeCoordinates<T> evenColumn)
    {
        return CubeToOffset(evenColumn);
    }
    public static explicit operator AxialCoordinates<T>(OddPointy<T> evenColumn)
    {
        return (AxialCoordinates<T>)OffsetToCube(evenColumn);
    }
    public static explicit operator OddPointy<T>(AxialCoordinates<T> evenColumn)
    {
        return CubeToOffset((CubeCoordinates<T>)evenColumn);
    }

    public static explicit operator OddPointy<T>(EvenPointy<T> evenPointy)
    {
        return (OddPointy<T>)(CubeCoordinates<T>)evenPointy;
    }
    public static explicit operator OddPointy<T>(EvenFlat<T> evenRow)
    {
        return (OddPointy<T>)(CubeCoordinates<T>)evenRow;
    }
    public static explicit operator OddPointy<T>(OddFlat<T> evenRow)
    {
        return (OddPointy<T>)(CubeCoordinates<T>)evenRow;
    }

    private static OddPointy<T> CubeToOffset(CubeCoordinates<T> cube)
    {
        var (q, r, _) = cube;
        var direction = Scalar.And(r, Scalar<T>.One);
        var col = Scalar.Add(q, Scalar.Divide(Scalar.Subtract(r, direction), Scalar<T>.Two));
        var row = r;
        return new OddPointy<T>(row, col);
    }

    private static CubeCoordinates<T> OffsetToCube(OddPointy<T> offset)
    {
        var direction = Scalar.Divide(Scalar.Subtract(offset.Row, Scalar.And(offset.Row, Scalar<T>.One)), Scalar<T>.Two);
        var q = Scalar.Subtract(offset.Col, direction);
        var r = offset.Row;
        var s = Scalar.Subtract(Scalar.Negate(q), r);
        return new CubeCoordinates<T>(q, r, s);
    }
}