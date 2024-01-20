namespace HexTileLib.Storage;

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Coordinates;
using Coordinates.DoubledCoordinates;
using Silk.NET.Maths;

/**
 * If set to ignore, you will end up with unused space!
 */
public enum FixedSizedStorageShape
{
    Rhombus,
    Rectangle,
    Ignore
}

/**
 * If FixedSizedStorageShape is set to ignore, you will end up with unused space!
 */
[DebuggerTypeProxy(typeof(CustomAccessStorageDebugView<,>), Target = typeof(CustomAccessStorage<,>))]
public readonly struct CustomAccessStorage<TVectorKeyType, TValue>(CustomAccessStorage<TVectorKeyType, TValue>.StorageAccess storageAccess, int height, FixedSizedStorageShape shape)
    where TVectorKeyType : unmanaged, IFormattable, IEquatable<TVectorKeyType>, IComparable<TVectorKeyType>
{
    internal readonly StorageAccess _storageAccess = storageAccess;
    internal readonly int _height = height;
    internal readonly FixedSizedStorageShape _shape = shape;

    public delegate Span<TValue> StorageAccess();
    
    public static CustomAccessStorage<TVectorKeyType, TValue> CreateArrayStorage(int width, int height, FixedSizedStorageShape shape)
    {
        var array = new TValue[width * height];
        return new CustomAccessStorage<TVectorKeyType, TValue>(array.AsSpan, height, shape);
    }
    
    public void Add(AxialCoordinates<TVectorKeyType> coordinates, TValue obj)
    {
        switch (_shape)
        {
            case FixedSizedStorageShape.Rectangle:
            {
                var doubleCoordinates = coordinates.ToDoubleCoordinates();
                var q = CalculateRectangleQ(doubleCoordinates.Q, doubleCoordinates.R);
                ArrayUtil<TValue>.Set2DCoordTo1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(doubleCoordinates.R), Scalar.As<TVectorKeyType, int>(q), _height, obj);
                return;
            }
            case FixedSizedStorageShape.Rhombus:
            case FixedSizedStorageShape.Ignore:
            {
                ArrayUtil<TValue>.Set2DCoordTo1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(coordinates.R), Scalar.As<TVectorKeyType, int>(coordinates.Q), _height, obj);
                return;
            }
            default:
                throw new InvalidConstraintException("FixedSizedStorage can only store Rectangle or Rhombus maps!");
        }
    }
    
    public void Add(DoubleHeightCoordinates<TVectorKeyType> coordinates, TValue obj)
    {
        switch (_shape)
        {
            case FixedSizedStorageShape.Rhombus:
            {
                var doubleCoordinates = coordinates.ToAxialCoordinates();
                ArrayUtil<TValue>.Set2DCoordTo1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(doubleCoordinates.R), Scalar.As<TVectorKeyType, int>(doubleCoordinates.Q), _height, obj);
                return;
            }
            case FixedSizedStorageShape.Rectangle:
            case FixedSizedStorageShape.Ignore:
            {
                var q = CalculateRectangleQ(coordinates.Q, coordinates.R);
                ArrayUtil<TValue>.Set2DCoordTo1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(coordinates.R), Scalar.As<TVectorKeyType, int>(q), _height, obj);
                return;
            }
            default:
                throw new InvalidConstraintException("FixedSizedStorage can only store Rectangle or Rhombus maps!");
        }
    }
    
    public void Add(DoubleWidthCoordinates<TVectorKeyType> coordinates, TValue obj)
    {
        switch (_shape)
        {
            case FixedSizedStorageShape.Rhombus:
            {
                var doubleCoordinates = coordinates.ToAxialCoordinates();
                ArrayUtil<TValue>.Set2DCoordTo1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(doubleCoordinates.R), Scalar.As<TVectorKeyType, int>(doubleCoordinates.Q), _height, obj);
                return;
            }
            case FixedSizedStorageShape.Rectangle:
            case FixedSizedStorageShape.Ignore:
            {
                var q = CalculateRectangleQ(coordinates.Q, coordinates.R);
                ArrayUtil<TValue>.Set2DCoordTo1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(coordinates.R), Scalar.As<TVectorKeyType, int>(q), _height, obj);
                return;
            }
            default:
                throw new InvalidConstraintException("FixedSizedStorage can only store Rectangle or Rhombus maps!");
        }
    }

    private static TVectorKeyType CalculateRectangleQ(TVectorKeyType q, TVectorKeyType r)
    {
        return Scalar.As<float, TVectorKeyType>(Scalar.Add(Scalar.As<TVectorKeyType, float>(q), Scalar.Floor(Scalar.Divide(Scalar.As<TVectorKeyType, float>(r), Scalar<float>.Two))));
    }
    
    public ref TValue Get(DoubleHeightCoordinates<TVectorKeyType> coordinates)
    {
        switch (_shape)
        {
            case FixedSizedStorageShape.Rhombus:
            {
                var axialCoordinates = coordinates.ToAxialCoordinates();
                return ref ArrayUtil<TValue>.Get2DCoordFrom1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(axialCoordinates.R), Scalar.As<TVectorKeyType, int>(axialCoordinates.Q), _height);
            }
            case FixedSizedStorageShape.Rectangle:
            case FixedSizedStorageShape.Ignore:
            {
                var q = CalculateRectangleQ(coordinates.Q, coordinates.R);
                return ref ArrayUtil<TValue>.Get2DCoordFrom1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(coordinates.R), Scalar.As<TVectorKeyType, int>(q), _height);
            }
            default:
                throw new InvalidConstraintException("FixedSizedStorage can only store Rectangle or Rhombus maps!");
        }
    }
    public ref TValue Get(DoubleWidthCoordinates<TVectorKeyType> coordinates)
    {
        switch (_shape)
        {
            case FixedSizedStorageShape.Rhombus:
            {
                var axialCoordinates = coordinates.ToAxialCoordinates();
                return ref ArrayUtil<TValue>.Get2DCoordFrom1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(axialCoordinates.R), Scalar.As<TVectorKeyType, int>(axialCoordinates.Q), _height);
            }
            case FixedSizedStorageShape.Rectangle:
            case FixedSizedStorageShape.Ignore:
            {
                var q = CalculateRectangleQ(coordinates.Q, coordinates.R);
                return ref ArrayUtil<TValue>.Get2DCoordFrom1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(coordinates.R), Scalar.As<TVectorKeyType, int>(q), _height);
            }
            default:
                throw new InvalidConstraintException("FixedSizedStorage can only store Rectangle or Rhombus maps!");
        }
    }
    public ref TValue Get(AxialCoordinates<TVectorKeyType> coordinates)
    {
        switch (_shape)
        {
            case FixedSizedStorageShape.Rectangle:
            {
                var doubleCoordinates = coordinates.ToDoubleCoordinates();
                var q = CalculateRectangleQ(doubleCoordinates.Q, doubleCoordinates.R);
                return ref ArrayUtil<TValue>.Get2DCoordFrom1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(doubleCoordinates.R), Scalar.As<TVectorKeyType, int>(q), _height);
            }
            case FixedSizedStorageShape.Rhombus:
            case FixedSizedStorageShape.Ignore:
            {
                return ref ArrayUtil<TValue>.Get2DCoordFrom1DArray(_storageAccess(), Scalar.As<TVectorKeyType, int>(coordinates.R), Scalar.As<TVectorKeyType, int>(coordinates.Q), _height);
            }
            default:
                throw new InvalidConstraintException("FixedSizedStorage can only store Rectangle or Rhombus maps!");
        }
    }
}
public class CustomAccessStorageDebugView<TVectorKeyType, TValue> where TVectorKeyType: unmanaged, IFormattable, IEquatable<TVectorKeyType>, IComparable<TVectorKeyType>
{
    private readonly CustomAccessStorage<TVectorKeyType, TValue> _collection;
    public CustomAccessStorageDebugView(CustomAccessStorage<TVectorKeyType, TValue> collection)
    {
        _collection = collection;
    }
    
    public Span<TValue> MemoryView
    {
        get
        {
            return _collection._storageAccess();
        }
    }
    
    public TValue[,] TableView
    {
        get
        {
            var col = _collection._storageAccess();
            var width = col.Length / _collection._height;
            var ret = new TValue[width, _collection._height];
            int position = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < _collection._height; y++)
                {
                    ret[x, y] = col[position++];
                }
            }
            return ret;
        }
    }

    public Dictionary<AxialCoordinates<TVectorKeyType>, TValue> RhombusDictionaryView
    {
        get
        {
            var ret = new Dictionary<AxialCoordinates<TVectorKeyType>, TValue>();
            var col = _collection._storageAccess();
            var width = col.Length / _collection._height;
            var wScalar = Scalar.As<int, TVectorKeyType>(width);
            var hScalar = Scalar.As<int, TVectorKeyType>(_collection._height);
            for (var x = Scalar<TVectorKeyType>.Zero; Scalar.GreaterThan(wScalar, x); x = Scalar.Add(x, Scalar<TVectorKeyType>.One))
            {
                for (var y = Scalar<TVectorKeyType>.Zero; Scalar.GreaterThan(hScalar, y); y = Scalar.Add(y, Scalar<TVectorKeyType>.One))
                {
                    var coords = new AxialCoordinates<TVectorKeyType>(x, y);
                    try
                    {
                        ret[coords] = _collection.Get(coords);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            return ret;
        }
    }

    public Dictionary<DoubleHeightCoordinates<TVectorKeyType>, TValue> RectangleDictionaryView
    {
        get
        {
            var ret = new Dictionary<DoubleHeightCoordinates<TVectorKeyType>, TValue>();
            var col = _collection._storageAccess();
            var width = col.Length / _collection._height;
            var wScalar = Scalar.As<float, TVectorKeyType>(Scalar.Floor(Scalar.Divide(Scalar.As<int, float>(width), Scalar<float>.Two)));
            var hScalar = Scalar.As<int, TVectorKeyType>(_collection._height);
            for (var x = Scalar.Negate(wScalar); Scalar.GreaterThan(wScalar, x); x = Scalar.Add(x, Scalar<TVectorKeyType>.One))
            {
                for (var y = Scalar<TVectorKeyType>.Zero; Scalar.GreaterThan(hScalar, y); y = Scalar.Add(y, Scalar<TVectorKeyType>.One))
                {
                    var coords = new DoubleHeightCoordinates<TVectorKeyType>(x, y);
                    try
                    {
                        ret[coords] = _collection.Get(coords);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            return ret;
        }
    }
}