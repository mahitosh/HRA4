using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Controllers.Pedigree.layoutSteps;
using RiskApps3.Controllers.Pedigree.layoutSteps.interaction;
using RiskApps3.View.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree
{
    /// <summary>
    /// This class is the controller of our model view controller architecture.
    /// This class mutates the model over time (dynamic layout) and in response
    /// to user actions (interactions).
    /// 
    /// The layout steps of this layout engine are aimed to solve the following 
    /// constraints:
    /// 1. The couples graph must be planar
    ///    Addressed by LevelRestrictedForceDirectedLayout
    /// 2. Children must be a fixed distance below their parents
    /// 3. Ancestral connection lines must never cross
    /// 4. Adjacent individuals must be no closer than a fixed distance
    /// 5. The pedigree must be as horizontally compact as possible
    /// 6. The pedigree must be centered horizontally on the display
    ///    Addressed by PullTowardCenterHorizontally
    /// 7. The pedigree must be centered vertically on the display
    /// 8. Sibship spans and their parents must be centered when possible
    /// 9. The father should be to the left of the mother when possible
    /// </summary>
    public class PedigreeController : LayoutEngine
    {
        public PedigreeControl.ModelConvergedCallbackType ModelConvergedCallback;


        //These strings are mode identifiers used as keys in a Dictionary of modes
        public const String SELF_ORGANIZING = "SELF_ORGANIZING";
        public const String STATIC_LAYOUT = "STATIC_LAYOUT";
        public const String MANUAL = "MANUAL";
        public const String SAVE_AND_CLOSE = "SAVE_AND_CLOSE";
        public const String SNAP = "SNAP";

        public PedigreeController(PedigreeModel model, string mode)
        {
            AddLayoutStep(SELF_ORGANIZING, new PlaceIndividualsOverCouples(model).step);
            AddLayoutStep(SELF_ORGANIZING, new PullTowardIdealVerticalPositions(model).step);
            AddLayoutStep(SELF_ORGANIZING, new DetectForceConvergence(model, this).step);
            AddLayoutStep(SELF_ORGANIZING, new IncrementControllerVariables(model, this).step);
            AddLayoutStep(SELF_ORGANIZING, new IncrementVelocities(model).step);
            AddLayoutStep(SELF_ORGANIZING, new RepelIndividuals(model).step);
            AddLayoutStep(SELF_ORGANIZING, new AttractCouples(model).step);

            AddLayoutStep(MANUAL, new IncrementControllerVariables(model, this).step);

            AddLayoutStep(SAVE_AND_CLOSE, new AutoSave(model, this).step);


            if (string.IsNullOrEmpty(mode))
            {
                SetMode(MANUAL);
            }
            else
            {
                SetMode(mode);
            }
        }


        internal void Shutdown()
        {
            
        }
    }
}


////////////////////////////////////
//AddLayoutStep(SELF_ORGANIZING, new LayoutHoldingPen(model).step);
//AddLayoutStep(SELF_ORGANIZING, new DragPoints(model, this).step);
//AddLayoutStep(SELF_ORGANIZING, new Pan(model).step);
//AddLayoutStep(SELF_ORGANIZING, new IncrementIOVariables(model).step);



//AddLayoutStep(MANUAL, new DragPoints(model,this).step);
//AddLayoutStep(MANUAL, new Pan(model).step);
//AddLayoutStep(MANUAL, new IncrementIOVariables(model).step);



//AddLayoutStep(SNAP, new DragPoints(model,this).step);
//AddLayoutStep(SNAP, new Pan(model).step);
//AddLayoutStep(SNAP, new IncrementIOVariables(model).step);
//AddLayoutStep(SNAP, new IncrementControllerVariables(model, this).step);
//AddLayoutStep(SNAP, new PullTowardIdealVerticalPositions(model).step);
//AddLayoutStep(SNAP, new SnapToGrid(model, this).step);





//AddLayoutStep(SELF_ORGANIZING, new SwapSibs(model).step);
//AddLayoutStep(SELF_ORGANIZING, new DetectValidLayout(model).step);
//AddLayoutStep(SELF_ORGANIZING, new ComputeCenters(model).step);
//AddLayoutStep(SELF_ORGANIZING, new LayoutCouples(model).step);
//AddLayoutStep(SELF_ORGANIZING, new LayoutSibships(model).step);
//AddLayoutStep(SELF_ORGANIZING, new RepelIndividualSets(model).step);
//AddLayoutStep(SELF_ORGANIZING, new EllapsedTimeCheck(model).step);

//AddLayoutStep(SELF_ORGANIZING, new DetectPlanarity(model).step);
//AddLayoutStep(SELF_ORGANIZING, new LevelRestrictedForceDirectedLayout(model).step);
//AddLayoutStep(SELF_ORGANIZING, new PullTowardCenterHorizontally(model).step);

//StaticLayout staticLayout = new StaticLayout(model, this);
            //AddLayoutStep(SELF_ORGANIZING, staticLayout.transitionToStaticLayout);

            //AddLayoutStep(STATIC_LAYOUT, staticLayout.step);
            //AddLayoutStep(STATIC_LAYOUT, new DragPoints(model).step);
            //AddLayoutStep(STATIC_LAYOUT, new Pan(model).step);
            //AddLayoutStep(STATIC_LAYOUT, new IncrementIOVariables(model).step);

            //AddLayoutStep(MANUAL, new SnapToGrid(model,this).step);

            //AddLayoutStep(MANUAL, new CenterParents(model).step);
            //AddLayoutStep(MANUAL, new PullToTargetPositions(model).step);
            //AddLayoutStep(MANUAL, new PullTowardCenterHorizontally(model).step);
            //AddLayoutStep(MANUAL, new LayoutSibships(model).step);
            //AddLayoutStep(MANUAL, new IncrementVelocities(model).step);



