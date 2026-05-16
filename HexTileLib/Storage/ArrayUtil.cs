namespace HexTileLib.Storage;

using System;
using System.Runtime.CompilerServices;

public static class ArrayUtil<T>
{
  
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set2DCoordTo1DArrayColumnMajor(Span<T> array, int indexX, int indexY, int sizeX, T value)
    {
        var index = indexY * sizeX + indexX;
        if (index >= 0 && index < array.Length)
            array[index] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static
#if !NET5_0_OR_GREATER
        unsafe
#endif
        ref T Get2DCoordFrom1DArrayColumnMajor(Span<T> array, int indexX, int indexY, int sizeX)
    {
        var index = indexY * sizeX + indexX;
        if (index >= 0 && index < array.Length)
            return ref array[index];
#if NET5_0_OR_GREATER
        return ref Unsafe.NullRef<T>();
#else
        return ref Unsafe.AsRef<T>(IntPtr.Zero.ToPointer());
#endif
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Set2DCoordTo1DArrayRowMajor(Span<T> array, int indexX, int indexY, int sizeY, T value)
    {
        var index = indexX * sizeY + indexY;
        if (index >= 0 && index < array.Length)
            array[index] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static 
#if !NET5_0_OR_GREATER
    unsafe
#endif
    ref T Get2DCoordFrom1DArrayRowMajor(Span<T> array, int indexX, int indexY, int sizeY)
    {
        var index = indexX * sizeY + indexY;
        if (index >= 0 && index < array.Length)
            return ref array[index];
#if NET5_0_OR_GREATER
        return ref Unsafe.NullRef<T>();
#else
        return ref Unsafe.AsRef<T>(IntPtr.Zero.ToPointer());
#endif
    }

#if NET5_0_OR_GREATER
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullref(ref T element)
    {
        return Unsafe.IsNullRef(ref element);
    }
#else
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool IsNullref(ref T element)
    {
        var oobPtr = IntPtr.Zero.ToPointer();
        var tilePtr = Unsafe.AsPointer(ref element);
        return oobPtr == tilePtr;
    }
#endif
}