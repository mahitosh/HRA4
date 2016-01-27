using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers.Pedigree;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Utilities;
using RiskApps3.Model;
using System.Reflection;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.View.PatientRecord.PMH;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord.PMH;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    /// <summary>
    /// This control is intended to serve as the top-level API for applications
    /// using the pedigree core library.
    /// </summary>
    public partial class PedigreeControl : UserControl
    {
        public int FrameRate
        {
            get
            {
                return at.frameRate;
            }
            set
            {
               // at.frameRate = value;
            }
        }

        /**************************************************************************************************/
        public PedigreeModel model;
        PedigreeView view;
        public PedigreeController controller;
        private Queue<RiskApps3.Model.PatientRecord.FHx.FamilyHistory> FHxQueue;
        AnimationThread at = new AnimationThread();
        private bool continueAnimation = true;
        public GUIPreference currentPrefs;

        private Dictionary<int, List<PedigreeIndividual>> Groups;

        /**************************************************************************************************/
        public delegate void ModelConvergedCallbackType();
        public ModelConvergedCallbackType ModelConvergedDelegate;

        /**************************************************************************************************/
        PedigreeForm.CloseFormCallbackType CloseFormCallback;
        PedigreeForm.PedigreeModelLoadedCallbackType PedigreeModelLoadedCallback;
        PedigreeForm.SelectedIndividualCallbackType SelectedIndividualCallback;

        /**************************************************************************************************/
        public List<Dictionary<int, PointWithVelocity>> individualHistory = new List<Dictionary<int, PointWithVelocity>>();
        public List<Dictionary<CoupleID, PointWithVelocity>> couplesHistory = new List<Dictionary<CoupleID, PointWithVelocity>>();

        /**************************************************************************************************/
        Dictionary<int, PointWithVelocity> previosPos = new Dictionary<int, PointWithVelocity>();
        Dictionary<CoupleID, PointWithVelocity> previosCouplePos = new Dictionary<CoupleID, PointWithVelocity>();

        /**************************************************************************************************/

        /**************************************************************************************************/
        public PedigreeControl(bool createThread)
        {
            InitializeComponent();
            FHxQueue = new Queue<RiskApps3.Model.PatientRecord.FHx.FamilyHistory>();
            ModelConvergedDelegate = ModelConverged;
            this.DoubleBuffered = true;
        }

        /**************************************************************************************************/
        public PedigreeControl()
        {
            InitializeComponent();

            FHxQueue = new Queue<RiskApps3.Model.PatientRecord.FHx.FamilyHistory>();

            this.DoubleBuffered = true;

            ModelConvergedDelegate = ModelConverged;

            at.SpawnAnimationThread
            (
                delegate()
                {
                    if (model != null && controller != null)
                    {
                        controller.IncrementLayout();
                        if (controller.LayoutComplete)
                        {
                            if (CloseFormCallback != null)
                            {
                                CloseFormCallback.Invoke();
                            }
                        }
                    }

                    this.Refresh();

                    return continueAnimation;
                }
            );


            /**************************************************************************************************/
            MouseDown += new MouseEventHandler(delegate(object sender, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Right)
                {
                    RightClickTimer.Enabled = true;
                }

                //PushPositionHistory();

                if (model != null)
                {
                    model.io.mouseX = e.X;
                    model.io.mouseY = e.Y;
                    model.io.mouseDown = true;
                    model.io.button = e.Button;
                }

                //PointWithVelocity pointUnderCursor = GetPointUnderCursor(model, e.X, e.Y);
                PedigreeIndividual underCursor = PedigreeUtils.GetIndividualUnderPoint(model, e.X, e.Y);

                if (underCursor != null)
                {
                    if (model.Selected.Count > 1)
                    {
                        foreach (PedigreeIndividual pi in model.Selected)
                        {
                            model.pointsBeingDragged.Add(pi.point);
                        }
                        if (!(model.pointsBeingDragged.Contains(underCursor.point)))
                        {
                            model.pointsBeingDragged.Clear();
                        }
                    }
                    else
                    {
                        if (underCursor.Group == null)
                        {
                            model.pointsBeingDragged.Add(underCursor.point);
                        }
                        else
                        {
                            foreach (PedigreeIndividual gm in underCursor.Group)
                            {
                                model.pointsBeingDragged.Add(gm.point);
                            }
                        }
                    }
                }

            });

            /**************************************************************************************************/
            MouseClick += new MouseEventHandler(delegate(object sender, MouseEventArgs e)
            {

            });

            /**************************************************************************************************/
            MouseUp += new MouseEventHandler(delegate(object sender, MouseEventArgs e)
            {

                if (model.SelectionLasso.Count > 0)
                {
                    Polygon poly = new Polygon(model.SelectionLasso.ToArray());

                    model.Selected.Clear();
                    foreach (PedigreeIndividual pi in model.individuals)
                    {
                        if (!model.parameters.hideNonBloodRelatives || pi.bloodRelative)
                        {
                            System.Drawing.Point scaled = new System.Drawing.Point((int)pi.point.x, (int)pi.point.y);

                            scaled.X += model.parameters.hOffset;
                            scaled.Y += model.parameters.vOffset;

                            scaled.X = (int)(model.parameters.scale * (double)scaled.X);
                            scaled.Y = (int)(model.parameters.scale * (double)scaled.Y);

                            if (poly.Contains(scaled))
                            {
                                pi.Selected = true;
                                if (model.Selected.Contains(pi) == false)
                                {
                                    model.Selected.Add(pi);
                                    model.pointsBeingDragged.Add(pi.point);
                                }
                            }
                            else
                            {
                                pi.Selected = false;
                            }
                        }
                    }
                }
                else
                {

                    PedigreeIndividual individual = PedigreeUtils.GetIndividualUnderPoint(model, (e.X), (e.Y));
                    if (individual != null)
                    {
                        if (individual.Selected == false)
                        {
                            foreach (PedigreeIndividual pi in model.Selected)
                            {
                                pi.Selected = false;
                            }
                            model.Selected.Clear();

                            if (individual.Group == null)
                            {
                                if (SelectedIndividualCallback != null)
                                {
                                    SelectedIndividualCallback.Invoke(individual.HraPerson);
                                }
                                individual.Selected = true;
                                if (model.Selected.Contains(individual) == false)
                                    model.Selected.Add(individual);
                            }
                            else
                            {
                                if (SelectedIndividualCallback != null)
                                {
                                    SelectedIndividualCallback.Invoke(null);
                                }
                                foreach (PedigreeIndividual gm in individual.Group)
                                {
                                    gm.Selected = true;
                                    if (model.Selected.Contains(gm) == false)
                                        model.Selected.Add(gm);
                                }
                            }
                        }
                    }
                    else
                    {
                        //if (currentPrefs != null)
                        //{
                        //    currentPrefs.GUIPreference_xOffset = model.parameters.hOffset;
                        //    currentPrefs.GUIPreference_yOffset = model.parameters.vOffset;
                        //}
                        if (SelectedIndividualCallback != null)
                        {
                            SelectedIndividualCallback.Invoke(null);
                        }
                        foreach (PedigreeIndividual pi in model.Selected)
                        {
                            pi.Selected = false;
                        }
                        model.Selected.Clear();
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    if (RightClickTimer.Enabled)
                    {
                        RightClickTimer.Enabled = false;
                        ShowContextMenu(e);

                        if (model.Selected.Count == 1)
                        {
                            if (model.Selected[0].HraPerson.PMH.Observations.Count > 0)
                            {
                                removeToolStripMenuItem.Enabled = true;
                                foreach (ClincalObservation co in model.Selected[0].HraPerson.PMH.Observations)
                                {
                                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                                    tsmi.Text = co.disease;
                                    if (string.IsNullOrEmpty(co.ageDiagnosis) == false)
                                    {
                                        tsmi.Text += " @ " + co.ageDiagnosis;
                                    }
                                    removeToolStripMenuItem.DropDownItems.Add(tsmi);
                                    tsmi.Tag = co;
                                    tsmi.Click += new System.EventHandler(RemoveCoClick);
                                }
                            }
                            else
                            {
                                removeToolStripMenuItem.Enabled = false;
                            }
                        }
                        else
                        {
                            removeToolStripMenuItem.Enabled = false;
                        }
                    }
                    else
                    {
                        //removeToolStripMenuItem.Enabled = false;
                    }
                }

                if (model != null)
                {
                    model.io.mouseX = e.X;
                    model.io.mouseY = e.Y;
                    model.io.mouseDown = false;
                    model.io.button = e.Button;
                }

                if (model.pointsBeingDragged.Count > 0)
                {
                    PushPositionHistory();

                    foreach (PedigreeIndividual p in model.individuals)
                    {
                        if (model.pointsBeingDragged.Contains(p.point))
                        {
                            bool changed = false;

                            double tempXpos = 800 / (p.point.x - ((model.displayXMax - 800) / 2));
                            double tempYpos = 600 / (p.point.y - ((model.displayYMax - 600) / 2));
                            int tempXnorm = (int)(p.point.x - (model.displayXMax / 2));
                            int tempYnorm = (int)(p.point.y - (model.displayYMax / 2));

                            if (tempXpos != p.HraPerson.x_position)
                            {
                                p.HraPerson.x_position = tempXpos;
                                changed = true;
                            }
                            if (tempYpos != p.HraPerson.y_position)
                            {
                                p.HraPerson.y_position = tempYpos;
                                changed = true;
                            }
                            if (tempXnorm != p.HraPerson.x_norm)
                            {
                                p.HraPerson.x_norm = tempXnorm;
                                changed = true;
                            }
                            if (tempYnorm != p.HraPerson.y_norm)
                            {
                                p.HraPerson.y_norm = tempYnorm;
                                changed = true;
                            }
                            if (changed)
                            {
                                HraModelChangedEventArgs args = new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent));
                                if (p.HraPerson.x_position > float.MinValue && p.HraPerson.x_position < float.MaxValue)
                                {
                                    foreach (MemberInfo mi in p.HraPerson.GetType().GetMember("x_*"))
                                    {
                                        args.updatedMembers.Add(mi);
                                    }
                                }
                                if (p.HraPerson.y_position > float.MinValue && p.HraPerson.y_position < float.MaxValue)
                                {
                                    foreach (MemberInfo mi in p.HraPerson.GetType().GetMember("y_*"))
                                    {
                                        args.updatedMembers.Add(mi);
                                    }
                                }
                                p.HraPerson.SignalModelChanged(args);
                            }
                        }
                    }
                }

                if (model.RelativesToLink.Count > 0)
                {
                    LinkToSelected();

                    model.RelativesToLink.Clear();
                    PushPositionHistory();
                    SavePositions(true);
                    SetPedigreeFromFHx(model.familyHistory);
                }

                model.pointsBeingDragged.Clear();

                model.SelectionLasso.Clear();

            });

            /**************************************************************************************************/
            MouseMove += new MouseEventHandler(delegate(object sender, MouseEventArgs e)
            {
                if (model != null)
                {
                    if (model.io.mouseDown)
                    {
                        if (!(model.pointsBeingDragged.Count > 0))
                        {
                            if (model.parameters.multiSelect)
                            {
                                model.SelectionLasso.Add(e.Location);
                            }
                            else
                            {
                                model.parameters.hOffset += (int)((System.Convert.ToDouble(e.X) - (model.io.mouseX)) / model.parameters.scale);
                                model.parameters.vOffset += (int)((System.Convert.ToDouble(e.Y) - (model.io.mouseY)) / model.parameters.scale);
                            }
                        }
                        else
                        {
                            foreach (PointWithVelocity point in model.pointsBeingDragged)
                            {
                                point.x += ((e.X - model.io.mouseX) / model.parameters.scale);
                                point.y += ((e.Y - model.io.mouseY) / model.parameters.scale);
                            }
                        }
                    }
                    model.io.mouseX = e.X;
                    model.io.mouseY = e.Y;
                }
            });
        }

        /**************************************************************************************************/
        private void LinkToSelected()
        {
            if (model.Selected.Count > 0 && model.RelativesToLink.Count > 0)
            {
                PedigreeIndividual single_parent = null;
                PedigreeCouple newParents = null;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    if (pi.spouseCouples.Count == 0)
                    {
                        single_parent = pi;
                    }

                    foreach (PedigreeCouple pc in pi.spouseCouples)
                    {
                        newParents = pc;
                    }
                }

                foreach (PedigreeIndividual pi in model.RelativesToLink)
                {
                    foreach (PedigreeCouple pc in model.couples)
                    {
                        if (pc.children.Contains(pi))
                            pc.children.Remove(pi);
                    }

                    if (newParents != null)
                    {
                        model.familyHistory.LinkRelative(pi.HraPerson, newParents);
                        newParents.children.Add(pi);
                        pi.point.x = (newParents.mother.point.x + newParents.father.point.x) / 2;
                        pi.point.y = ((newParents.mother.point.y + newParents.father.point.y) / 2) + model.parameters.verticalSpacing;
                        //pi.HraPerson.Person_x_position = pi.point.x;
                        //pi.HraPerson.Person_y_position = pi.point.y;
                    }
                    else if (single_parent != null)
                    {
                        Person spouse = model.familyHistory.LinkRelative(pi.HraPerson, single_parent);
                        //pi.HraPerson.x_position = single_parent.HraPerson.x_position;
                        //pi.HraPerson.y_position = single_parent.HraPerson.y_position + model.parameters.verticalSpacing;
                        //spouse.x_position = single_parent.HraPerson.x_position;
                        //spouse.y_position = single_parent.HraPerson.y_position;
                        pi.point.x = single_parent.point.x;
                        pi.point.y = single_parent.point.y + model.parameters.verticalSpacing;
                        //pi.HraPerson.Person_x_position = pi.point.x;
                        //pi.HraPerson.Person_y_position = pi.point.y;
                    }
                }
            }
        }

        /**************************************************************************************************/
        private PointWithVelocity GetPointUnderCursor(PedigreeModel model, double x, double y)
        {

            PedigreeIndividual individual = PedigreeUtils.GetIndividualUnderPoint(model, x, y);
            if (individual != null)
                return individual.point;
            else
                return null;
        }

        /**************************************************************************************************/
        public void SetSelection(Person person)
        {

            if (model != null)
            {
                model.Selected.Clear();
                foreach (PedigreeIndividual pi in model.individuals)
                {
                    if (pi.HraPerson == person)
                    {
                        pi.Selected = true;
                        if (model.Selected.Contains(pi) == false)
                            model.Selected.Add(pi);
                    }
                    else
                    {
                        pi.Selected = false;
                    }
                }
            }
        }

        /**************************************************************************************************/
        public void ModelConverged()
        {
            PushPositionHistory();
            SavePositions(true);
        }

        /**************************************************************************************************/
        public void Register(PedigreeForm.CloseFormCallbackType CloseFormDelegate, PedigreeForm.PedigreeModelLoadedCallbackType PedigreeModelLoadedDelegate, PedigreeForm.SelectedIndividualCallbackType SelectedIndividualDelegate)
        {
            CloseFormCallback = CloseFormDelegate;
            PedigreeModelLoadedCallback = PedigreeModelLoadedDelegate;
            SelectedIndividualCallback = SelectedIndividualDelegate;
        }

        /**************************************************************************************************/
        private void ShowContextMenu(MouseEventArgs e)
        {
            PedigreeIndividual individual = PedigreeUtils.GetIndividualUnderPoint(model, (e.X), (e.Y));

            if (individual != null)
            {
                contextMenuStrip1.Tag = individual;

                if (model.Selected.Count > 1)
                {
                    linkToParentToolStripMenuItem.Enabled = false;
                    deletePersonToolStripMenuItem.Enabled = false;
                    //addParentsToolStripMenuItem.Enabled = false;
                }
                else
                {
                    if (individual.HraPerson.IsStockRelative() == true)
                    {
                        linkToParentToolStripMenuItem.Enabled = false;
                        deletePersonToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        linkToParentToolStripMenuItem.Enabled = true;
                        deletePersonToolStripMenuItem.Enabled = true;
                    }

                    if (individual.Parents == null)
                    {
                        addParentsToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        addParentsToolStripMenuItem.Enabled = false;
                    }

                    if (individual.hasChildren())
                    {
                        deletePersonToolStripMenuItem.Enabled = false;
                    }
                }
                contextMenuStrip1.Show(this, e.X, e.Y);
            }
        }

        /**************************************************************************************************/
        protected override void OnPaint(PaintEventArgs pe)
        {
            if (view != null)
                view.Draw(pe.Graphics);
        }

        /**************************************************************************************************/
        public void DrawFromView(Graphics g, int width, int height)
        {
            if (view != null)
                view.Draw(g);
        }

        /**************************************************************************************************/
        public void SavePositions(bool persist)
        {
            foreach (PedigreeIndividual p in model.individuals)
            {
                p.HraPerson.x_position = 800 / (p.point.x - ((model.displayXMax - 800) / 2));
                p.HraPerson.y_position = 600 / (p.point.y - ((model.displayYMax - 600) / 2));
                p.HraPerson.x_norm = (int)(p.point.x - (model.displayXMax / 2));
                p.HraPerson.y_norm = (int)(p.point.y - (model.displayYMax / 2));

                HraModelChangedEventArgs args = new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent));
                args.Persist = persist;
                if (p.HraPerson.x_position > float.MinValue && p.HraPerson.x_position < float.MaxValue)
                {
                    foreach (MemberInfo mi in p.HraPerson.GetType().GetMember("x_*"))
                    {
                        args.updatedMembers.Add(mi);
                    }
                }
                if (p.HraPerson.y_position > float.MinValue && p.HraPerson.y_position < float.MaxValue)
                {
                    foreach (MemberInfo mi in p.HraPerson.GetType().GetMember("y_*"))
                    {
                        args.updatedMembers.Add(mi);
                    }
                }
                p.HraPerson.SignalModelChanged(args);
            }
        }
        /**************************************************************************************************/
        public void SaveSelectedPositions(bool persist)
        {
            foreach (PedigreeIndividual p in model.Selected)
            {
                p.HraPerson.x_position = 800 / (p.point.x - ((model.displayXMax - 800) / 2));
                p.HraPerson.y_position = 600 / (p.point.y - ((model.displayYMax - 600) / 2));
                p.HraPerson.x_norm = (int)(p.point.x - (model.displayXMax / 2));
                p.HraPerson.y_norm = (int)(p.point.y - (model.displayYMax / 2));

                HraModelChangedEventArgs args = new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent));
                args.Persist = persist;
                if (p.HraPerson.x_position > float.MinValue && p.HraPerson.x_position < float.MaxValue)
                {
                    foreach (MemberInfo mi in p.HraPerson.GetType().GetMember("x_*"))
                    {
                        args.updatedMembers.Add(mi);
                    }
                }
                if (p.HraPerson.y_position > float.MinValue && p.HraPerson.y_position < float.MaxValue)
                {
                    foreach (MemberInfo mi in p.HraPerson.GetType().GetMember("y_*"))
                    {
                        args.updatedMembers.Add(mi);
                    }
                }
                p.HraPerson.SignalModelChanged(args);
            }
        }
        /**************************************************************************************************/
        public void PushPositionHistory()
        {
            try
            {
                if (this.model != null)
                {
                    //SavePositions(false);

                    Dictionary<int, PointWithVelocity> indiPos = new Dictionary<int, PointWithVelocity>();
                    foreach (PedigreeIndividual pi in model.individuals)
                    {
                        PointWithVelocity p = new PointWithVelocity();
                        p.x = pi.point.x;
                        p.y = pi.point.y;
                        indiPos.Add(pi.HraPerson.relativeID, p);
                    }
                    individualHistory.Add(indiPos);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        /**************************************************************************************************/
        private void PedigreeControl_Resize(object sender, EventArgs e)
        {
            if (model != null)
            {

            }
        }

        /**************************************************************************************************/
        public void UndoLastPosition()
        {
            if (individualHistory.Count > 1)
            {
                Dictionary<int, PointWithVelocity> indiPos = individualHistory[individualHistory.Count - 2];
                foreach (PedigreeIndividual pi in model.individuals)
                {
                    if (indiPos.ContainsKey(pi.HraPerson.relativeID))
                    {
                        pi.point.x = indiPos[pi.HraPerson.relativeID].x;
                        pi.point.y = indiPos[pi.HraPerson.relativeID].y;
                    }
                }
                individualHistory.RemoveAt(individualHistory.Count - 1);
            }
        }

        /**************************************************************************************************/
        private void RightClick_Tick(object sender, EventArgs e)
        {
            //RightClickTimer.Enabled = false;

            //if (controller != null)
            //    controller.SetMode("SELF_ORGANIZING");
        }

        /**************************************************************************************************/
        public void ForceLoadFamily(RiskApps3.Model.PatientRecord.FHx.FamilyHistory fhx)
        {
            FHxQueue.Enqueue(fhx);
            LoadFamilyWorking_DoWork(null, null);
        }
        
        /**************************************************************************************************/
        private void LoadFamilyWorking_DoWork(object sender, DoWorkEventArgs e)
        {
            while (FHxQueue.Count > 0)
            {
                RiskApps3.Model.PatientRecord.FHx.FamilyHistory fhx = FHxQueue.Dequeue();
                if (FHxQueue.Count == 0)
                {
                    PedigreeModel pm = new PedigreeModel(fhx)
                    {
                        controlHeight = this.Height,
                        controlWidth = this.Width
                    };


                    previosPos.Clear();

                    List<PedigreeIndividual> unplaced = new List<PedigreeIndividual>();
                    int matAuntUncCount = 0;
                    int patAuntUncCount = 0;
                    int brosisCount = 0;
                    int childCount = 0;
                    foreach (PedigreeIndividual pi in pm.individuals)
                    {
                        if (SetPersonFromHraValues(pi, pm) == false)
                        {
                            unplaced.Add(pi);
                        }
                        else
                        {
                            switch (Relationship.Parse(pi.HraPerson.relationship))
                            {
                                case RelationshipEnum.SPOUSE:
                                case RelationshipEnum.BROTHER:
                                case RelationshipEnum.SISTER:
                                    brosisCount++;
                                    break;
                                case RelationshipEnum.SON:
                                case RelationshipEnum.DAUGHTER:
                                    childCount++;
                                    break;
                                case RelationshipEnum.AUNT:
                                case RelationshipEnum.UNCLE:
                                    if (pi.HraPerson.bloodline == "Maternal")
                                    {
                                        matAuntUncCount++;
                                    }
                                    else if (pi.HraPerson.bloodline == "Paternal")
                                    {
                                        patAuntUncCount++;
                                    }
                                    break;
                            }
                        }
                    }
                    foreach (PedigreeCouple pc in pm.couples)
                    {
                        if (LoadFamilyWorking.CancellationPending)
                            return;

                        if (pc.mother == null)
                        {
                            pc.point.x = pc.father.point.x;
                            pc.point.y = pc.father.point.y;
                        }
                        else if (pc.father == null)
                        {
                            pc.point.x = pc.mother.point.x;
                            pc.point.y = pc.mother.point.y;
                        }
                        else
                        {
                            pc.point.x = (pc.mother.point.x + pc.father.point.x) / 2;
                            pc.point.y = (pc.mother.point.y + pc.father.point.y) / 2;
                        }
                    }


                    Groups = new Dictionary<int, List<PedigreeIndividual>>();
                    foreach (PedigreeIndividual pi in pm.individuals)
                    {
                        if (pi.HraPerson.pedigreeGroup > 0)
                        {
                            if (Groups.Keys.Contains(pi.HraPerson.pedigreeGroup))
                            {
                                Groups[pi.HraPerson.pedigreeGroup].Add(pi);
                                pi.Group = Groups[pi.HraPerson.pedigreeGroup];
                            }
                            else
                            {
                                List<PedigreeIndividual> newGroup = new List<PedigreeIndividual>();
                                newGroup.Add(pi);
                                pi.Group = newGroup;
                                Groups.Add(pi.HraPerson.pedigreeGroup, newGroup);
                            }
                        }
                    }
                    if (model != null)
                        pm.parameters.Set(model.parameters);

                    this.model = pm;

                    //List<PedigreeIndividual> remainder =
                    StaticLayout(unplaced, brosisCount, matAuntUncCount, patAuntUncCount, childCount);


                    if (FHxQueue.Count == 0)
                    {
                        view = new PedigreeView(model);
                        if (controller != null)
                        {
                            controller = new PedigreeController(model, controller.GetMode());
                        }
                        else
                        {
                            controller = new PedigreeController(model, "");
                        }
                        controller.ModelConvergedCallback += ModelConverged;
                    }
                }
            }
        }

        public List<PedigreeIndividual> StaticLayout(List<PedigreeIndividual> unplaced, int brosisCount, int matAuntUncCount, int patAuntUncCount, int childCount)
        {
            List<PedigreeIndividual> retval = new List<PedigreeIndividual>();
            bool foundSpouse = false;
            foreach (PedigreeIndividual p in unplaced)
            {
                if (p.HraPerson.Person_relationshipOther == "Spouse of Self")
                {
                    brosisCount++;
                }
            }
            foreach (PedigreeIndividual p in unplaced)
            {

                switch (Relationship.Parse(p.HraPerson.relationship))
                {
                    case RelationshipEnum.SELF:
                        p.point.x = (model.displayXMax / 2) - 13;
                        p.point.y = (model.displayYMax / 2) + 99;
                        break;
                    case RelationshipEnum.BROTHER:
                    case RelationshipEnum.SISTER:

                        if (brosisCount % 2 == 0)
                            p.point.x = (model.displayXMax / 2) + (70 + ((brosisCount / 2) * model.parameters.horizontalSpacing));
                        else
                            p.point.x = (model.displayXMax / 2) - (70 + ((brosisCount / 2) * model.parameters.horizontalSpacing));

                        p.point.y = (model.displayYMax / 2) + 99; brosisCount++;
                        break;
                    case RelationshipEnum.MOTHER:
                        p.point.x = (model.displayXMax / 2) + 57;
                        p.point.y = (model.displayYMax / 2) + 0;
                        break;
                    case RelationshipEnum.FATHER:
                        p.point.x = (model.displayXMax / 2) - 83;
                        p.point.y = (model.displayYMax / 2) + 0;
                        break;
                    case RelationshipEnum.AUNT:
                    case RelationshipEnum.UNCLE:
                        if (p.HraPerson.bloodline == "Maternal")
                        {
                            matAuntUncCount++;
                            p.point.x = (model.displayXMax / 2) + (57 + (matAuntUncCount * model.parameters.horizontalSpacing));
                            p.point.y = (model.displayYMax / 2) + 0;
                        }
                        else if (p.HraPerson.bloodline == "Paternal")
                        {
                            patAuntUncCount++;
                            p.point.x = (model.displayXMax / 2) - (83 + (patAuntUncCount * model.parameters.horizontalSpacing));
                            p.point.y = (model.displayYMax / 2) + 0;
                        }
                        break;
                    case RelationshipEnum.GRANDMOTHER:
                        if (p.HraPerson.bloodline == "Maternal")
                        {
                            p.point.x = (model.displayXMax / 2) + 92;
                            p.point.y = (model.displayYMax / 2) - 99;
                        }
                        else if (p.HraPerson.bloodline == "Paternal")
                        {
                            p.point.x = (model.displayXMax / 2) - 48;
                            p.point.y = (model.displayYMax / 2) - 99;
                        }
                        break;
                   case RelationshipEnum.GRANDFATHER:

                        if (p.HraPerson.bloodline == "Maternal")
                        {
                            p.point.x = (model.displayXMax / 2) + 22;
                            p.point.y = (model.displayYMax / 2) - 99;
                        }
                        else if (p.HraPerson.bloodline == "Paternal")
                        {
                            p.point.x = (model.displayXMax / 2) - 118;
                            p.point.y = (model.displayYMax / 2) - 99;
                        }
                        break;
 
                    case RelationshipEnum.SON:
                        if (childCount % 2 == 0)
                            p.point.x = (model.displayXMax / 2) + (35 + ((childCount / 2) * model.parameters.horizontalSpacing));
                        else
                            p.point.x = (model.displayXMax / 2) - (35 + ((childCount / 2) * model.parameters.horizontalSpacing));

                        p.point.y = (model.displayYMax / 2) + 198;
                        childCount++;
                        break;
                    
                    case RelationshipEnum.DAUGHTER:
                        if (childCount % 2 == 0)
                            p.point.x = (model.displayXMax / 2) + (35 + ((childCount / 2) * model.parameters.horizontalSpacing));
                        else
                            p.point.x = (model.displayXMax / 2) - (35 + ((childCount / 2) * model.parameters.horizontalSpacing));

                        p.point.y = (model.displayYMax / 2) + 198;
                        childCount++;
                        break;

                    case RelationshipEnum.GRANDFATHERS_FATHER:
                        if (p.HraPerson.bloodline == "Maternal")
                        {
                            p.point.x = (model.displayXMax / 2) + 82;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        else if (p.HraPerson.bloodline == "Paternal")
                        {
                            p.point.x = (model.displayXMax / 2) - 58;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        break;
                    case RelationshipEnum.GRANDFATHERS_MOTHER:
                        if (p.HraPerson.bloodline == "Maternal")
                        {
                            p.point.x = (model.displayXMax / 2) + 102;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        else if (p.HraPerson.bloodline == "Paternal")
                        {
                            p.point.x = (model.displayXMax / 2) - 38;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        break;
                    case RelationshipEnum.GRANDMOTHERS_FATHER:
                        if (p.HraPerson.bloodline == "Maternal")
                        {
                            p.point.x = (model.displayXMax / 2) + 12;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        else if (p.HraPerson.bloodline == "Paternal")
                        {
                            p.point.x = (model.displayXMax / 2) - 128;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        break;
                    case RelationshipEnum.GRANDMOTHERS_MOTHER:
                        if (p.HraPerson.bloodline == "Maternal")
                        {
                            p.point.x = (model.displayXMax / 2) + 32;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        else if (p.HraPerson.bloodline == "Paternal")
                        {
                            p.point.x = (model.displayXMax / 2) - 108;
                            p.point.y = (model.displayYMax / 2) - 198;
                        }
                        break;
                    
                    default:
                        if (p.HraPerson.Person_relationshipOther == "Spouse of Self")
                        {
                            p.point.x = (model.displayXMax / 2) + 70;
                            p.point.y = (model.displayYMax / 2) + 99;
                        }
                        else
                        {
                            retval.Add(p);
                        }
                        break;
                }

            }

            if (retval.Count > 0)
            {
                double maxX = 0;
                double yMin = double.MaxValue;
                double yMax = double.MinValue;
                double yAcc = ((model.displayYMax - model.displayYMin) / 2) - (this.Height / 2) + (double)(model.parameters.verticalSpacing);

                foreach (PedigreeIndividual pi in model.individuals)
                {
                    if (retval.Contains(pi) == false)
                    {
                        if (pi.point.x > maxX)
                            maxX = pi.point.x;
                        if (pi.point.y < yMin)
                            yMin = pi.point.y;
                        if (pi.point.y > yMax)
                            yMax = pi.point.y;
                    }
                }
                List<PedigreeIndividual> matchedBySpouse = new List<PedigreeIndividual>();
                foreach (PedigreeIndividual pi in retval)
                {
                    bool placed = false;
                    foreach (PedigreeCouple pc in pi.spouseCouples)
                    {
                        if (pc.mother != null && pc.father != null)
                        {
                            if (pi.HraPerson.relativeID == pc.mother.HraPerson.relativeID && retval.Contains(pc.father) == false)
                            {
                                pi.point.x = pc.father.point.x + model.parameters.horizontalSpacing / 2;
                                pi.point.y = pc.father.point.y;
                                placed = true;
                                matchedBySpouse.Add(pi);
                                break;
                            }
                            else if (pi.HraPerson.relativeID == pc.father.HraPerson.relativeID && retval.Contains(pc.mother) == false)
                            {
                                pi.point.x = pc.mother.point.x - model.parameters.horizontalSpacing / 2;
                                pi.point.y = pc.mother.point.y;
                                matchedBySpouse.Add(pi);
                                placed = true;
                                break;
                            }
                        }
                    }
                    if (!placed)
                    {
                        //bool placedByKids = false;
                        foreach (PedigreeIndividual query in model.individuals)
                        {
                            if (query.HraPerson.motherID == pi.HraPerson.relativeID)
                            {
                                //placedByKids = true;
                                matchedBySpouse.Add(pi);
                                pi.point.x = query.point.x + model.parameters.horizontalSpacing / 2;
                                pi.point.y = query.point.y - model.parameters.verticalSpacing;
                                break;
                            }
                            else if (query.HraPerson.fatherID == pi.HraPerson.relativeID)
                            {
                                //placedByKids = true;
                                matchedBySpouse.Add(pi);
                                pi.point.x = query.point.x - model.parameters.horizontalSpacing / 2;
                                pi.point.y = query.point.y - model.parameters.verticalSpacing;
                                break;
                            }
                        }
                        //if (placedByKids == false)
                        //{
                        //    int col_hgt = 10 * model.parameters.verticalSpacing;
                        //    int col = (int)yAcc / col_hgt;
                        //    int row = (int)yAcc % col_hgt;
                        //    pi.point.x = maxX + (col * model.parameters.horizontalSpacing);
                        //    pi.point.y = yMin + row; //yAcc;
                        //    yAcc += model.parameters.verticalSpacing;
                        //}
                    }
                }
                foreach (PedigreeIndividual pi in matchedBySpouse)
                {
                    retval.Remove(pi);
                }
                yAcc = 0;
                foreach (PedigreeIndividual pi in retval)
                {
                    bool matchedByChildren = false;
                    foreach (PedigreeCouple pc in model.couples)
                    {
                        if (pc.children.Contains(pi))
                        {
                            if (pc.mother != null && pc.father != null)
                            {
                                pi.point.x = (pc.mother.point.x + pc.father.point.x) / 2;
                                pi.point.y = pc.mother.point.y + model.parameters.verticalSpacing;
                                matchedByChildren = true;
                                break;
                            }
                        }
                    }
                    if (matchedByChildren == false)
                    {
                        //pi.HraPerson.motherID = 0;
                        //pi.HraPerson.motherID = 0;
                        //int col_hgt = 10 * model.parameters.verticalSpacing;
                        //int col = (int)yAcc / col_hgt;
                        //int row = (int)yAcc % col_hgt;
                        //pi.point.x = (3 * model.parameters.horizontalSpacing) + maxX + (col * model.parameters.horizontalSpacing);
                        //pi.point.y = yMin + row - (col_hgt / 4); //yAcc;
                        //yAcc += model.parameters.verticalSpacing;

                        double x = maxX + model.parameters.horizontalSpacing + model.parameters.horizontalSpacing; 
                        double y = yMin;

                        foreach (PedigreeIndividual search in model.individuals)
                        {
                            if (search != pi)
                            {
                                if (Math.Abs(search.point.x - pi.point.x) < model.parameters.horizontalSpacing / 2 &&
                                    Math.Abs(search.point.y - pi.point.y) < model.parameters.verticalSpacing / 2)
                                {
                                    y += model.parameters.verticalSpacing;
                                }
                            }
                        }
                        pi.point.y = y;
                        pi.point.x = x;
                    }
                }
            }
            foreach (PedigreeCouple pc in model.couples)
            {
                if (pc.mother != null && pc.father != null)
                {
                    if (pc.mother.Parents == null && pc.father.Parents == null)
                    {
                        if (pc.children.Count > 0)
                        {
                            double spacing = Math.Abs(pc.mother.point.x - pc.father.point.x);
                            double xAvg = 0;
                            foreach (PedigreeIndividual pi in pc.children)
                            {
                                xAvg += pi.point.x;
                            }
                            xAvg /= pc.children.Count;

                            pc.point.x = xAvg;
                            if (pc.mother.point.x > pc.father.point.x)
                            {
                                pc.mother.point.x = xAvg + (spacing / 2);
                                pc.father.point.x = xAvg - (spacing / 2);
                            }
                            else
                            {
                                pc.mother.point.x = xAvg - (spacing / 2);
                                pc.father.point.x = xAvg + (spacing / 2);
                            }
                        }
                    }
                }
            }

            return retval;
        }

        public bool SetPersonFromHraValues(PedigreeIndividual pi, PedigreeModel pm)
        {
            

            bool retval = true;
            if (pi.HraPerson.x_norm > int.MinValue)
            {
                pi.point.x = (pm.displayXMax / 2) + pi.HraPerson.x_norm;
            }
            else
            {
                if (pi.HraPerson.x_position == 0)
                {
                    retval = false;
                    pi.point.x = pm.displayXMax / 2;
                }
                else
                {
                    pi.point.x = (800 / pi.HraPerson.x_position) + ((pm.displayXMax - 800) / 2);
                }
            }

            if (pi.HraPerson.y_norm > int.MinValue)
            {
                pi.point.y = (pm.displayYMax / 2) + pi.HraPerson.y_norm;
            }
            else
            {
                if (pi.HraPerson.y_position == 0)
                {
                    retval = false;
                    pi.point.y = pm.displayYMax / 2;
                }
                else
                {
                    pi.point.y = (600 / pi.HraPerson.y_position) + ((pm.displayYMax - 600) / 2);
                }
            }

            return retval;

        }
        /**************************************************************************************************/
        public void SetPedigreeModel(PedigreeModel model)
        {


        }

        /**************************************************************************************************/
        //private void personChanged(object sender, HraModelChangedEventArgs e)
        //{
        //    if (e.sendingView != (HraView)(this.Parent))
        //    {
        //        foreach (PedigreeIndividual pi in model.individuals)
        //        {

        //            if (pi.HraPerson == (Person)sender)
        //                SetPersonFromHraValues(pi, model);
        //            //return;
        //        }
        //    }
        //}

        /**************************************************************************************************/
        private void LoadFamilyWorking_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void LoadFamilyWorking_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //loadingCircle1.Active = false;
            //loadingCircle1.Visible = false;
            if (FHxQueue.Count > 0)
            {
                LoadFamilyWorking.RunWorkerAsync();
            }
            else if (PedigreeModelLoadedCallback != null)
            {
                PedigreeModelLoadedCallback.Invoke(model);
            }
        }

        /**************************************************************************************************/
        public void SetPedigreeFromFHx(RiskApps3.Model.PatientRecord.FHx.FamilyHistory fhx)
        {
            FHxQueue.Enqueue(fhx);
            if (!LoadFamilyWorking.IsBusy)
            {
                LoadFamilyWorking.RunWorkerAsync();
            }

        }


        /**************************************************************************************************/
        private void addSonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controller.GetMode() == "SELF_ORGANIZING" && model.converged == false)
                SavePositions(true);
        
            AddSon(sender, false);
        }

        /**************************************************************************************************/
        private void addParentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (model.Selected.Count == 0)
            {
                if (contextMenuStrip1.Tag != null)
                {
                    PedigreeIndividual pi = (PedigreeIndividual)(contextMenuStrip1.Tag);
                    List<Person> parents = model.familyHistory.AddParents(pi.HraPerson,model);
                    //foreach (Person new_parent in parents)
                    //{
                    //    //Relationship.SetRelationshipByParentType(Gender.Parse(new_parent.gender), Relationship.Parse(pi.HraPerson.relationship), Bloodline.Parse(pi.HraPerson.bloodline), out new_parent.relationship, out new_parent.relationshipOther, out new_parent.bloodline);
                    //    double x = pi.point.x;
                    //    if (new_parent.gender.ToLower().StartsWith("f"))
                    //        x += (model.parameters.horizontalSpacing / 4);
                    //    else if (new_parent.gender.ToLower().StartsWith("m"))
                    //        x -= (model.parameters.horizontalSpacing / 4);
                    //    double y = pi.point.y - model.parameters.verticalSpacing;
                    //    new_parent.x_position = model.displayXMax / x;
                    //    new_parent.y_position = model.displayYMax / y;
                    //    new_parent.x_norm = (int)(x - (model.displayXMax / 2));
                    //    new_parent.y_norm = (int)(y - (model.displayYMax / 2));
                    //    new_parent.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
                    //}
                    pi.HraPerson.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
                }
            }
            else
            {
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    List<Person> parents = model.familyHistory.AddParents(pi.HraPerson,model);
                    //foreach (Person new_parent in parents)
                    //{
                    //    //Relationship.SetRelationshipByParentType(Gender.Parse(new_parent.gender), Relationship.Parse(pi.HraPerson.relationship), Bloodline.Parse(pi.HraPerson.bloodline), out new_parent.relationship, out new_parent.relationshipOther, out new_parent.bloodline);
                    //    //double x = pi.point.x;
                    //    //double y = pi.point.y - model.parameters.verticalSpacing;
                    //    //if (new_parent.gender.ToLower().StartsWith("f"))
                    //    //    x += (model.parameters.horizontalSpacing / 4);
                    //    //else if (new_parent.gender.ToLower().StartsWith("m"))
                    //    //    x -= (model.parameters.horizontalSpacing / 4);
                    //    //new_parent.x_position = model.displayXMax / x;
                    //    //new_parent.y_position = model.displayYMax / y;
                    //    //new_parent.x_norm = (int)(x - (model.displayXMax / 2));
                    //    //new_parent.y_norm = (int)(y - (model.displayYMax / 2));
                    //    //new_parent.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
                    //}
                    pi.HraPerson.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));

                }
            }


            //model.familyHistory.Relatives.ForEach(p => p.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent))));
            //TODO ??? model.familyHistory.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));

        }

        /**************************************************************************************************/
        private void addDaughterToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (controller.GetMode() == "SELF_ORGANIZING" && model.converged == false)
                SavePositions(true);
        
            
            AddDaughter(sender, false);
        }

        /***************************************************************************/
        private void AddSon(object sender, bool CreateNewSpouse)
        {
            int count = 1;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Tag != null)
                int.TryParse((string)(tsmi.Tag), out count);

            if (model.Selected.Count == 0)
            {
                if (contextMenuStrip1.Tag != null)
                {
                    PedigreeIndividual parent = (PedigreeIndividual)(contextMenuStrip1.Tag);
                    List<Person> kids = model.familyHistory.AddChild(parent.HraPerson, CreateNewSpouse, count, GenderEnum.Male);
                    foreach (Person kid in kids)
                    {
                        // we either got a kid or spouse
                        if (kid.motherID == parent.HraPerson.relativeID || kid.fatherID == parent.HraPerson.relativeID)
                        {
                            double x = parent.point.x;
                            foreach (PedigreeCouple pc in model.couples)
                            {
                                if (pc.mother.HraPerson.relativeID == kid.motherID && pc.father.HraPerson.relativeID == kid.fatherID)
                                {
                                    x = (pc.mother.point.x + pc.father.point.x) / 2;
                                }
                            }
                            double y = parent.point.y + model.parameters.verticalSpacing;
                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }
                        else
                        {
                            double x = parent.point.x;
                            double y = parent.point.y;

                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }

                        kid.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
                        //this.model.familyHistory.AddToList(kid, new HraModelChangedEventArgs((HraView)this.Parent));
                    }
                }
            }
            else
            {
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    List<Person> kids = model.familyHistory.AddChild(pi.HraPerson, CreateNewSpouse, count, GenderEnum.Male);
                    foreach (Person kid in kids)
                    {
                        if (kid.motherID == pi.HraPerson.relativeID || kid.fatherID == pi.HraPerson.relativeID)
                        {
                            double x = pi.point.x;
                            foreach (PedigreeCouple pc in model.couples)
                            {
                                if (pc.mother.HraPerson.relativeID == kid.motherID && pc.father.HraPerson.relativeID == kid.fatherID)
                                {
                                    x = (pc.mother.point.x + pc.father.point.x) / 2;
                                }
                            }
                            double y = pi.point.y + model.parameters.verticalSpacing;
                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }
                        else
                        {
                            double spouseOffset = 0;

                            if (pi.spouseCouples.Count > 0)
                            {
                                if (pi.spouseCouples[0].mother == pi)
                                {
                                    if (pi.spouseCouples[0].father.point.x < pi.point.x)
                                    {
                                        spouseOffset += model.parameters.horizontalSpacing;
                                    }
                                    else
                                    {
                                        spouseOffset -= model.parameters.horizontalSpacing;
                                    }
                                }
                                else
                                {
                                    if (pi.spouseCouples[0].mother.point.x < pi.point.x)
                                    {
                                        spouseOffset += model.parameters.horizontalSpacing;
                                    }
                                    else
                                    {
                                        spouseOffset -= model.parameters.horizontalSpacing;
                                    }
                                }
                            }
                            double x = pi.point.x + spouseOffset;
                            double y = pi.point.y;

                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }
                        kid.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
                        //this.model.familyHistory.AddToList(kid, new HraModelChangedEventArgs((HraView)this.Parent));
                    }
                }
            }
            //model.familyHistory.Relatives.ForEach(p => p.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent))));
            //TODO ??? model.familyHistory.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
        }

        /***************************************************************************/
        private void AddDaughter(object sender, bool CreateNewSpouse)
        {
            int count = 1;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Tag != null)
                int.TryParse((string)(tsmi.Tag), out count);

            if (model.Selected.Count == 0)
            {
                if (contextMenuStrip1.Tag != null)
                {
                    PedigreeIndividual parent = (PedigreeIndividual)(contextMenuStrip1.Tag);
                    List<Person> kids = model.familyHistory.AddChild(parent.HraPerson, CreateNewSpouse, count, GenderEnum.Female);
                    foreach (Person kid in kids)
                    {
                        if (kid.motherID == parent.HraPerson.relativeID || kid.fatherID == parent.HraPerson.relativeID)
                        {
                            double x = parent.point.x;
                            foreach (PedigreeCouple pc in model.couples)
                            {
                                if (pc.mother.HraPerson.relativeID == kid.motherID && pc.father.HraPerson.relativeID == kid.fatherID)
                                {
                                    x = (pc.mother.point.x + pc.father.point.x) / 2;
                                }
                            }
                            double y = parent.point.y + model.parameters.verticalSpacing;
                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }
                        else
                        {
                            double x = parent.point.x;
                            double y = parent.point.y;

                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }
                        kid.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
                        //this.model.familyHistory.AddToList(kid, new HraModelChangedEventArgs((HraView)this.Parent));
                    }
                }
            }
            else
            {
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    List<Person> kids = model.familyHistory.AddChild(pi.HraPerson, CreateNewSpouse, count, GenderEnum.Female);
                    foreach (Person kid in kids)
                    {
                        if (kid.motherID == pi.HraPerson.relativeID || kid.fatherID == pi.HraPerson.relativeID)
                        {
                            double x = pi.point.x;
                            foreach (PedigreeCouple pc in model.couples)
                            {
                                if (pc.mother.HraPerson.relativeID == kid.motherID && pc.father.HraPerson.relativeID == kid.fatherID)
                                {
                                    x = (pc.mother.point.x + pc.father.point.x) / 2;
                                }
                            }
                            double y = pi.point.y + model.parameters.verticalSpacing;
                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }
                        else
                        {
                            double x = pi.point.x;
                            double y = pi.point.y;

                            kid.x_position = model.displayXMax / x;
                            kid.y_position = model.displayYMax / y;
                            kid.x_norm = (int)(x - (model.displayXMax / 2));
                            kid.y_norm = (int)(y - (model.displayYMax / 2));
                        }
                        kid.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
                        //this.model.familyHistory.AddToList(kid, new HraModelChangedEventArgs((HraView)this.Parent));
                    }
                }
            }
            //model.familyHistory.Relatives.ForEach(p => p.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent))));
            //TODO ??? model.familyHistory.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs((HraView)(this.Parent)));
        }


        internal void ReleaseListeners()
        {
            at.Shutdown();

            if (model != null)
            {
                foreach (PedigreeIndividual pi in model.individuals)
                {
                    pi.HraPerson.ReleaseListeners(this);
                }
            }

            SessionManager.Instance.RemoveHraView(this);
        }

        internal void CenterIndividuals()
        {

            double xMin = double.MaxValue;
            double xMax = double.MinValue;
            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            foreach (PedigreeIndividual pi in model.individuals)
            {
                if (xMin > pi.point.x)
                    xMin = pi.point.x;
                if (xMax < pi.point.x)
                    xMax = pi.point.x;
                if (yMin > pi.point.y)
                    yMin = pi.point.y;
                if (yMax < pi.point.y)
                    yMax = pi.point.y;
            }

            double xMid = (xMin + xMax) / 2;
            double yMid = (yMin + yMax) / 2;

            double deltax = ((model.displayXMin + model.displayXMax) / 2) - xMid;
            double deltay = ((model.displayYMin + model.displayYMax) / 2) - yMid;

            foreach (PedigreeIndividual pi in model.individuals)
            {
                pi.point.x += deltax;
                pi.point.y += deltay;
            }

        }

        internal void SetPersonPosition(Person person)
        {
            foreach (PedigreeIndividual pi in model.individuals)
            {
                if (pi.HraPerson == person)
                {
                    SetPersonFromHraValues(pi, model);
                }
            }
        }

        private void addDiseaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Point screenPoint = Cursor.Position;
            Point pictureBoxPoint = this.PointToClient(screenPoint);

            int count = 1;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if (tsmi.Tag != null)
                int.TryParse((string)(tsmi.Tag), out count);


            if (model.Selected.Count == 0)
            {

            }
            else
            {
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    for (int i = 0; i < count; i++)
                    {
                        AddDiseasePopup adp = new AddDiseasePopup();
                        adp.pmh = pi.HraPerson.PMH;

                        adp.sendingView = (HraView)(this.Parent);


                        int X = model.parameters.hOffset + (int)pi.point.x; ;
                        int Y = model.parameters.vOffset + (int)pi.point.y; ;

                        X = (int)(model.parameters.scale * (double)X);
                        Y = (int)(model.parameters.scale * (double)Y);



                        adp.Show();
                        if (X + adp.Width > this.Location.X + this.Width)
                        {
                            X = X - adp.Width;
                        }
                        adp.Location = this.PointToScreen(new Point(X, Y + (i * adp.Height)));
                        adp.SetDiseaseBoxFocus();
                    }
                }
            }
        }



        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            removeToolStripMenuItem.DropDownItems.Clear();
        }
        /**************************************************************************************************/
        private void RemoveCoClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            ClincalObservation co = (ClincalObservation)tsmi.Tag;

            if (co != null)
            {
                co.owningPMH.Observations.RemoveFromList(co, SessionManager.Instance.securityContext);
            }
        }

        internal void Close()
        {
            continueAnimation = false;
            if (controller != null)
                controller.SetMode("MANUAL");

        }

        public void AlignVertically()
        {
            if (model.Selected.Count < 2)
            {

            }
            else
            {
                double avg = 0;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    avg += pi.point.x;
                }
                avg /= model.Selected.Count;

                foreach (PedigreeIndividual pi in model.Selected)
                {
                    pi.point.x = avg;
                }
            }
        }
        public void AlignHorizontaly()
        {
            if (model.Selected.Count < 2)
            {

            }
            else
            {
                double avg = 0;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    avg += pi.point.y;
                }
                avg /= model.Selected.Count;

                foreach (PedigreeIndividual pi in model.Selected)
                {
                    pi.point.y = avg;
                }
            }
        }
        public void DistributeHorizontaly(double delta)
        {
            if (model.Selected.Count < 2)
            {

            }
            else
            {
                double min = double.MaxValue;
                double max = double.MinValue;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    if (pi.point.x > max)
                        max = pi.point.x;
                    if (pi.point.x < min)
                        min = pi.point.x;
                }

                double spacing = delta * ((max - min) / (model.Selected.Count - 1));

                model.Selected.Sort(ComparePedigreeIndividualByXPos);
                double count = 0;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    pi.point.x = min + (count * spacing);
                    count++;
                }
            }
        }
        public void ShrinkCouples()
        {

            foreach (PedigreeIndividual pi in model.Selected)
            {
                if (pi.spouseCouples.Count == 1)
                {
                    if ((pi.spouseCouples[0].father.HraPerson.motherID == 0 && 
                        pi.spouseCouples[0].father.HraPerson.fatherID == 0) ||
                        (pi.spouseCouples[0].mother.HraPerson.motherID == 0 &&
                        pi.spouseCouples[0].mother.HraPerson.fatherID == 0))
                    {
                        double xavg = (pi.spouseCouples[0].father.point.x + pi.spouseCouples[0].mother.point.x)/2;
                        if (pi.spouseCouples[0].father.point.x > pi.spouseCouples[0].mother.point.x)
                        {
                            pi.spouseCouples[0].father.point.x = xavg + (model.parameters.horizontalSpacing / 2);
                            pi.spouseCouples[0].mother.point.x = xavg - (model.parameters.horizontalSpacing / 2);
                        }
                        else
                        {
                            pi.spouseCouples[0].father.point.x = xavg - (model.parameters.horizontalSpacing / 2);
                            pi.spouseCouples[0].mother.point.x = xavg + (model.parameters.horizontalSpacing / 2);
                        }
                    }
                }
            }
            
        }
        public void DistributeByAge()
        {
            if (model.Selected.Count == 0)
            {

            }
            else
            {
                List<double> locations = new List<double>();
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    locations.Add(pi.point.x);
                }
                locations.Sort();
                model.Selected.Sort(ComparePedigreeIndividualByAge);
                int count = 0;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    pi.point.x = locations[count];
                    count++;
                }
            }
        }
        public void DistributeVertically()
        {
            if (model.Selected.Count < 2)
            {

            }
            else
            {
                double min = double.MaxValue;
                double max = double.MinValue;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    if (pi.point.y > max)
                        max = pi.point.y;
                    if (pi.point.y < min)
                        min = pi.point.y;
                }
                double spacing = (max - min) / (model.Selected.Count - 1);

                model.Selected.Sort(ComparePedigreeIndividualByYPos);
                double count = 0;
                foreach (PedigreeIndividual pi in model.Selected)
                {
                    pi.point.y = min + (count * spacing);
                    count++;
                }
            }
        }

        private static int ComparePedigreeIndividualByXPos(PedigreeIndividual x, PedigreeIndividual y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    return x.point.x.CompareTo(y.point.x);
                }
            }
        }
        private static int ComparePedigreeIndividualByAge(PedigreeIndividual x, PedigreeIndividual y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    int xAge = 0;
                    int.TryParse(x.HraPerson.age, out xAge);
                    int yAge = 0;
                    int.TryParse(y.HraPerson.age, out yAge);
                    return xAge.CompareTo(yAge);
                }
            }
        }
        private static int ComparePedigreeIndividualByYPos(PedigreeIndividual x, PedigreeIndividual y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (y == null)
                {
                    return 1;
                }
                else
                {
                    return x.point.y.CompareTo(y.point.y);
                }
            }
        }

        private void addSonSpouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSon(sender, true);
        }

        private void addSonWNewSpouseX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSon(sender, true);
        }

        private void addSonWNewSpouseX3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSon(sender, true);
        }

        private void addDaughterWNewSpouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDaughter(sender, true);
        }

        private void addDaughterWNewSpouseX2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDaughter(sender, true);
        }

        private void addDaughterWNewSpouseX3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDaughter(sender, true);
        }

        private void addDaughterWNewSpouseX4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDaughter(sender, true);
        }

        private void monozygoticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (model.Selected.Count < 2)
                return;

            int newTwinId = 1;
            foreach (PedigreeIndividual pi in model.individuals)
            {
                if (pi.HraPerson.twinID >= newTwinId)
                    newTwinId = pi.HraPerson.twinID + 1;
            }

            foreach (PedigreeIndividual pi in model.Selected)
            {
                pi.HraPerson.Person_twinID = newTwinId;
                pi.HraPerson.Person_twinType = "Identical";
            }
        }

        private void dizygoticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (model.Selected.Count < 2)
                return;

            int newTwinId = 1;
            foreach (PedigreeIndividual pi in model.individuals)
            {
                if (pi.HraPerson.twinID >= newTwinId)
                    newTwinId = pi.HraPerson.twinID + 1;
            }

            foreach (PedigreeIndividual pi in model.Selected)
            {
                pi.HraPerson.Person_twinID = newTwinId;
                pi.HraPerson.Person_twinType = "Fraternal";
            }
        }

        private void unTwinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PedigreeIndividual pi in model.Selected)
            {
                pi.HraPerson.Person_twinID = 0;
            }
        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (model.Selected.Count > 1)
            {
                //determine group number - first check if a selected member is in a group
                List<PedigreeIndividual> target = null;
                double targetX = -1;

                foreach (PedigreeIndividual pi in model.Selected)
                {
                    if (pi.Group != null)
                    {
                        target = pi.Group;
                        targetX = pi.point.x;
                        break;
                    }
                }
                int groupId = -1;
                if (target != null )
                {
                    foreach (int i in Groups.Keys)
                    {
                        if (Groups[i] == target)
                        {
                            groupId = i;
                        }
                    }
                    if (groupId > 0)
                    {
                        foreach (PedigreeIndividual pi in model.Selected)
                        {
                            if (pi.Group == null)
                            {
                                if (targetX > 0)
                                {
                                    pi.point.x = targetX;
                                    //pi.HraPerson.Person_x_position = pi.point.x;
                                    //int tempXnorm = (int)(pi.point.x - (model.displayXMax / 2));
                                    //pi.HraPerson.Person_x_norm = tempXnorm;
                                }
                                pi.Group = target;
                                pi.Group.Add(pi);
                                pi.HraPerson.Person_pedigreeGroup = groupId;
                            }
                        }
                    }
                }
                else
                {
                    groupId = 1;
                    while (Groups.Keys.Contains(groupId))
                    {
                        groupId++;
                    }
                    List<PedigreeIndividual> newGroup = new List<PedigreeIndividual>();
                    
                    double groupX = 0;
                    foreach (PedigreeIndividual pi in model.Selected)
                    {
                        pi.HraPerson.Person_pedigreeGroup = groupId;
                        newGroup.Add(pi);
                        pi.Group = newGroup;
                        groupX += pi.point.x;
                    }
                    groupX /= newGroup.Count;

                    Groups.Add(groupId, newGroup);

                    foreach (PedigreeIndividual pi in newGroup)
                    {
                        pi.point.x = groupX;
                    }
                }

                SaveSelectedPositions(true);
            }
        }

        private void ungroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PedigreeIndividual pi in model.Selected)
            {
                pi.Group.Remove(pi);
                if (pi.Group.Count == 0)
                    if (Groups.Keys.Contains(pi.HraPerson.Person_pedigreeGroup))
                        Groups.Remove(pi.HraPerson.Person_pedigreeGroup);

                pi.HraPerson.Person_pedigreeGroup = -1;

                pi.Group = null;
            }
        }

        private void unhideSpouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PedigreeIndividual pi in model.Selected)
            {
                RecursivelyUnhide(pi);

            }
        }

        private void RecursivelyUnhide(PedigreeIndividual pi)
        {
            foreach (PedigreeCouple pc in pi.spouseCouples)
            {
                pc.mother.bloodRelative = true;
                pc.father.bloodRelative = true;
                foreach (PedigreeIndividual child in pc.children)
                {
                    child.bloodRelative = true;
                    RecursivelyUnhide(child);
                }
            }
        }

        private void hideSpouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PedigreeIndividual pi in model.Selected)
            {
                pi.bloodRelative = false;
            }

        }

        public void MoveHiddenToSpouse()
        {
            foreach (PedigreeIndividual pi in model.individuals)
            {
                if (pi.bloodRelative == false)
                {
                    foreach (PedigreeCouple pc in pi.spouseCouples)
                    {
                        if (pi.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                        {
                            pi.point.x = pc.father.point.x;
                        }
                        else
                        {
                            pi.point.x = pc.mother.point.x;
                        }
                    }
                }
            }

        }

        private void linkToParentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (PedigreeIndividual pi in model.Selected)
            {
                model.RelativesToLink.Add(pi);
            }

            foreach (PedigreeIndividual pi in model.Selected)
            {
                pi.Selected = false;
            }
            model.Selected.Clear();
        }

        private void PedigreeControl_Resize_1(object sender, EventArgs e)
        {
            if (model != null)
            {
                model.controlHeight = this.Height;
                model.controlWidth = this.Width;
            }
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (contextMenuStrip1.Tag != null)
            {
                PedigreeIndividual pi = (PedigreeIndividual)(contextMenuStrip1.Tag);
                foreach (PedigreeCouple pc in pi.spouseCouples)
                {
                    foreach (PedigreeIndividual kid in pc.children)
                    {
                        if (kid.HraPerson.motherID == pi.HraPerson.relativeID)
                        {
                            kid.HraPerson.Person_motherID = 0;
                        }
                        else if (kid.HraPerson.fatherID == pi.HraPerson.relativeID)
                        {
                            kid.HraPerson.Person_fatherID = 0;
                        }
                    }
                }
                pi.HraPerson.owningFHx.RemoveFromList(pi.HraPerson, SessionManager.Instance.securityContext);
            }
            
        }

        private void consanguineousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (model.Selected.Count == 2)
            {
                foreach (PedigreeCouple pc in model.couples)
                {
                    if (model.Selected.Contains(pc.mother) && model.Selected.Contains(pc.father))
                    {
                        pc.mother.HraPerson.Person_consanguineousSpouseID = pc.father.HraPerson.relativeID;
                        pc.father.HraPerson.Person_consanguineousSpouseID = pc.mother.HraPerson.relativeID;
                    }
                }
            }
        }

        private void notConsanguineousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (model.Selected.Count == 2)
            {
                foreach (PedigreeCouple pc in model.couples)
                {
                    if (model.Selected.Contains(pc.mother) && model.Selected.Contains(pc.father))
                    {
                        pc.mother.HraPerson.Person_consanguineousSpouseID = 0;
                        pc.father.HraPerson.Person_consanguineousSpouseID = 0;
                    }
                }
            }
        }
    }
}



//if (remainder.Count > 0)
//{
//    double maxX = 0;
//    double yAcc = ((model.displayYMax - model.displayYMin) / 2) - (this.Height / 2) + (double)(model.parameters.verticalSpacing);

//    foreach (PedigreeIndividual pi in model.individuals)
//    {
//        if (pi.point.x > maxX)
//            maxX = pi.point.x;
//    }
//    foreach (PedigreeIndividual pi in remainder)
//    {
//        pi.point.x = maxX + (2 * model.parameters.horizontalSpacing);
//        pi.point.y = yAcc;
//        yAcc += model.parameters.verticalSpacing;
//    }
//}
//foreach (PedigreeCouple pc in pm.couples)
//{
//    if (pc.mother != null && pc.father != null)
//    {
//        if (pc.mother.Parents == null && pc.father.Parents == null)
//        {
//            if (pc.children.Count > 0)
//            {
//                double spacing = Math.Abs(pc.mother.point.x - pc.father.point.x);
//                double xAvg = 0;
//                foreach (PedigreeIndividual pi in pc.children)
//                {
//                    xAvg += pi.point.x;
//                }
//                xAvg /= pc.children.Count;

//                pc.point.x = xAvg;
//                if (pc.mother.point.x > pc.father.point.x)
//                {
//                    pc.mother.point.x = xAvg + (spacing / 2);
//                    pc.father.point.x = xAvg - (spacing / 2);
//                }
//                else
//                {
//                    pc.mother.point.x = xAvg - (spacing / 2);
//                    pc.father.point.x = xAvg + (spacing / 2);
//                }
//            }
//        }
//    }
//}