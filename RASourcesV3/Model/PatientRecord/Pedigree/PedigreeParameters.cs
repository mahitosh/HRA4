using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using RiskApps3.View.PatientRecord.Pedigree;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    /// <summary>
    /// The class encapsulates all user-definable pedigree layout and drawing parameters.
    /// </summary>
    public class PedigreeParameters
    {

        /// <summary>
        /// The distance between generational levels.
        /// </summary>
        public int verticalSpacing = 100;

        /// <summary>
        /// The distance between adjacent individuals.
        /// </summary>
        public int horizontalSpacing = 70;

        /// <summary>
        /// The radius of the circles representing nodes of the couples graph.
        /// </summary>
        public int coupleNodeRadius = 10;

        /// <summary>
        /// The size (bounding box side length) of individuals (circles or squares).
        /// </summary>
        public int individualSize = 30;

        /// <summary>
        /// A flag indicating whether or not to draw the couples graph.
        /// </summary>
        public bool drawCouplesGraph = false;

        /// <summary>
        /// A flag indicating whether or not to draw the visual debugging overlay.
        /// </summary>
        public bool drawVisualDebugging = false;

        /// <summary>
        /// A flag indicating whether or not to draw the id labels for individuals.
        /// </summary>
        public bool drawIdLabels = false;
        //public bool ShowName = false;
        //public bool ShowUnitnum = false;
        //public bool ShowDob = false;
        //public bool ShowSymbol = false;

        /// <summary>
        /// The value by which the velocities of all points is multiplied every frame
        /// to roughly simulate a viscous fluid.
        /// </summary>
        public double dampeningFactor = 0.1;

        /// <summary>
        /// The value determining the strength of the constant pull toward the display center
        /// which is exerted on all individuals and couples.
        /// </summary>
        public double centeringForceStrength = 0.05;

        /// <summary>
        /// The value determining the strength of the vertical pull on all
        /// couples and individuals toward their computed ideal positions
        /// </summary>
        public double verticalPositioningForceStrength = 0.1;

        /// <summary>
        /// The strength of the forces which pull individuals in couples 
        /// toward their optimal positions.
        /// </summary>
        public double layoutCouplesStrength = 0.075;

        /// <summary>
        /// The strength of the forces which pull individuals in sibships 
        /// toward their optimal positions.
        /// </summary>
        public double layoutSibshipsStrength = 0.5;


        /// <summary>
        /// The height of the arc drawn between adjacent couples in 
        /// half siblings to represent their edge in the couples graph
        /// </summary>
        public int halfSibsArcHeight = 15;

        /// <summary>
        /// A force factors used in the RepelIndividualSets layout step.
        /// </summary>
        public double repelIndividualSetsStrength = .2;

        public int PositionHistoryDepth = 50;

        public double sibshipShrinkingFacor = 0.575;
        /// <summary>
        /// The strength of the force attracting couples connected by an edge.
        /// </summary>
        //public double couplesAttractionStrength = 0.05;


        public float scale = 1.0F;
        public int vOffset = 0;
        public int hOffset = 0;

        public double gridWidth = 20;
        public double gridHeight = 20;

        public bool saveAndClose = false;
        public bool allRelativesAccountedFor = false;

        public int annotationFontSize = 10;

        public Point HoldingPenOrigin = new Point(20, 20);

        public double avgVelocityThreshold = 0.2;

        public string ControllerMode = "";

        public List<AnnotationContainer> annotation_areas;

        
        public bool multiSelect = true;

        public Brush BackgroundBrush = Brushes.White;

        public int nameWidth = 8;

        public bool limitedEthnicity = false;
        public bool limitedNationality = true;

        public string VariantFoundText = "+";
        public string VariantFoundVusText = "vus";
        public string VariantNotFoundText = "-";
        public string VariantUnknownText = "?";
        public string VariantNotTestedText = "NT";
        public string VariantHeteroText = "+/-";
        
        public bool hideNonBloodRelatives = false;

        internal void Set(PedigreeParameters pedigreeParameters)
        {
            verticalSpacing = pedigreeParameters.verticalSpacing;
            horizontalSpacing = pedigreeParameters.horizontalSpacing;
            coupleNodeRadius = pedigreeParameters.coupleNodeRadius;
            individualSize = pedigreeParameters.individualSize;
            drawCouplesGraph = pedigreeParameters.drawCouplesGraph;
            drawVisualDebugging = pedigreeParameters.drawVisualDebugging;
            drawIdLabels = pedigreeParameters.drawIdLabels;

            //ShowName = pedigreeParameters.ShowName;
            //ShowUnitnum = pedigreeParameters.ShowUnitnum;
            //ShowDob = pedigreeParameters.ShowDob;
            //ShowSymbol = pedigreeParameters.ShowSymbol;

            dampeningFactor = pedigreeParameters.dampeningFactor;
            centeringForceStrength = pedigreeParameters.centeringForceStrength;
            verticalPositioningForceStrength = pedigreeParameters.verticalPositioningForceStrength;
            layoutCouplesStrength = pedigreeParameters.layoutCouplesStrength;
            layoutSibshipsStrength = pedigreeParameters.layoutSibshipsStrength;
            halfSibsArcHeight = pedigreeParameters.halfSibsArcHeight;
            repelIndividualSetsStrength = pedigreeParameters.repelIndividualSetsStrength;
            //couplesAttractionStrength = pedigreeParameters.couplesAttractionStrength;
            sibshipShrinkingFacor = pedigreeParameters.sibshipShrinkingFacor;
            PositionHistoryDepth = pedigreeParameters.PositionHistoryDepth;
            avgVelocityThreshold = pedigreeParameters.avgVelocityThreshold;
            ControllerMode = pedigreeParameters.ControllerMode;

            scale = pedigreeParameters.scale;
            vOffset = pedigreeParameters.vOffset;
            hOffset = pedigreeParameters.hOffset;

            gridWidth = pedigreeParameters.gridWidth;
            gridHeight = pedigreeParameters.gridHeight;

            saveAndClose = pedigreeParameters.saveAndClose;
            allRelativesAccountedFor = pedigreeParameters.allRelativesAccountedFor;

            annotationFontSize = pedigreeParameters.annotationFontSize;

            HoldingPenOrigin.X = pedigreeParameters.HoldingPenOrigin.X;
            HoldingPenOrigin.Y = pedigreeParameters.HoldingPenOrigin.Y;
            //annotation_areas = pedigreeParameters.annotation_areas;

            hideNonBloodRelatives = pedigreeParameters.hideNonBloodRelatives;

            multiSelect = pedigreeParameters.multiSelect;

            annotation_areas = pedigreeParameters.annotation_areas;

            BackgroundBrush = pedigreeParameters.BackgroundBrush;

            nameWidth = pedigreeParameters.nameWidth;

            limitedEthnicity = pedigreeParameters.limitedEthnicity;

            this.VariantFoundText = pedigreeParameters.VariantFoundText;
            this.VariantFoundVusText = pedigreeParameters.VariantFoundVusText;
            this.VariantNotFoundText = pedigreeParameters.VariantNotFoundText;
            this.VariantUnknownText = pedigreeParameters.VariantUnknownText;
            this.VariantNotTestedText = pedigreeParameters.VariantNotTestedText;
            this.VariantHeteroText = pedigreeParameters.VariantHeteroText;

            this.hideNonBloodRelatives = pedigreeParameters.hideNonBloodRelatives;
        }
    }
}
