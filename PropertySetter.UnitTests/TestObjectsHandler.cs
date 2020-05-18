using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace PropertySetter.UnitTests
{
    public static class TestObjectsHandler
    {
        internal static Beam GetBeam()
        {
            Beam beam = new Beam(new Point(5000, 7000, 0), new Point(6000, 7000, 0));
            beam.Profile.ProfileString = "250*250";
            beam.Material.MaterialString = "K40-1";
            beam.Finish = "PAINT";
            beam.Insert();
            return beam;
        }

        internal static SingleRebar GetSingleRebar()
        {
            var beam = GetBeam();
            var solid = beam.GetSolid();

            Polygon Polygon = new Polygon();
            Polygon.Points.Add(new Point(solid.MinimumPoint.X, solid.MaximumPoint.Y, solid.MaximumPoint.Z));
            Polygon.Points.Add(new Point(solid.MaximumPoint.X, solid.MaximumPoint.Y, solid.MaximumPoint.Z));

            SingleRebar singleRebar = new SingleRebar
            {
                Polygon = Polygon,
                Father = beam,
                Name = "SingleRebar",
                Class = 9,
                Size = "12",
                Grade = "A500HW",
                OnPlaneOffsets = new ArrayList { 25.00 },
                NumberingSeries = new NumberingSeries("Single", 0),
                StartHook = new RebarHookData()
                {
                    Angle = -90,
                    Length = 10,
                    Radius = 10,
                    Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK
                },
                EndHook = new RebarHookData()
                {
                    Angle = 90,
                    Length = 10,
                    Radius = 10,
                    Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK
                }
            };

            singleRebar.Insert();
            return singleRebar;
        }

        internal static ModelObject GetRebarGroup()
        {
            var beam = GetBeam();
            var solid = beam.GetSolid();
            
            Polygon polygon1 = new Polygon();
            polygon1.Points.Add(new Point(solid.MinimumPoint.X, solid.MaximumPoint.Y, solid.MinimumPoint.Z));
            polygon1.Points.Add(new Point(solid.MinimumPoint.X, solid.MinimumPoint.Y, solid.MinimumPoint.Z));
            polygon1.Points.Add(new Point(solid.MinimumPoint.X, solid.MinimumPoint.Y, solid.MaximumPoint.Z));
            polygon1.Points.Add(new Point(solid.MinimumPoint.X, solid.MaximumPoint.Y, solid.MaximumPoint.Z));

            Polygon polygon2 = new Polygon();
            polygon2.Points.Add(new Point(solid.MaximumPoint.X, solid.MaximumPoint.Y, solid.MinimumPoint.Z));
            polygon2.Points.Add(new Point(solid.MaximumPoint.X, solid.MinimumPoint.Y, solid.MinimumPoint.Z));
            polygon2.Points.Add(new Point(solid.MaximumPoint.X, solid.MinimumPoint.Y, solid.MaximumPoint.Z));
            polygon2.Points.Add(new Point(solid.MaximumPoint.X, solid.MaximumPoint.Y, solid.MaximumPoint.Z));

            RebarGroup rg = new RebarGroup
            {
                Size = "12",
                Class = 3,
                Grade = "A500HW",
                Name = "RebarGroup",
                Father = beam,
                SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE,
                ExcludeType = BaseRebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH
            };
            rg.Polygons.Add(polygon1);
            rg.Polygons.Add(polygon2);
            rg.RadiusValues.Add(40.0);
            rg.Spacings.Add(30.0);
            rg.NumberingSeries.StartNumber = 0;
            rg.NumberingSeries.Prefix = "Group";
            rg.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            rg.StartHook.Angle = -90;
            rg.StartHook.Length = 3;
            rg.StartHook.Radius = 20;
            rg.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            rg.EndHook.Angle = -90;
            rg.EndHook.Length = 3;
            rg.EndHook.Radius = 20;
            rg.OnPlaneOffsets.Add(25.0);
            rg.OnPlaneOffsets.Add(10.0);
            rg.OnPlaneOffsets.Add(25.0);
            rg.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rg.StartPointOffsetValue = 20;
            rg.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rg.EndPointOffsetValue = 60;
            rg.FromPlaneOffset = 40;

            rg.Insert();
            return rg;
        }

        internal static ModelObject GetBooleanPart()
        {
            var beam = GetBeam();
            var cuttingPart = new Beam()
            {
                StartPoint = new Point(5500, 6000, 0),
                EndPoint = new Point(5500, 8000, 0),
                Class = BooleanPart.BooleanOperativeClassName,
            };
            cuttingPart.Profile.ProfileString = "100*100";
            cuttingPart.Insert();

            var cut = new BooleanPart();
            cut.Father = beam;
            cut.SetOperativePart(cuttingPart);
            cut.Insert();
            cuttingPart.Delete();
            return cut;
        }

        internal static Assembly GetAssembly()
        {
            return GetBeam().GetAssembly();
        }
    }
}
