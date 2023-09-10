using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace SpiralCurveG
{
    public class SpiralCurveGInfo : GH_AssemblyInfo
    {
        public override string Name => "SpiralCurveG";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "This Component is an other algorithm to create a spiral curve";

        public override Guid Id => new Guid("44B7CED4-4721-4BCC-BCF1-45337FAB06A4");

        //Return a string identifying you or your company.
        public override string AuthorName => "Parastorm lab - Maan Abdulkareem";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}