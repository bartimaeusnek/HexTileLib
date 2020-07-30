using System;
using System.Collections.Generic;
using System.Numerics;
using HexTileLib.Coordinates;
using HexTileLib.Coordinates.OffsetCoordinates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HexTileLib.Test
{
    [TestClass]
    public class TestCoordinates
    {
        [TestMethod]
        public void TestEqualityCube()
        {
            var v1 = new CubeCoordinates(0, 0, 0);
            var v2 = new CubeCoordinates(0, 0, 0);
            Assert.AreEqual(v1, v1);
            Assert.AreEqual(v1, v2);
            Assert.IsTrue(v1  == v2);
            Assert.IsFalse(v1 != v2);
        }

        [TestMethod]
        public void TestConstructorSafeness()
        {
            Assert.ThrowsException<ArgumentException>(() => new CubeCoordinates(1, 0, 0));
        }

        [TestMethod]
        public void TestGetters()
        {
            var v1 = new CubeCoordinates(1, -2, 1);
            Assert.AreEqual(1,  v1.q);
            Assert.AreEqual(-2, v1.r);
            Assert.AreEqual(1,  v1.s);
        }
        
        [TestMethod]
        public void TestRotation()
        {
            var v1 = new CubeCoordinates(1, -2, 1);
            var vl = v1.RotateLeft;
            var vr = v1.RotateRight;
            var vlA = new CubeCoordinates(-1,-1,2);
            var vrA = new CubeCoordinates(2,-1, -1);
            Assert.AreEqual(vlA, vl);
            Assert.AreEqual(vrA, vr);
        }

        [TestMethod]
        public void TestLength()
        {
            var v1 = new CubeCoordinates(1, -2, 1);
            Assert.AreEqual(2, v1.Length);
        }

        [TestMethod]
        public void TestDistance()
        {
            var v1 = new CubeCoordinates(1, -2, 1);
            var v2 = new CubeCoordinates(2, -2, 0);
            Assert.AreEqual(1, v1.Distance(v2));
        }

        [TestMethod]
        public void TestEqualityAxial()
        {
            var v1 = new AxialCoordinates(0, 0);
            var v2 = new AxialCoordinates(0, 0);
            Assert.AreEqual(v1, v1);
            Assert.AreEqual(v1, v2);
            Assert.IsTrue(v1  == v2);
            Assert.IsFalse(v1 != v2);
        }

        [TestMethod]
        public void TestPlus()
        {
            var v1 = new CubeCoordinates(1, 0,  -1);
            var v2 = new CubeCoordinates(1, -1, 0);
            var v3 = v1 + v2;
            Assert.AreEqual(new CubeCoordinates(2, -1, -1), v3);
        }

        [TestMethod]
        public void TestCastAxialCube()
        {
            var v1 = new CubeCoordinates(1, -1, 0);
            var (q1, r1, i2) = (AxialCoordinates) v1;
            var v3 = new AxialCoordinates(1, -1);
            var (q, r, s) = (CubeCoordinates) v3;
            Assert.AreEqual(q1, q);
            Assert.AreEqual(r1, r);
            Assert.AreEqual(i2, s);
        }
#if NETCOREAPP
        [TestMethod]
        public void TestCastVec3()
        {
            var v1 = new CubeCoordinates(1, -1, 0);
            var v2 = (Vector3) v1;
            var v3 = new Vector3(1, -1, 0);
            var (q, r, s) = (CubeCoordinates) v3;
            Assert.AreEqual((int) v2.X, q);
            Assert.AreEqual((int) v2.Y, r);
            Assert.AreEqual((int) v2.Z, s);
        }
#endif
        [TestMethod]
        public void TestCastOffetCubeEvenRow()
        {
            var v1 = new CubeCoordinates(1, -1, 0);
            var v2 = (EvenRow) v1;
            var v3 = (CubeCoordinates) v2;
            Assert.AreEqual(v1, v3);

            var v4 = new AxialCoordinates(1, -1);
            var v5 = (EvenRow) v4;
            var v6 = (AxialCoordinates) v5;
            Assert.AreEqual(v4, v6);
        }

        [TestMethod]
        public void TestCastOffetCubeEvenColumn()
        {
            var v1 = new CubeCoordinates(1, -1, 0);
            var v2 = (EvenColumn) v1;
            var v3 = (CubeCoordinates) v2;
            Assert.AreEqual(v1, v3);

            var v4 = new AxialCoordinates(1, -1);
            var v5 = (EvenColumn) v4;
            var v6 = (AxialCoordinates) v5;
            Assert.AreEqual(v4, v6);
        }

        [TestMethod]
        public void TestCastOffetCubeOddColumn()
        {
            var v1 = new CubeCoordinates(1, -1, 0);
            var v2 = (OddColumn) v1;
            var v3 = (CubeCoordinates) v2;
            Assert.AreEqual(v1, v3);

            var v4 = new AxialCoordinates(1, -1);
            var v5 = (OddColumn) v4;
            var v6 = (AxialCoordinates) v5;
            Assert.AreEqual(v4, v6);
        }

        [TestMethod]
        public void TestCastOffetCubeOddRow()
        {
            var v1 = new CubeCoordinates(1, -1, 0);
            var v2 = (OddRow) v1;
            var v3 = (CubeCoordinates) v2;
            Assert.AreEqual(v1, v3);

            var v4 = new AxialCoordinates(1, -1);
            var v5 = (OddRow) v4;
            var v6 = (AxialCoordinates) v5;
            Assert.AreEqual(v4, v6);
        }

        [TestMethod]
        public void TestRings()
        {
            List<CubeCoordinates> range1 = new List<CubeCoordinates>
                                           {
                                               new CubeCoordinates(0,  1,  -1),
                                               new CubeCoordinates(1,  0,  -1),
                                               new CubeCoordinates(1,  -1, 0),
                                               new CubeCoordinates(0,  -1, 1),
                                               new CubeCoordinates(-1, 0,  1),
                                               new CubeCoordinates(-1, 1,  0)
                                           };
            var v1       = (CubeCoordinates) new AxialCoordinates(0, 0);
            var distance = v1.Rings(1);
            for (var i = 0; i < distance.Count; i++)
                Assert.AreEqual(range1[i], distance[i]);
            
            List<CubeCoordinates> range2 = new List<CubeCoordinates>
                                           {
                                               new CubeCoordinates(0,  2,  -2),
                                               new CubeCoordinates(1,  1,  -2),
                                               new CubeCoordinates(2,  0,  -2),
                                               new CubeCoordinates(2,  -1, -1),
                                               new CubeCoordinates(2,  -2, 0),
                                               new CubeCoordinates(1,  -2, 1),
                                               new CubeCoordinates(0,  -2, 2),
                                               new CubeCoordinates(-1, -1, 2),
                                               new CubeCoordinates(-2, 0,  2),
                                               new CubeCoordinates(-2, 1,  1),
                                               new CubeCoordinates(-2, 2,  0),
                                               new CubeCoordinates(-1, 2,  -1)
                                           };
            var distance2 = v1.Rings(2);
            for (var i = 0; i < distance2.Count; i++)
                Assert.AreEqual(range2[i], distance2[i]);
        }


        [TestMethod]
        public void TestExplicitCast()
        {
            EvenColumn ec = new CubeCoordinates(1, 0, -1);
            OddColumn  oc = new CubeCoordinates(1, 0, -1);
            EvenRow    er = new CubeCoordinates(1, 0, -1);
            OddRow     or = new CubeCoordinates(1, 0, -1);

            ec = (EvenColumn) er;
            ec = (EvenColumn) or;
            ec = (EvenColumn) oc;
        
            er = (EvenRow) ec;
            er = (EvenRow) or;
            er = (EvenRow) oc;
       
            
            or = (OddRow) ec;
            or = (OddRow) er;
            or = (OddRow) oc;
         

            
            oc = (OddColumn) ec;
            oc = (OddColumn) er;
            oc = (OddColumn) or;
        }
        
#if NETCOREAPP
        [TestMethod]
        public void TestVecCast()
        {
            var v1 = new CubeCoordinates(1,0,-1);
            Vector2 vec = v1;
            AxialCoordinates axial = vec;
            Vector3 vec3 = axial;
            AxialCoordinates axial2 = vec3;
            CubeCoordinates cube = vec3;
            Vector2 vec2 = axial;
            EvenColumn ec = axial2;
            OddColumn  oc = (OddColumn) ec;
            EvenRow    er = (EvenRow) oc;
            OddRow     or = (OddRow) er;

            vec2 = oc;
            Assert.AreEqual(v1, (CubeCoordinates) vec2);
            vec2 = or;
            Assert.AreEqual(v1, (CubeCoordinates) vec2);
            vec2 = ec;
            Assert.AreEqual(v1, (CubeCoordinates) vec2);
            vec2 = er;
            Assert.AreEqual(v1, (CubeCoordinates) vec2);
            
            oc = vec2;
            or = vec2;
            ec = vec2;
            er = vec2;

            vec3 = oc;
            Assert.AreEqual(v1, (CubeCoordinates) vec3);
            vec3 = or;
            Assert.AreEqual(v1, (CubeCoordinates) vec3);
            vec3 = ec;
            Assert.AreEqual(v1, (CubeCoordinates) vec3);
            vec3 = er;
            Assert.AreEqual(v1, (CubeCoordinates) vec3);
            
            oc = vec3;
            or = vec3;
            ec = vec3;
            er = vec3;

            Assert.AreEqual(v1,(CubeCoordinates) vec);
            Assert.AreEqual(v1,(CubeCoordinates) axial);
            Assert.AreEqual(v1,(CubeCoordinates) vec3);
            Assert.AreEqual(v1,(CubeCoordinates) cube);
            Assert.AreEqual(v1,(CubeCoordinates) vec2);
        }
#endif

        [TestMethod]
        public void TestToString()
        {
            var v1 = new CubeCoordinates(1,-1,0);
#if NET471
            var v2 = (1, -1, 0);
#elif NETCOREAPP
            var v2 = new Vector3(1, -1, 0);
#endif
            Assert.AreEqual(v2.ToString(),v1.ToString());
        }
        
        [TestMethod]
        public void TestNeighbours()
        {
            var v1 = new CubeCoordinates(0, 0, 0);
            var v2 = v1.Neighbor((CoordinateDirectionFlat) 1);
            var v3 = v1.Neighbor((CoordinateDirectionPointy) 1);
            Assert.AreEqual(v2, v3);
            Assert.AreEqual(CubeCoordinates.Direction(1), v2);
            var v4 = v1.DiagonalNeighbor((CoordinateDirectionFlatDiagonal) 1);
            var v5 = v1.DiagonalNeighbor((CoordinateDirectionPointyDiagonal) 1);
            Assert.AreEqual(CubeCoordinates.Diagonals(1), v4);
            Assert.AreEqual(v4, v5);
        }

        [TestMethod]
        public void TestArithmethic()
        {
            var v1 = new CubeCoordinates(-4, 2, 2);
            var v2 = new CubeCoordinates(2, -1, -1);

            var v3 = v1 + v2;
            Assert.AreEqual(new CubeCoordinates(-2,1,1), v3);
            var v4 = v1 - v2;
            Assert.AreEqual(new CubeCoordinates(-6, 3, 3), v4);
            var v5 = v1 * 2;
            Assert.AreEqual(new CubeCoordinates(-8, 4, 4), v5);
            var v6 = v1 / 2;
            Assert.AreEqual(new CubeCoordinates(-2, 1, 1), v6);
        }

        [TestMethod]
        public void TestHashcodeSample()
        {
            var dict = new HashSet<CubeCoordinates>();
            var dictax = new HashSet<AxialCoordinates>();
            for (int x = -99; x < 100; x++)
            {
                for (int y = -99; y < 100; y++)
                {
                    for (int z = -99; z < 100; z++)
                    {
                        if (x + y + z != 0)
                            continue;
                        Assert.IsTrue(dict.Add(new CubeCoordinates(x, y, z)));
                        Assert.IsTrue(dictax.Add(new AxialCoordinates(x, z)));
                    }
                }
            }
        }

        [TestMethod]
        public void TestCubicDistance()
        {
            List<CubeCoordinates> range1 = new List<CubeCoordinates>
                                           {
                                               new CubeCoordinates(-1, 0,  1),
                                               new CubeCoordinates(-1, 1,  0),
                                               new CubeCoordinates(0,  -1, 1),
                                               new AxialCoordinates(0, 0),
                                               new CubeCoordinates(0, 1,  -1),
                                               new CubeCoordinates(1, -1, 0),
                                               new CubeCoordinates(1, 0,  -1)
                                           };
            var v1       = (CubeCoordinates) new AxialCoordinates(0, 0);
            var distance = v1.CubicDistance(1);
            for (var i = 0; i < distance.Count; i++)
                Assert.AreEqual(range1[i],  distance[i]);

            List<CubeCoordinates> range2 = new List<CubeCoordinates>
                                           {
                                               new CubeCoordinates(-2, 0,  2),
                                               new CubeCoordinates(-2, 1,  1),
                                               new CubeCoordinates(-2, 2,  0),
                                               new CubeCoordinates(-1, -1, 2),
                                               new CubeCoordinates(-1, 0,  1),
                                               new CubeCoordinates(-1, 1,  0),
                                               new CubeCoordinates(-1, 2,  -1),
                                               new CubeCoordinates(0,  -2, 2),
                                               new CubeCoordinates(0,  -1, 1),
                                               new CubeCoordinates(0,  0,  0),
                                               new CubeCoordinates(0,  1,  -1),
                                               new CubeCoordinates(0,  2,  -2),
                                               new CubeCoordinates(1,  -2, 1),
                                               new CubeCoordinates(1,  -1, 0),
                                               new CubeCoordinates(1,  0,  -1),
                                               new CubeCoordinates(1,  1,  -2),
                                               new CubeCoordinates(2,  -2, 0),
                                               new CubeCoordinates(2,  -1, -1),
                                               new CubeCoordinates(2,  0,  -2)
                                           };
            var distance2 = v1.CubicDistance(2);
            for (var i = 0; i < distance2.Count; i++)
                Assert.AreEqual(range2[i], distance2[i]);
        }
    }
}