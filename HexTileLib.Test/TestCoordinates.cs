namespace HexTileLib.Test;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Coordinates;
using Coordinates.OffsetCoordinates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Silk.NET.Maths;

[TestClass]
public class TestCoordinates
{
    [TestMethod]
    public void TestEqualityCube()
    {
        var v1 = new CubeCoordinates<int>(0, 0, 0);
        var v2 = new CubeCoordinates<int>(0, 0, 0);
        Assert.AreEqual(v1, v1);
        Assert.AreEqual(v1, v2);
        Assert.IsTrue(v1 == v2);
        Assert.IsFalse(v1 != v2);
    }

    [TestMethod]
    public void TestConstructorSafeness()
    {
        Assert.ThrowsException<ArgumentException>(() => new CubeCoordinates<int>(1, 0, 0));
    }

    [TestMethod]
    public void TestGetters()
    {
        var v1 = new CubeCoordinates<int>(1, -2, 1);
        Assert.AreEqual(1, v1.Q);
        Assert.AreEqual(-2, v1.R);
        Assert.AreEqual(1, v1.S);
    }

    [TestMethod]
    public void TestRotation()
    {
        var v1 = new CubeCoordinates<int>(1, -2, 1);
        var vl = v1.RotateLeft;
        var vr = v1.RotateRight;
        var vlA = new CubeCoordinates<int>(-1, -1, 2);
        var vrA = new CubeCoordinates<int>(2, -1, -1);
        Assert.AreEqual(vlA, vl);
        Assert.AreEqual(vrA, vr);
    }

    [TestMethod]
    public void TestLength()
    {
        var v1 = new CubeCoordinates<int>(1, -2, 1);
        Assert.AreEqual(2, v1.Length);
    }

    [TestMethod]
    public void TestDistance()
    {
        var v1 = new CubeCoordinates<int>(1, -2, 1);
        var v2 = new CubeCoordinates<int>(2, -2, 0);
        Assert.AreEqual(1, v1.Distance(v2));
    }

    [TestMethod]
    public void TestEqualityAxial()
    {
        var v1 = new AxialCoordinates<int>(0, 0);
        var v2 = new AxialCoordinates<int>(0, 0);
        Assert.AreEqual(v1, v1);
        Assert.AreEqual(v1, v2);
        Assert.IsTrue(v1 == v2);
        Assert.IsFalse(v1 != v2);
    }

    [TestMethod]
    public void TestPlus()
    {
        var v1 = new CubeCoordinates<int>(1, 0, -1);
        var v2 = new CubeCoordinates<int>(1, -1, 0);
        var v3 = v1 + v2;
        Assert.AreEqual(new CubeCoordinates<int>(2, -1, -1), v3);
    }

    [TestMethod]
    public void TestCastAxialCube()
    {
        var v1 = new CubeCoordinates<int>(1, -1, 0);
        (int q1, int r1, int i2) = (AxialCoordinates<int>)v1;
        var v3 = new AxialCoordinates<int>(1, -1);
        (int q, int r, int s) = (CubeCoordinates<int>)v3;
        Assert.AreEqual(q1, q);
        Assert.AreEqual(r1, r);
        Assert.AreEqual(i2, s);
    }
    
    [TestMethod]
    public void TestCastVec3()
    {
        var v1 = new CubeCoordinates<int>(1, -1, 0);
        var v2 = (Vector3D<int>) v1;
        var v3 = new Vector3D<int>(1, -1, 0);
        var (q, r, s) = (CubeCoordinates<int>) v3;
        Assert.AreEqual(v2.X, q);
        Assert.AreEqual(v2.Y, r);
        Assert.AreEqual(v2.Z, s);
    }
    
    [TestMethod]
    public void TestCastOffsetCubeEvenRow()
    {
        var v1 = new CubeCoordinates<int>(1, -1, 0);
        var v2 = (EvenPointy<int>)v1;
        var v3 = (CubeCoordinates<int>)v2;
        Assert.AreEqual(v1, v3);

        var v4 = new AxialCoordinates<int>(1, -1);
        var v5 = (EvenPointy<int>)v4;
        var v6 = (AxialCoordinates<int>)v5;
        Assert.AreEqual(v4, v6);
    }

    [TestMethod]
    public void TestCastOffsetCubeEvenColumn()
    {
        var v1 = new CubeCoordinates<int>(1, -1, 0);
        var v2 = (EvenFlat<int>)v1;
        var v3 = (CubeCoordinates<int>)v2;
        Assert.AreEqual(v1, v3);

        var v4 = new AxialCoordinates<int>(1, -1);
        var v5 = (EvenFlat<int>)v4;
        var v6 = (AxialCoordinates<int>)v5;
        Assert.AreEqual(v4, v6);
    }

    [TestMethod]
    public void TestCastOffsetCubeOddColumn()
    {
        var v1 = new CubeCoordinates<int>(1, -1, 0);
        var v2 = (OddFlat<int>)v1;
        var v3 = (CubeCoordinates<int>)v2;
        Assert.AreEqual(v1, v3);

        var v4 = new AxialCoordinates<int>(1, -1);
        var v5 = (OddFlat<int>)v4;
        var v6 = (AxialCoordinates<int>)v5;
        Assert.AreEqual(v4, v6);
    }

    [TestMethod]
    public void TestCastOffsetCubeOddRow()
    {
        var v1 = new CubeCoordinates<int>(1, -1, 0);
        var v2 = (OddPointy<int>)v1;
        var v3 = (CubeCoordinates<int>)v2;
        Assert.AreEqual(v1, v3);

        var v4 = new AxialCoordinates<int>(1, -1);
        var v5 = (OddPointy<int>)v4;
        var v6 = (AxialCoordinates<int>)v5;
        Assert.AreEqual(v4, v6);
    }

    [TestMethod]
    public void TestRings()
    {
        var range1 = new List<CubeCoordinates<int>>
        {
            new CubeCoordinates<int>(0, 1, -1),
            new CubeCoordinates<int>(1, 0, -1),
            new CubeCoordinates<int>(1, -1, 0),
            new CubeCoordinates<int>(0, -1, 1),
            new CubeCoordinates<int>(-1, 0, 1),
            new CubeCoordinates<int>(-1, 1, 0)
        };
        var v1 = (CubeCoordinates<int>)new AxialCoordinates<int>(0, 0);
        var distance = v1.Rings(1);
        for (int i = 0; i < distance.Count; i++)
            Assert.AreEqual(range1[i], distance[i]);

        var range2 = new List<CubeCoordinates<int>>
        {
            new CubeCoordinates<int>(0, 2, -2),
            new CubeCoordinates<int>(1, 1, -2),
            new CubeCoordinates<int>(2, 0, -2),
            new CubeCoordinates<int>(2, -1, -1),
            new CubeCoordinates<int>(2, -2, 0),
            new CubeCoordinates<int>(1, -2, 1),
            new CubeCoordinates<int>(0, -2, 2),
            new CubeCoordinates<int>(-1, -1, 2),
            new CubeCoordinates<int>(-2, 0, 2),
            new CubeCoordinates<int>(-2, 1, 1),
            new CubeCoordinates<int>(-2, 2, 0),
            new CubeCoordinates<int>(-1, 2, -1)
        };
        var distance2 = v1.Rings(2);
        for (int i = 0; i < distance2.Count; i++)
            Assert.AreEqual(range2[i], distance2[i]);
    }


    [TestMethod]
    [SuppressMessage("ReSharper", "RedundantAssignment")]
    public void TestExplicitCast()
    {
        try
        {
            var ec = (EvenFlat<int>)new CubeCoordinates<int>(1, 0, -1);
            var oc = (OddFlat<int>)new CubeCoordinates<int>(1, 0, -1);
            var er = (EvenPointy<int>)new CubeCoordinates<int>(1, 0, -1);
            var or = (OddPointy<int>)new CubeCoordinates<int>(1, 0, -1);

            ec = (EvenFlat<int>)er;
            ec = (EvenFlat<int>)or;
            ec = (EvenFlat<int>)oc;

            er = (EvenPointy<int>)ec;
            er = (EvenPointy<int>)or;
            er = (EvenPointy<int>)oc;

            or = (OddPointy<int>)ec;
            or = (OddPointy<int>)er;
            or = (OddPointy<int>)oc;

            oc = (OddFlat<int>)ec;
            oc = (OddFlat<int>)er;
            oc = (OddFlat<int>)or;
        }
        catch (Exception)
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public void TestVecCast()
    {
        var cube = new CubeCoordinates<int>(1, 0, -1);
        var axial = (AxialCoordinates<int>)cube;
        var evenFlat = (EvenFlat<int>)cube;
        var oddFlat = (OddFlat<int>)cube;
        var evenPointy = (EvenPointy<int>)cube;
        var oddPointy = (OddPointy<int>)cube;

        Assert.AreEqual(cube, (CubeCoordinates<int>)axial);
        Assert.AreEqual(cube, (CubeCoordinates<int>)evenFlat);
        Assert.AreEqual(cube, (CubeCoordinates<int>)oddFlat);
        Assert.AreEqual(cube, (CubeCoordinates<int>)evenPointy);
        Assert.AreEqual(cube, (CubeCoordinates<int>)oddPointy);

        Assert.AreEqual(axial, (AxialCoordinates<int>)cube);
        Assert.AreEqual(axial, (AxialCoordinates<int>)evenFlat);
        Assert.AreEqual(axial, (AxialCoordinates<int>)oddFlat);
        Assert.AreEqual(axial, (AxialCoordinates<int>)evenPointy);
        Assert.AreEqual(axial, (AxialCoordinates<int>)oddPointy);

        Assert.AreEqual(evenFlat, (EvenFlat<int>)cube);
        Assert.AreEqual(evenFlat, (EvenFlat<int>)axial);
        Assert.AreEqual(evenFlat, (EvenFlat<int>)oddFlat);
        Assert.AreEqual(evenFlat, (EvenFlat<int>)evenPointy);
        Assert.AreEqual(evenFlat, (EvenFlat<int>)oddPointy);

        Assert.AreEqual(oddFlat, (OddFlat<int>)cube);
        Assert.AreEqual(oddFlat, (OddFlat<int>)axial);
        Assert.AreEqual(oddFlat, (OddFlat<int>)evenFlat);
        Assert.AreEqual(oddFlat, (OddFlat<int>)evenPointy);
        Assert.AreEqual(oddFlat, (OddFlat<int>)oddPointy);

        Assert.AreEqual(evenPointy, (EvenPointy<int>)cube);
        Assert.AreEqual(evenPointy, (EvenPointy<int>)axial);
        Assert.AreEqual(evenPointy, (EvenPointy<int>)evenFlat);
        Assert.AreEqual(evenPointy, (EvenPointy<int>)oddFlat);
        Assert.AreEqual(evenPointy, (EvenPointy<int>)oddPointy);

        Assert.AreEqual(oddPointy, (OddPointy<int>)cube);
        Assert.AreEqual(oddPointy, (OddPointy<int>)axial);
        Assert.AreEqual(oddPointy, (OddPointy<int>)evenFlat);
        Assert.AreEqual(oddPointy, (OddPointy<int>)oddFlat);
        Assert.AreEqual(oddPointy, (OddPointy<int>)evenPointy);
    }

    [TestMethod]
    public void TestToString()
    {
        var v1 = new CubeCoordinates<int>(1, -1, 0);
        var v2 = new Vector3D<int>(1, -1, 0);
        Assert.AreEqual(v2.ToString(), v1.ToString());
    }

    [TestMethod]
    public void TestNeighbours()
    {
        var v1 = new CubeCoordinates<int>(0, 0, 0);
        var v2 = v1.Neighbor((CoordinateDirectionFlat)1);
        var v3 = v1.Neighbor((CoordinateDirectionPointy)1);
        Assert.AreEqual(v2, v3);
        Assert.AreEqual(CubeCoordinates<int>.Direction(1), v2);
        var v4 = v1.DiagonalNeighbor((CoordinateDirectionFlatDiagonal)1);
        var v5 = v1.DiagonalNeighbor((CoordinateDirectionPointyDiagonal)1);
        Assert.AreEqual(CubeCoordinates<int>.Diagonals(1), v4);
        Assert.AreEqual(v4, v5);
    }

    [TestMethod]
    public void TestArithmetic()
    {
        var v1 = new CubeCoordinates<int>(-4, 2, 2);
        var v2 = new CubeCoordinates<int>(2, -1, -1);

        var v3 = v1 + v2;
        Assert.AreEqual(new CubeCoordinates<int>(-2, 1, 1), v3);
        var v4 = v1 - v2;
        Assert.AreEqual(new CubeCoordinates<int>(-6, 3, 3), v4);
        var v5 = v1 * 2;
        Assert.AreEqual(new CubeCoordinates<int>(-8, 4, 4), v5);
        var v6 = v1 / 2;
        Assert.AreEqual(new CubeCoordinates<int>(-2, 1, 1), v6);
    }

    [TestMethod]
    public void TestHashcodeSample()
    {
        var dict = new HashSet<CubeCoordinates<int>>();
        var dictax = new HashSet<AxialCoordinates<int>>();
        for (int x = -99; x < 100; x++)
        {
            for (int y = -99; y < 100; y++)
            {
                for (int z = -99; z < 100; z++)
                {
                    if (x + y + z != 0)
                        continue;
                    Assert.IsTrue(dict.Add(new CubeCoordinates<int>(x, y, z)));
                    Assert.IsTrue(dictax.Add(new AxialCoordinates<int>(x, z)));
                }
            }
        }
    }

    [TestMethod]
    public void TestCubicDistance()
    {
        var range1 = new List<CubeCoordinates<int>>
        {
            new CubeCoordinates<int>(-1, 0, 1),
            new CubeCoordinates<int>(-1, 1, 0),
            new CubeCoordinates<int>(0, -1, 1),
            (CubeCoordinates<int>)new AxialCoordinates<int>(0, 0),
            new CubeCoordinates<int>(0, 1, -1),
            new CubeCoordinates<int>(1, -1, 0),
            new CubeCoordinates<int>(1, 0, -1)
        };
        var v1 = (CubeCoordinates<int>)new AxialCoordinates<int>(0, 0);
        var distance = v1.CubicDistance(1);
        for (int i = 0; i < distance.Count; i++)
            Assert.AreEqual(range1[i], distance[i]);

        var range2 = new List<CubeCoordinates<int>>
        {
            new CubeCoordinates<int>(-2, 0, 2),
            new CubeCoordinates<int>(-2, 1, 1),
            new CubeCoordinates<int>(-2, 2, 0),
            new CubeCoordinates<int>(-1, -1, 2),
            new CubeCoordinates<int>(-1, 0, 1),
            new CubeCoordinates<int>(-1, 1, 0),
            new CubeCoordinates<int>(-1, 2, -1),
            new CubeCoordinates<int>(0, -2, 2),
            new CubeCoordinates<int>(0, -1, 1),
            new CubeCoordinates<int>(0, 0, 0),
            new CubeCoordinates<int>(0, 1, -1),
            new CubeCoordinates<int>(0, 2, -2),
            new CubeCoordinates<int>(1, -2, 1),
            new CubeCoordinates<int>(1, -1, 0),
            new CubeCoordinates<int>(1, 0, -1),
            new CubeCoordinates<int>(1, 1, -2),
            new CubeCoordinates<int>(2, -2, 0),
            new CubeCoordinates<int>(2, -1, -1),
            new CubeCoordinates<int>(2, 0, -2)
        };
        var distance2 = v1.CubicDistance(2);
        for (int i = 0; i < distance2.Count; i++)
            Assert.AreEqual(range2[i], distance2[i]);
    }

    [TestMethod]
    public void TestAxialDoubleConversion()
    {
        for (int x = -512; x < 512; x++)
        {
            for (int y = -512; y < 512; y++)
            {
                var ax = new AxialCoordinates<int>(x, y);
                var ax2 = ax.ToDoubleCoordinates().ToAxialCoordinates();
                Assert.AreEqual(ax,ax2);
            }
        }
        var tollerance = new Vector2D<float>(0.001f);
        for (float x = -1.0f; x < 1.0f; x+=0.01f)
        {
            for (float y = -1.0f; y < 1.0f; y+=0.01f)
            {
                var ax = new AxialCoordinates<float>(x, y);
                var ax2 = ax.ToDoubleCoordinates().ToAxialCoordinates();
                var a = Vector2D.Abs(ax.Coords - ax2.Coords);
                var b = Vector2D.Min(a, tollerance);
                Assert.AreEqual(a,b);
            }
        }
    }
}