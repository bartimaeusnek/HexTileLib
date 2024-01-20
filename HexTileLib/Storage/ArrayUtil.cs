namespace HexTileLib.Storage;

using System;
using System.Runtime.CompilerServices;

public static class ArrayUtil<T>
{
    private static T _oob = default(T);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set2DCoordTo1DArray(Span<T> array, int indexX, int indexY, int sizeY, T value)
    {
        var index = indexX * sizeY + indexY;
        if (index >= 0 && index < array.Length)
            array[indexX * sizeY + indexY] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Get2DCoordFrom1DArray(Span<T> array, int indexX, int indexY, int sizeY)
    {
        var index = indexX * sizeY + indexY;
        if (index >= 0 && index < array.Length)
            return ref array[index];
        return ref _oob;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool IsNullref(ref T element)
    {
        var oobPtr = Unsafe.AsPointer(ref _oob);
        var tilePtr = Unsafe.AsPointer(ref element);
        return oobPtr == tilePtr;
    }
}