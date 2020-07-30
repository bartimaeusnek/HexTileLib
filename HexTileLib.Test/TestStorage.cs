using System;
using HexTileLib.Coordinates;
using HexTileLib.Coordinates.OffsetCoordinates;
using HexTileLib.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HexTileLib.Test
{
    [TestClass]
    public class TestStorage
    {
        [TestMethod]
        public void TestOverflow()
            => Assert.ThrowsException<OverflowException>(() => DynamicArrayStorage<CubeCoordinates>.PreventOverflow(ulong.MaxValue));
        

        [TestMethod]
        public void TestArrayStorage()
        {
            var storage = new ArrayBasedStorage<CubeCoordinates>(100,100);
            var cc = new CubeCoordinates(0, 0, 0);
            storage.Add(cc);
            Assert.AreEqual(cc,storage.Get(0, 0));
            Assert.AreEqual(cc, storage[0, 0]);
            cc = new CubeCoordinates(1, -2, 1);
            storage.Add(cc);
            Assert.AreEqual(cc, storage.Get(1, 1));
            Assert.AreEqual(cc, storage[1, 1]);
        }
        
        [TestMethod]
        public void TestDynamicArrayStorage()
        {
            var storage = new DynamicArrayStorage<CubeCoordinates>();
            var cc = new CubeCoordinates(0, 0, 0);
            storage.Add(cc);
            Assert.AreEqual(cc, storage.Get(0, 0));
            cc = new CubeCoordinates(1, -2, 1);
            storage.Add(cc);
            Assert.AreEqual(cc, storage.Get(1, 1));
            Assert.ThrowsException<ArgumentException>(() => storage.Add(cc));
            storage.Clear();
            for (int x = -99; x < 100; x++)
            {
                for (int y = -99; y < 100; y++)
                {
                    for (int z = -99; z < 100; z++)
                    {
                        if (x + y + z != 0)
                            continue;
                        cc = new CubeCoordinates(x, y, z);
                        storage.Add(cc);
                        Assert.AreEqual(cc, storage.Get(x, z));
                        Assert.AreEqual(cc, storage[x, z]);
                    }
                }
            }
        }

        [TestMethod]
        public void TestOffsetBaseItf()
        {
            var storage = new DynamicArrayStorage<OffsetCoordinatesBase>();
            var ec = new EvenColumn(12, 3);
            storage.Add(ec);
            Assert.AreEqual(ec,storage.Get(12, 3));
        }

        [TestMethod]
        public void TestTableBasedStorage()
        {
            var storage = new TableBasedStorage<CubeCoordinates>();
            var cc      = new CubeCoordinates(0, 0, 0);
            storage.Add(cc);
            Assert.AreEqual(cc, storage.Get(0, 0));
            Assert.AreEqual(cc, storage[0, 0]);
            cc = new CubeCoordinates(1, -2, 1);
            storage.Add(cc);
            Assert.AreEqual(cc, storage.Get(1, 1));
            Assert.AreEqual(cc, storage[1, 1]);
        }
    }
}