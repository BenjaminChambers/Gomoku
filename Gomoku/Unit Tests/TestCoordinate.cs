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
            for (int row = 0; row < 15; row++)
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

        [TestMethod]
        public void RotateCCW()
        {
            var Indices = new int[][] {
                new int[] { 210, 195, 180, 165, 150, 135, 120, 105, 90,  75,  60,  45,  30,  15,  0},
                new int[] { 211, 196, 181, 166, 151, 136, 121, 106, 91,  76,  61,  46,  31,  16,  1 },
                new int[] { 212, 197, 182, 167, 152, 137, 122, 107, 92,  77,  62,  47,  32,  17,  2 },
                new int[] { 213, 198, 183, 168, 153, 138, 123, 108, 93,  78,  63,  48,  33,  18,  3 },
                new int[] { 214, 199, 184, 169, 154, 139, 124, 109, 94,  79,  64,  49,  34,  19,  4 },
                new int[] { 215, 200, 185, 170, 155, 140, 125, 110, 95,  80,  65,  50,  35,  20,  5 },
                new int[] { 216, 201, 186, 171, 156, 141, 126, 111, 96,  81,  66,  51,  36,  21,  6 },
                new int[] { 217, 202, 187, 172, 157, 142, 127, 112, 97,  82,  67,  52,  37,  22,  7 },
                new int[] { 218, 203, 188, 173, 158, 143, 128, 113, 98,  83,  68,  53,  38,  23,  8 },
                new int[] { 219, 204, 189, 174, 159, 144, 129, 114, 99,  84,  69,  54,  39,  24,  9 },
                new int[] { 220, 205, 190, 175, 160, 145, 130, 115, 100, 85,  70,  55,  40,  25,  10 },
                new int[] { 221, 206, 191, 176, 161, 146, 131, 116, 101, 86,  71,  56,  41,  26,  11 },
                new int[] { 222, 207, 192, 177, 162, 147, 132, 117, 102, 87,  72,  57,  42,  27,  12 },
                new int[] { 223, 208, 193, 178, 163, 148, 133, 118, 103, 88,  73,  58,  43,  28,  13 },
                new int[] { 224, 209, 194, 179, 164, 149, 134, 119, 104, 89,  74,  59,  44,  29,  14 }
            };

            for (int c = 0; c < 15; c++)
                for (int r = 0; r < 15; r++)
                    Assert.AreEqual(new Coordinate(c, r).RotateCCW().Index, Indices[r][c]);
        }

        [TestMethod]
        public void RotateCW()
        {
            var Indices = new int[][] {
                new int[] { 14,  29,  44,  59,  74,  89,  104, 119, 134, 149, 164, 179, 194, 209, 224},
                new int[] { 13,  28,  43,  58,  73,  88,  103, 118, 133, 148, 163, 178, 193, 208, 223},
                new int[] { 12,  27,  42,  57,  72,  87,  102, 117, 132, 147, 162, 177, 192, 207, 222},
                new int[] { 11,  26,  41,  56,  71,  86,  101, 116, 131, 146, 161, 176, 191, 206, 221},
                new int[] { 10,  25,  40,  55,  70,  85,  100, 115, 130, 145, 160, 175, 190, 205, 220},
                new int[] { 9,   24,  39,  54,  69,  84,  99,  114, 129, 144, 159, 174, 189, 204, 219},
                new int[] { 8,   23,  38,  53,  68,  83,  98,  113, 128, 143, 158, 173, 188, 203, 218},
                new int[] { 7,   22,  37,  52,  67,  82,  97,  112, 127, 142, 157, 172, 187, 202, 217},
                new int[] { 6,   21,  36,  51,  66,  81,  96,  111, 126, 141, 156, 171, 186, 201, 216},
                new int[] { 5,   20,  35,  50,  65,  80,  95,  110, 125, 140, 155, 170, 185, 200, 215},
                new int[] { 4,   19,  34,  49,  64,  79,  94,  109, 124, 139, 154, 169, 184, 199, 214},
                new int[] { 3,   18,  33,  48,  63,  78,  93,  108, 123, 138, 153, 168, 183, 198, 213},
                new int[] { 2,   17,  32,  47,  62,  77,  92,  107, 122, 137, 152, 167, 182, 197, 212},
                new int[] { 1,   16,  31,  46,  61,  76,  91,  106, 121, 136, 151, 166, 181, 196, 211},
                new int[] { 0,   15,  30,  45,  60,  75,  90,  105, 120, 135, 150, 165, 180, 195, 210 }

            };

            for (int c = 0; c < 15; c++)
                for (int r = 0; r < 15; r++)
                    Assert.AreEqual(new Coordinate(c, r).RotateCW().Index, Indices[r][c]);
        }
    }
}
