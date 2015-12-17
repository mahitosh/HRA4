using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using RiskApps3.Model.PatientRecord.Pedigree;
using System.Drawing.Drawing2D;
using RiskApps3.View.PatientRecord.Pedigree.drawingSteps;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    /// <summary>
    /// The renderer of the pedigree. This serves as the view part of our 
    /// model view controller architecture. It is a drawing engine, which 
    /// is a sequence of drawing steps which get executed every frame.
    /// </summary>
    class PedigreeView : DrawingEngine
    {
        public PedigreeView(PedigreeModel model)
        {
               
            AddDrawingStep(new ClearBackground(model).step);         
   

            AddDrawingStep(new DrawLasso(model).step);
            
            AddDrawingStep(new DrawZoom(model).step);
            

            
            AddDrawingStep(new DrawAncestralConnectionLines(model).step);
            AddDrawingStep(new DrawIndividuals(model).step);
            AddDrawingStep(new DrawCouplesGraphNodes(model).step);
            AddDrawingStep(new DrawCouplesGraphEdges(model).step);
            AddDrawingStep(new DrawLayoutSteps(model).step);
            
            
           //AddDrawingStep(new DrawLoading(model).step);
        }
    }
}
