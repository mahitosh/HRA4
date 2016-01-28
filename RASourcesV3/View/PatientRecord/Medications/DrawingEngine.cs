using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    /// <summary>
    /// A drawing step is a computation which draws one aspect of the visualization
    /// based on the state of the model.
    /// </summary>
    /// <param name="g">The graphics to draw on</param>
    public delegate void DrawingStep(Graphics g);

    /// <summary>
    /// A drawing engine is a sequence of drawing steps acting on a model.
    /// </summary>
    public class DrawingEngine
    {
        private List<DrawingStep> steps = new List<DrawingStep>();
        public void Draw(Graphics g)
        {
            foreach (DrawingStep step in steps)
                step(g);
        }
        public void AddDrawingStep(DrawingStep drawingStep)
        {
            steps.Add(drawingStep);
        }
    }
}
