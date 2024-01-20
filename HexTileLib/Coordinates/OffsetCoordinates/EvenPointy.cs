namespace HexTileLib.Coordinates.OffsetCoordinates;

using System;
using Silk.NET.Maths;

public readonly struct EvenPointy<T>(T row, T col) : ICoordinates<T>, IEquatable<EvenPointy<T>> where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
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
    public bool Equals(EvenPointy<T> other)
    {
        return _vector2.Equals(other._vector2);
    }
    public override bool Equals(object obj)
    {
        return obj is EvenPointy<T> other && Equals(other);
    }
    public override int GetHashCode()
    {
        return _vector2.GetHashCode();
    }
    public static bool operator ==(EvenPointy<T> left, EvenPointy<T> right)
    {
        return Equals(left, right);
    }
    public static bool operator !=(EvenPointy<T> left, EvenPointy<T> right)
    {
        return !Equals(left, right);
    }

    public override string ToString()
    {
        return _vector2.ToString();
    }
#endregion
    
    public static explicit operator CubeCoordinates<T>(EvenPointy<T> evenColumn)
    {
        return OffsetToCube(evenColumn);
    }
    public static explicit operator EvenPointy<T>(CubeCoordinates<T> evenColumn)
    {
        return CubeToOffset(evenColumn);
    }
    public static explicit operator AxialCoordinates<T>(EvenPointy<T> evenColumn)
    {
        return (AxialCoordinates<T>)OffsetToCube(evenColumn);
    }
    public static explicit operator EvenPointy<T>(AxialCoordinates<T> evenColumn)
    {
        return CubeToOffset((CubeCoordinates<T>)evenColumn);
    }
    public static explicit operator EvenPointy<T>(EvenFlat<T> evenRow)
    {
        return (EvenPointy<T>)(CubeCoordinates<T>)evenRow;
    }
    public static explicit operator EvenPointy<T>(OddFlat<T> evenRow)
    {
        return (EvenPointy<T>)(CubeCoordinates<T>)evenRow;
    }
    public static explicit operator EvenPointy<T>(OddPointy<T> evenPointy)
    {
        return (EvenPointy<T>)(CubeCoordinates<T>)evenPointy;
    }

    private static EvenPointy<T> CubeToOffset(CubeCoordinates<T> cube)
    {
        var (q, r, _) = cube;
        var direction = Scalar.And(r, Scalar<T>.One);
        var col = Scalar.Add(q, Scalar.Divide(Scalar.Add(r, direction), Scalar<T>.Two));
        var row = r;
        return new EvenPointy<T>(row, col);
    }

    private static CubeCoordinates<T> OffsetToCube(EvenPointy<T> offset)
    {
        var direction = Scalar.Divide(Scalar.Add(offset.Row, Scalar.And(offset.Row, Scalar<T>.One)), Scalar<T>.Two);
        var q = Scalar.Subtract(offset.Col, direction);
        var r = offset.Row;
        var s = Scalar.Subtract(Scalar.Negate(q), r);
        return new CubeCoordinates<T>(q, r, s);
    }
}