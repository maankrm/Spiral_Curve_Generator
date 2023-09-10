using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace SpiralCurveG
{
    public class SpiralCurveGComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public SpiralCurveGComponent()
          : base("SpiralCurveG", "SPI",
            "Generate a Spiral Curve With 2 Radius, Height and Turns Factors ",
            "Curve", "Spline")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPlaneParameter("Plane", "Pl", "Main plane",GH_ParamAccess.item, Plane.Unset);
            pManager.AddNumberParameter("Radius1", "R1", "Radius one to change size of the shape",GH_ParamAccess.item,2.00);
            pManager.AddNumberParameter("Radius2", "R2", "Radius two to change size of the shape",GH_ParamAccess.item,2.00);
            pManager.AddIntegerParameter("Turns", "T", "Turns of shape", GH_ParamAccess.item,3);
            pManager.AddNumberParameter("Height", "H", "Height of shape", GH_ParamAccess.item,3.00);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("Curve", "C", "polyline curve", GH_ParamAccess.item);
            //pManager.AddPointParameter("Points", "P", "List of Points",GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // vars
            Plane plane = Plane.Unset;
            double R1 = 0;
            double R2 = 0;
            int turns = 0;
            double height = 0;

            // inputs
            if (!DA.GetData(0, ref plane)) return;
            if (!DA.GetData(1, ref R1)) return;
            if (!DA.GetData(2, ref R2)) return;
            if (!DA.GetData(3, ref turns)) return;
            if (!DA.GetData(4, ref height)) return;



            Polyline poly = new Polyline();
            
            poly.AddRange(spiralP(plane, R1, R2, turns, height));
            //List<Point3d> ListOfPoints = spiralP(plane, R1, R2, turns, height);

            // outputs
            DA.SetData(0, poly);
            //DA.SetDataList(1, ListOfPoints);

        }


        // function that to create spiral points
        public List<Point3d> spiralP(Plane plane, double Radius1, double Radius2, int Turns, double Height)
        {
            
            double angle = 0.0;
            double increment = 4.0 * Math.PI / 720;
            double dr = (Radius2 - Radius1) / 720;

            //vectors
            Vector3d XVector = new Vector3d(1, 0, 0);
            Vector3d YVector = new Vector3d(0, 1, 0);
            Vector3d ZVector = new Vector3d(0, 0, 1);

            // Points List Storage
            List<Point3d> points = new List<Point3d>();
            double x;
            double y;
            double z;

            for (int i = 0; i < Turns * 360; i++)
            {
                if (plane.XAxis == YVector && plane.YAxis == ZVector)
                {
                    y = Radius1 * Math.Cos(angle);
                    z = Radius1 * Math.Sin(angle);
                    x = i * Height / 360;
                    Point3d point = new Point3d(x, y, z);
                    points.Add(point);

                }
                else if (plane.XAxis == XVector && plane.YAxis == ZVector)
                {
                    x = Radius1 * Math.Cos(angle);
                    z = Radius1 * Math.Sin(angle);
                    y = i * Height / 360;
                    Point3d point = new Point3d(x, y, z);
                    points.Add(point);
                }

                else
                {
                    x = Radius1 * Math.Cos(angle);
                    y = Radius1 * Math.Sin(angle);
                    z = i * Height / 360;
                    Point3d point = new Point3d(x, y, z);
                    points.Add(point);
                    
                }

                angle += increment;
                Radius1 += dr;
            }
            return points;

        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => Properties.Resources.spi3;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("F337BE93-B2FA-4876-AB1C-E5EA40FA58FA");
    }
}