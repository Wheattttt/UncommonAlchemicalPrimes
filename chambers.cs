using Quintessential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PipelineChamber = class_183;
using Texture = class_256;
using TextureOffsets = class_107;

namespace UncommonPrimes;
public static class Chambers
{
    public static PipelineChamber 
        LargeDonut, MediumDiamond, LargeDiamond, SmallTriangle, SmallTriangleFlipped, MediumTriangle, MediumTriangleFlipped, SmallWiderRotated, SmallWiderRotatedFlipped, MediumLine, LargeLine;

    public static Texture largeDonut_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_donut_border");
    public static Texture largeDonut_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_donut_interior");
    public static Texture largeDonut_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_donut_mask");

    public static Texture mediumDiamond_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/medium_diamond_border");
    public static Texture mediumDiamond_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/medium_diamond_interior");
    public static Texture mediumDiamond_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/medium_diamond_mask");

    public static Texture largeDiamond_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_diamond_border");
    public static Texture largeDiamond_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_diamond_interior");
    public static Texture largeDiamond_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_diamond_mask");

    public static Texture smallTriangle_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_triangle_border");
    public static Texture smallTriangle_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_triangle_interior");
    public static Texture smallTriangle_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_triangle_mask");
    public static Texture smallTriangleFlipped_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_triangle_flipped_border");
    public static Texture smallTriangleFlipped_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_triangle_flipped_interior");
    public static Texture smallTriangleFlipped_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_triangle_flipped_mask");

    public static Texture smallWiderRotated_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_wider_hex_rotated_border");
    public static Texture smallWiderRotated_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_wider_hex_rotated_interior");
    public static Texture smallWiderRotated_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_wider_hex_rotated_mask");
    public static Texture smallWiderRotatedFlipped_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_wider_hex_rotated_flipped_border");
    public static Texture smallWiderRotatedFlipped_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_wider_hex_rotated_flipped_interior");
    public static Texture smallWiderRotatedFlipped_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/small_wider_hex_rotated_flipped_mask");

    public static Texture mediumLine_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/medium_line_border");
    public static Texture mediumLine_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/medium_line_interior");
    public static Texture mediumLine_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/medium_line_mask");

    public static Texture largeLine_Border = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_line_border");
    public static Texture largeLine_Interior = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_line_interior");
    public static Texture largeLine_Mask = Brimstone.API.GetTexture("textures/chambers/UncommonPrimes/large_line_mask");
    public static void AddChambers()
    {
        LargeDonut = new PipelineChamber
        {
            field_1727 = "LargeDonut",
            field_1728 = new HashSet<HexIndex>(
                HexIndex.EnumerateSpiral(new HexIndex(0, 0), 3).Skip(1)),
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(0, 0),
                new HexIndex(4, 0),
                new HexIndex(3, 1),
                new HexIndex(2, 2),
                new HexIndex(1, 3),
                new HexIndex(0, 4),
                new HexIndex(-1, 4),
                new HexIndex(-2, 4),
                new HexIndex(-3, 4),
                new HexIndex(-4, 4),
                new HexIndex(-4, 3),
                new HexIndex(-4, 2),
                new HexIndex(-4, 1),
                new HexIndex(-4, 0),
                new HexIndex(-3, -1),
                new HexIndex(-2, -2),
                new HexIndex(-1, -3),
                new HexIndex(0, -4),
                new HexIndex(1, -4),
                new HexIndex(2, -4),
                new HexIndex(3, -4),
                new HexIndex(4, -4),
                new HexIndex(4, -3),
                new HexIndex(4, -2),
                new HexIndex(4, -1)
            },
            field_1730 = largeDonut_Border,
            field_1731 = largeDonut_Interior,
            field_1732 = largeDonut_Mask
        };
        QApi.AddChamberType(LargeDonut, true);


        MediumDiamond = new PipelineChamber
        {
            field_1727 = "MediumDiamond",
            field_1728 = new HashSet<HexIndex>
            {
                new HexIndex(-1, 2),
                new HexIndex(-1, 1),
                new HexIndex(-1, 0),
                new HexIndex(0, 1),
                new HexIndex(0, 0),
                new HexIndex(0, -1),
                new HexIndex(1, 0),
                new HexIndex(1, -1),
                new HexIndex(1, -2),
            },
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-1, 3),
                new HexIndex(0, 2),
                new HexIndex(1, 1),
                new HexIndex(2, 0),
                new HexIndex(2, -1),
                new HexIndex(2, -2),
                new HexIndex(2, -3),
                new HexIndex(1, -3),
                new HexIndex(0, -2),
                new HexIndex(-1, -1),
                new HexIndex(-2, 0),
                new HexIndex(-2, 1),
                new HexIndex(-2, 2),
                new HexIndex(-2, 3),
            },
            field_1730 = mediumDiamond_Border,
            field_1731 = mediumDiamond_Interior,
            field_1732 = mediumDiamond_Mask
        };
        QApi.AddChamberType(MediumDiamond, true);


        LargeDiamond = new PipelineChamber
        {
            field_1727 = "LargeDiamond",
            field_1728 = new HashSet<HexIndex>
            {
                new HexIndex(-2, 3),
                new HexIndex(-2, 2),
                new HexIndex(-1, 2),
                new HexIndex(-2, 1),
                new HexIndex(-1, 1),
                new HexIndex(0, 1),
                new HexIndex(-2, 0),
                new HexIndex(-1, 0),
                new HexIndex(0, 0),
                new HexIndex(1, 0),
                new HexIndex(-1, -1),
                new HexIndex(0, -1),
                new HexIndex(1, -1),
                new HexIndex(0, -2),
                new HexIndex(1, -2),
                new HexIndex(1, -3),
            },
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-2, 4),
                new HexIndex(-1, 3),
                new HexIndex(0, 2),
                new HexIndex(1, 1),
                new HexIndex(2, 0),
                new HexIndex(2, -1),
                new HexIndex(2, -2),
                new HexIndex(2, -3),
                new HexIndex(2, -4),
                new HexIndex(1, -4),
                new HexIndex(0, -3),
                new HexIndex(-1, -2),
                new HexIndex(-2, -1),
                new HexIndex(-3, 0),
                new HexIndex(-3, 1),
                new HexIndex(-3, 2),
                new HexIndex(-3, 3),
                new HexIndex(-3, 4),
            },
            field_1730 = largeDiamond_Border,
            field_1731 = largeDiamond_Interior,
            field_1732 = largeDiamond_Mask
        };
        QApi.AddChamberType(LargeDiamond, true);


        SmallTriangle = new PipelineChamber
        {
            field_1727 = "SmallTriangle",
            field_1728 = new HashSet<HexIndex>
            {
                new HexIndex(-1, 0),
                new HexIndex(0, 0),
                new HexIndex(0, -1),
                new HexIndex(1, 0),
                new HexIndex(1, -1),
                new HexIndex(1, -2),


            },
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-2, 1),
                new HexIndex(-1, 1),
                new HexIndex(0, 1),
                new HexIndex(1, 1),
                new HexIndex(2, 0),
                new HexIndex(2, -1),
                new HexIndex(2, -2),
                new HexIndex(2, -3),
                new HexIndex(1, -3),
                new HexIndex(0, -2),
                new HexIndex(-1, -1),
                new HexIndex(-2, 0),

            },
            field_1730 = smallTriangle_Border,
            field_1731 = smallTriangle_Interior,
            field_1732 = smallTriangle_Mask
        };
        QApi.AddChamberType(SmallTriangle, true);


        SmallTriangleFlipped = new PipelineChamber
        {
            field_1727 = "SmallTriangleFlipped",
            field_1728 = new HashSet<HexIndex>
            {
                new HexIndex(-1, 2),
                new HexIndex(-1, 1),
                new HexIndex(-1, 0),
                new HexIndex(0, 1),
                new HexIndex(0, 0),
                new HexIndex(1, 0),

            },
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-1, 3),
                new HexIndex(0, 2),
                new HexIndex(1, 1),
                new HexIndex(2, 0),
                new HexIndex(2, -1),
                new HexIndex(1, -1),
                new HexIndex(0, -1),
                new HexIndex(-1, -1),
                new HexIndex(-2, 0),
                new HexIndex(-2, 1),
                new HexIndex(-2, 2),
                new HexIndex(-2, 3),
            },
            field_1730 = smallTriangleFlipped_Border,
            field_1731 = smallTriangleFlipped_Interior,
            field_1732 = smallTriangleFlipped_Mask
        };
        QApi.AddChamberType(SmallTriangleFlipped, true);

        SmallWiderRotated = new PipelineChamber
        {
            field_1727 = "SmallWiderRotated",
            field_1728 = new HashSet<HexIndex>(HexIndex.EnumerateSpiral(new HexIndex(0, -1), 1).Concat(HexIndex.EnumerateSpiral(new HexIndex(0, 1), 1))),
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-2, -1),
                new HexIndex(-2, 0),
                new HexIndex(-2, 1),
                new HexIndex(-2, 2),
                new HexIndex(-2, 3),
                new HexIndex(-1, 3),
                new HexIndex(0, 3),
                new HexIndex(1, 2),
                new HexIndex(2, 1),
                new HexIndex(2, 0),
                new HexIndex(2, -1),
                new HexIndex(2, -2),
                new HexIndex(2, -3),
                new HexIndex(1, -3),
                new HexIndex(0, -3),
                new HexIndex(-1, -2),
            },
            field_1730 = smallWiderRotated_Border,
            field_1731 = smallWiderRotated_Interior,
            field_1732 = smallWiderRotated_Mask
        };
        QApi.AddChamberType(SmallWiderRotated, true);

        SmallWiderRotatedFlipped = new PipelineChamber
        {
            field_1727 = "SmallWiderRotatedFlipped",
            field_1728 = new HashSet<HexIndex>(HexIndex.EnumerateSpiral(new HexIndex(-1, 1), 1).Concat(HexIndex.EnumerateSpiral(new HexIndex(1, -1), 1))),
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-1, 3),
                new HexIndex(0, 2),
                new HexIndex(1, 1),
                new HexIndex(2, 0),
                new HexIndex(3, -1),
                new HexIndex(3, -2),
                new HexIndex(3, -3),
                new HexIndex(2, -3),
                new HexIndex(1, -3),
                new HexIndex(0, -2),
                new HexIndex(-1, -1),
                new HexIndex(-2, 0),
                new HexIndex(-3, 1),
                new HexIndex(-3, 2),
                new HexIndex(-3, 3),
                new HexIndex(-2, 3),
            },
            field_1730 = smallWiderRotatedFlipped_Border,
            field_1731 = smallWiderRotatedFlipped_Interior,
            field_1732 = smallWiderRotatedFlipped_Mask
        };
        QApi.AddChamberType(SmallWiderRotatedFlipped, true);

        MediumLine = new PipelineChamber
        {
            field_1727 = "MediumLine",
            field_1728 = new HashSet<HexIndex>
            {
                new HexIndex(-2, 0),
                new HexIndex(-1, 0),
                new HexIndex(0, 0),
                new HexIndex(1, 0),
                new HexIndex(2, 0),

            },
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-3, 0),
                new HexIndex(-3, 1),
                new HexIndex(-2, 1),
                new HexIndex(-1, 1),
                new HexIndex(0, 1),
                new HexIndex(1, 1),
                new HexIndex(2, 1),
                new HexIndex(3, 0),
                new HexIndex(3, -1),
                new HexIndex(2, -1),
                new HexIndex(1, -1),
                new HexIndex(0, -1),
                new HexIndex(-1, -1),
                new HexIndex(-2, -1),
            },
            field_1730 = mediumLine_Border,
            field_1731 = mediumLine_Interior,
            field_1732 = mediumLine_Mask
        };
        QApi.AddChamberType(MediumLine, true);


        LargeLine = new PipelineChamber
        {
            field_1727 = "LargeLine",
            field_1728 = new HashSet<HexIndex>
            {
                new HexIndex(-3, 0),
                new HexIndex(-2, 0),
                new HexIndex(-1, 0),
                new HexIndex(0, 0),
                new HexIndex(1, 0),
                new HexIndex(2, 0),
                new HexIndex(3, 0),

            },
            field_1729 = new HashSet<HexIndex>
            {
                new HexIndex(-4, 0),
                new HexIndex(-4, 1),
                new HexIndex(-3, 1),
                new HexIndex(-2, 1),
                new HexIndex(-1, 1),
                new HexIndex(0, 1),
                new HexIndex(1, 1),
                new HexIndex(2, 1),
                new HexIndex(3, 1),
                new HexIndex(4, 0),
                new HexIndex(4, -1),
                new HexIndex(3, -1),
                new HexIndex(2, -1),
                new HexIndex(1, -1),
                new HexIndex(0, -1),
                new HexIndex(-1, -1),
                new HexIndex(-2, -1),
                new HexIndex(-3, -1),
            },
            field_1730 = largeLine_Border,
            field_1731 = largeLine_Interior,
            field_1732 = largeLine_Mask
        };
        QApi.AddChamberType(LargeLine, true);
    }
}