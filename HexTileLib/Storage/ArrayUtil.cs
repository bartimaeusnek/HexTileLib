using System;

namespace HexTileLib.Storage
{
    public static class ArrayUtil
    {
        public static void Set2DCoordTo1DArray<T>(this T[] array, int indexX, int indexY, int sizeY, T value) {
            var index = indexX * sizeY + indexY;
            array[index] = value;
        }

        public static T Get2DCoordFrom1DArray<T>(this T[] array, int indexX, int indexY, int sizeY) {
            var index = indexX * sizeY + indexY;
            return array[index];
        }     
        
        public static T[] ResizeStorage<T>(this T[] array, ulong factor)
        {
            var s = new T[(ulong)array.LongLength + factor];
            Array.Copy(array, s, array.LongLength);
            array = s;
            return array;
        }
    }
}