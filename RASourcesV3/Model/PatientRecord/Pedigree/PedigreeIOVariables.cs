using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    /// <summary>
    /// Dynamic variables relating to user interaction which are part of the pedigree model.
    /// </summary>
    public class PedigreeIOVariables
    {
        /// <summary>
        /// True when the mouse is down. (set by mouse listener in PedigreeControl)
        /// </summary>
        public bool mouseDown = false;

        /// <summary>
        /// 
        /// </summary>
        public MouseButtons button = MouseButtons.None;

        /// <summary>
        /// True when the mouse was down last frame. (set by the IncrementIOVariables layout step)
        /// </summary>
        public bool pMouseDown = false;

        /// <summary>
        /// True when the mouse has been pressed down this frame. (set by the IncrementIOVariables layout step)
        /// </summary>
        public bool mousePressed = false;

        /// <summary>
        /// True when the mouse has been released this frame. (set by the IncrementIOVariables layout step)
        /// </summary>
        public bool mouseReleased = false;
        
        /// <summary>
        /// The x coordinate of the mouse point. (set by mouse listener in PedigreeControl)
        /// </summary>
        public double mouseX = 0;

        /// <summary>
        /// The x coordinate of the mouse point last frame. (set by the IncrementIOVariables layout step)
        /// </summary>
        public double pMouseX = 0;

        /// <summary>
        /// The y coordinate of the mouse point. (set by mouse listener in PedigreeControl)
        /// </summary>
        public double mouseY = 0;

        /// <summary>
        /// The y coordinate of the mouse point last frame. (set by the IncrementIOVariables layout step)
        /// </summary>
        public double pMouseY = 0;


    }
}
