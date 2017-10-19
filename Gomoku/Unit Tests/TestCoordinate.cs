using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gomoku;

namespace Unit_Tests
{
    [TestClass]
    public class TestCoordinate
    {
        [TestMethod]
        public void FlipHorizontal()
        {
            for (int row=0; row<15; row++)
            {
                Assert.AreEqual(new Coordinate(0, row).FlipHorizontal().Column, 14);
                Assert.AreEqual(new Coordinate(1, row).FlipHorizontal().Column, 13);
                Assert.AreEqual(new Coordinate(2, row).FlipHorizontal().Column, 12);
                Assert.AreEqual(new Coordinate(3, row).FlipHorizontal().Column, 11);
                Assert.AreEqual(new Coordinate(4, row).FlipHorizontal().Column, 10);
                Assert.AreEqual(new Coordinate(5, row).FlipHorizontal().Column, 9);
                Assert.AreEqual(new Coordinate(6, row).FlipHorizontal().Column, 8);
                Assert.AreEqual(new Coordinate(7, row).FlipHorizontal().Column, 7);
                Assert.AreEqual(new Coordinate(8, row).FlipHorizontal().Column, 6);
                Assert.AreEqual(new Coordinate(9, row).FlipHorizontal().Column, 5);
                Assert.AreEqual(new Coordinate(10, row).FlipHorizontal().Column, 4);
                Assert.AreEqual(new Coordinate(11, row).FlipHorizontal().Column, 3);
                Assert.AreEqual(new Coordinate(12, row).FlipHorizontal().Column, 2);
                Assert.AreEqual(new Coordinate(13, row).FlipHorizontal().Column, 1);
                Assert.AreEqual(new Coordinate(14, row).FlipHorizontal().Column, 0);
            }
        }

        [TestMethod]
        public void FlipVertical()
        {
            for (int Column = 0; Column < 15; Column++)
            {
                Assert.AreEqual(new Coordinate(Column, 0).FlipVertical().Row, 14);
                Assert.AreEqual(new Coordinate(Column, 1).FlipVertical().Row, 13);
                Assert.AreEqual(new Coordinate(Column, 2).FlipVertical().Row, 12);
                Assert.AreEqual(new Coordinate(Column, 3).FlipVertical().Row, 11);
                Assert.AreEqual(new Coordinate(Column, 4).FlipVertical().Row, 10);
                Assert.AreEqual(new Coordinate(Column, 5).FlipVertical().Row, 9);
                Assert.AreEqual(new Coordinate(Column, 6).FlipVertical().Row, 8);
                Assert.AreEqual(new Coordinate(Column, 7).FlipVertical().Row, 7);
                Assert.AreEqual(new Coordinate(Column, 8).FlipVertical().Row, 6);
                Assert.AreEqual(new Coordinate(Column, 9).FlipVertical().Row, 5);
                Assert.AreEqual(new Coordinate(Column, 10).FlipVertical().Row, 4);
                Assert.AreEqual(new Coordinate(Column, 11).FlipVertical().Row, 3);
                Assert.AreEqual(new Coordinate(Column, 12).FlipVertical().Row, 2);
                Assert.AreEqual(new Coordinate(Column, 13).FlipVertical().Row, 1);
                Assert.AreEqual(new Coordinate(Column, 14).FlipVertical().Row, 0);
            }
        }
    }
}
