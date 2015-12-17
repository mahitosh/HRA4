using System;
using System.Collections.Generic;

using System.Text;

using System.Drawing;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    public class PedigreeModel
    {
        /// <summary>
        /// The object containing the fixed parameters of the pedigree diagram.
        /// </summary>
        public readonly PedigreeParameters parameters = new PedigreeParameters();

        /// <summary>
        /// Dynamic variables relating to user interaction IO.
        /// </summary>
        public readonly PedigreeIOVariables io = new PedigreeIOVariables();

        /// <summary>
        /// The FamilyHistory.
        /// </summary>
        public FamilyHistory familyHistory;
        /// <summary>
        /// The list of all individuals in the model.
        /// </summary>
        public readonly List<PedigreeIndividual> individuals = new List<PedigreeIndividual>();

        /// <summary>
        /// A means to look up individuals by id.
        /// </summary>
        public readonly Dictionary<int, PedigreeIndividual> individualsDictionary =
            new Dictionary<int, PedigreeIndividual>();

        /// <summary>
        /// The list of all couples in this model.
        /// </summary>
        public readonly List<PedigreeCouple> couples = new List<PedigreeCouple>();

        public readonly Dictionary<CoupleID, PedigreeCouple> couplesDictionary;

        /// <summary>
        /// The edges of the couples graph.
        /// </summary>
        public readonly List<PedigreeCoupleEdge> coupleEdges = new List<PedigreeCoupleEdge>();

        /// <summary>
        /// The set of all individual sets. Adjacent individual sets in the same level
        /// are repelled by the RepelIndividualSets layout step.
        /// Keys are generational levels.
        /// </summary>
        public readonly Dictionary<int, List<PedigreeIndividualSet>> individualSets = new Dictionary<int, List<PedigreeIndividualSet>>();

        /// <summary>
        /// Whether or not the couples graph is planar this frame.
        /// TODO: Compute this by the DetectEdgeCrossings layout step
        /// </summary>
        public bool couplesGraphIsPlanar = true;

        public bool converged = false;

        /// <summary>
        /// A buffer of pedigree postions
        /// </summary>
        public Dictionary<int, PointWithVelocity> TargetPositions = new Dictionary<int, PointWithVelocity>();
        public Dictionary<int, List<double>> PositionHistoryX = new Dictionary<int, List<double>>();
        public Dictionary<int, List<double>> PositionHistoryY = new Dictionary<int, List<double>>();

        public Dictionary<int, PedigreeIndividual> HoldingPen = new Dictionary<int, PedigreeIndividual>();

        public List<PedigreeIndividual> RelativesToLink = new List<PedigreeIndividual>();

        /// <summary>
        /// Whether or not the pedigree layout (at the individuals level) is
        /// "valid" - that is there are no edge crossings among ancestral connection lines.
        /// </summary>
        public bool layoutIsValid = false;

        /// <summary>
        /// True when the velocities sum to a small number.
        /// </summary>
        public bool forcesHaveConverged = false;

        /// <summary>
        /// The list of all points, of both couples and individuals.
        /// </summary>
        public readonly List<PointWithVelocity> points = new List<PointWithVelocity>();

        /// <summary>
        /// The points currently being dragged.
        /// </summary>
        public readonly List<PointWithVelocity> pointsBeingDragged = new List<PointWithVelocity>();

        /// <summary>
        /// The maximum generational level, computed at construction time.
        /// </summary>
        public readonly int maxGenerationalLevel;

        /// <summary>
        /// The viewport of the display.
        /// </summary>
        public double displayXMin = 0;
        public double displayXMax = 8000;
        public double displayYMin = 0;
        public double displayYMax = 8000;

        public int controlWidth = 100;
        public int controlHeight = 100;

        public int cycles = 0;

        /// <summary>
        /// Parameters to do with pan and zoom.
        /// </summary>


        public Dictionary<GeneticTestResult,List<Person>> FamilialVariants;

        public bool selecting = false;
        public bool firstSnapFrame = true;
        public List<PedigreeIndividual> Selected = new List<PedigreeIndividual>();
        public List<Point> SelectionLasso = new List<Point>();

        public PedigreeModel(FamilyHistory p_familyHistory)
        {
            familyHistory = p_familyHistory;
            
            //Store the individuals as PedigreeIndividuals
            lock (familyHistory.Relatives)
            {
                foreach (Person Person in familyHistory.Relatives)
                {
                    PedigreeIndividual pedigreeIndividual = new PedigreeIndividual(Person);
                    individuals.Add(pedigreeIndividual);
                    individualsDictionary[Person.relativeID] = pedigreeIndividual;
                    points.Add(pedigreeIndividual.point);
                }
            }
            //here keys are the couple ids, values are the couples to which they map
            couplesDictionary = new Dictionary<CoupleID, PedigreeCouple>();

            //Link PedigreeIndividuals with their parents in the object model, derive couples
            foreach (PedigreeIndividual child in individuals)
            {
                //determine whether or not each parent exists in the data
                bool hasMother = individualsDictionary.ContainsKey(child.HraPerson.motherID);
                bool hasFather = individualsDictionary.ContainsKey(child.HraPerson.fatherID);
                CoupleID coupleId = new CoupleID(child.HraPerson.fatherID, child.HraPerson.motherID);
                if (hasMother || hasFather)
                {
                    //link individuals with their parents in the object model, forming a pointer graph
                    if (hasMother)
                        child.Mother = individualsDictionary[child.HraPerson.motherID];
                    if (hasFather)
                        child.Father = individualsDictionary[child.HraPerson.fatherID];

                    //derive couples and store them
                    {
                        //if this individual has parents, set it's "parents" pointer to the
                        //couple representing it's parents (deriving couples from the data as needed).

                        //get the parents couple
                        PedigreeCouple parents = null;
                        {
                            //check if the parents have already been stored as a couple

                            if (couplesDictionary.ContainsKey(coupleId))
                                //if so, then use the previously stored couple.
                                parents = couplesDictionary[coupleId];
                            else
                            {
                                //if not store parents as a couple if they haven't been already
                                parents = new PedigreeCouple(child.Mother, child.Father);
                                couplesDictionary[coupleId] = parents;
                                couples.Add(parents);
                                points.Add(parents.point);



                                //link participating individuals with the new couple
                                if (child.Mother != null)
                                    if (child.Mother.spouseCouples != null)
                                        child.Mother.spouseCouples.Add(parents);

                                if (child.Father != null)
                                    if (child.Father.spouseCouples != null)
                                        child.Father.spouseCouples.Add(parents);
                            }
                        }
                        //set this individual's "parents" pointer to the parents couple
                        child.Parents = parents;

                        //add the child to the children of the parents couple
                        parents.children.Add(child);
                    }
                }
            }

            // derive the intergenerational edges of the couples graph
            foreach (PedigreeIndividual parent in individuals)
            {
                bool parentHasGrandparents = parent.Parents != null;
                bool parentIsPartOfACouple = parent.spouseCouples.Count != 0;

                if (parentHasGrandparents && parentIsPartOfACouple)
                {
                    foreach (PedigreeCouple parents in parent.spouseCouples)
                    {
                        PedigreeCouple grandparents = parent.Parents;
                        bool intergenerational = true;
                        coupleEdges.Add(new PedigreeCoupleEdge(grandparents, parents, intergenerational));
                    }
                }
            }

            // derive the intragenerational edges of the couples graph
            // (from half sibling relationships)
            foreach (PedigreeIndividual parent in individuals)
            {
                if (parent.spouseCouples.Count == 2)
                {
                    PedigreeCouple u = parent.spouseCouples[0];
                    PedigreeCouple v = parent.spouseCouples[1];
                    bool intergenerational = false;
                    coupleEdges.Add(new PedigreeCoupleEdge(u, v, intergenerational));
                }
                //else if (parent.spouseCouples.Count > 2)
                //    throw new Exception("Pedigree not drawable: individual " + parent.relativeID + " has more than two spouses");
            }

            //derive generational levels
            bool undefinedLevelsRemain = true;
            int minGenerationalLevel = 0;
            int maxGenerationalLevel = 0;
            if (couples.Count > 0)
            {
                //assign the seed level
                couples[0].GenerationalLevel = 0;

                //propagate the seed level through the graph
                int NULL = PedigreeCouple.UNDEFINED_GENERATION;
                while (undefinedLevelsRemain)
                {
                    undefinedLevelsRemain = false;
                    foreach (PedigreeCoupleEdge e in coupleEdges)
                    {
                        if (e.intergenerational)
                        {
                            PedigreeCouple grandparents = e.u;
                            PedigreeCouple parents = e.v;

                            bool parentsHaveLevel = parents.GenerationalLevel != NULL;
                            bool grandparentsHaveLevel = grandparents.GenerationalLevel != NULL;

                            if (!parentsHaveLevel || !grandparentsHaveLevel)
                                undefinedLevelsRemain = true;

                            if (!parentsHaveLevel && !grandparentsHaveLevel)
                            {
                                undefinedLevelsRemain = false;
                            }

                            if (parentsHaveLevel && !grandparentsHaveLevel)
                            {
                                grandparents.GenerationalLevel = parents.GenerationalLevel - 1;
                                if (grandparents.GenerationalLevel < minGenerationalLevel)
                                    minGenerationalLevel = grandparents.GenerationalLevel;
                            }
                            else if (!parentsHaveLevel && grandparentsHaveLevel)
                            {
                                parents.GenerationalLevel = grandparents.GenerationalLevel + 1;
                                if (parents.GenerationalLevel > maxGenerationalLevel)
                                    maxGenerationalLevel = parents.GenerationalLevel;
                            }

                        }
                        else
                        {
                            //propagate levels through intragenerational edges (half siblings)
                            if (e.u.GenerationalLevel == NULL)
                                e.u.GenerationalLevel = e.v.GenerationalLevel;
                            else if (e.v.GenerationalLevel == NULL)
                                e.v.GenerationalLevel = e.u.GenerationalLevel;
                        }
                    }
                }
            }

            //normalize the levels
            foreach (PedigreeCouple couple in couples)
                couple.GenerationalLevel -= minGenerationalLevel;

            //store the (normalized) max level in the model
            this.maxGenerationalLevel = maxGenerationalLevel - minGenerationalLevel;

            //when a parent set of half siblings (father, mother, father) 
            //is detedted, the mother id is added to this list. If a 
            //couple involving this mother is detected later, this 
            //list is checked to see if she has already been counted.
            List<int> halfSiblingParentsMotherIds = new List<int>();

            //derive individual sets
            foreach (PedigreeCouple couple in couples)
            {
                //add the [mother,father] individual sets
                if (couple.mother != null && couple.father != null)
                {
                    if (couple.mother.Parents == null && couple.father.Parents == null)
                    {
                        //if the mother has a single spouse
                        if (couple.mother.spouseCouples.Count == 1)
                        {
                            PedigreeIndividualSet parentsIndividualSet = new PedigreeIndividualSet(couple);
                            parentsIndividualSet.Add(couple.mother);
                            parentsIndividualSet.Add(couple.father);
                            AddIndividualSet(couple.GenerationalLevel, parentsIndividualSet);
                        }
                        //if the mother has a two spouses
                        else if (couple.mother.spouseCouples.Count == 2)
                        {
                            //collapse parents of half siblings into a single individual set
                            if (!halfSiblingParentsMotherIds.Contains(couple.mother.HraPerson.relativeID))
                            {
                                halfSiblingParentsMotherIds.Add(couple.mother.HraPerson.relativeID);
                                PedigreeIndividualSet parentsIndividualSet = new PedigreeIndividualSet(couple);
                                parentsIndividualSet.Add(couple.mother);
                                parentsIndividualSet.Add(couple.mother.spouseCouples[0].father);
                                parentsIndividualSet.Add(couple.mother.spouseCouples[1].father);
                                AddIndividualSet(couple.GenerationalLevel, parentsIndividualSet);
                            }
                        }
                    }

                    try
                    {

                        //add the children individual sets
                        PedigreeIndividualSet childrenIndividualSet = new PedigreeIndividualSet(couple);
                        foreach (PedigreeIndividual child in couple.children)
                        {
                            childrenIndividualSet.Add(child);
                            foreach (PedigreeCouple pc in child.spouseCouples)
                            {
                                if (pc.mother.HraPerson.relativeID == child.HraPerson.relativeID)
                                {
                                    childrenIndividualSet.Add(pc.father);
                                }
                                else
                                {
                                    childrenIndividualSet.Add(pc.mother);
                                }
                            }
                        }
                        AddIndividualSet(couple.GenerationalLevel + 1, childrenIndividualSet);
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteToLog(e.ToString());
                    }
                }
            }

            if (individualsDictionary.ContainsKey(1))
                SetBloodRelatives(individualsDictionary[1]);

            foreach(PedigreeIndividual pi in individuals)
            {
                if (pi.bloodRelative == false)
                {
                    if (pi.spouseCouples.Count == 0)
                    {
                        pi.bloodRelative = true;
                    }
                    else
                    {
                        bool bloodFound = false;
                        foreach (PedigreeCouple pc in pi.spouseCouples)
                        {
                            if ((pc.mother != null) && (pc.father != null))
                            {
                                if (pc.mother.bloodRelative == true || pc.father.bloodRelative == true)
                                    bloodFound = true;
                            }
                        }
                        if (bloodFound == false)
                        {
                            foreach (PedigreeCouple pc in pi.spouseCouples)
                            {
                                if (pc.mother != null) pc.mother.bloodRelative = true;
                                if (pc.father != null) pc.father.bloodRelative = true;
                            }
                        }
                    }
                }
            }
            this.FamilialVariants = p_familyHistory.ReloadFamilialVariants();
        }

        private void SetBloodRelatives(PedigreeIndividual pi)
        {
            if (pi != null)
            {
                pi.bloodRelative = true;
                if (pi.Parents != null)
                {
                    foreach (PedigreeIndividual sib in pi.Parents.children)
                    {
                        sib.bloodRelative = true;
                        //if (sib != pi)
                        //{
                            MarkChildrenAsBloodRelatives(sib);
                        //}
                    }
                    SetBloodRelatives(pi.Parents.mother);
                    SetBloodRelatives(pi.Parents.father);
                }
            }
        }

        private void MarkChildrenAsBloodRelatives(PedigreeIndividual sib)
        {
            foreach (PedigreeCouple pc in sib.spouseCouples)
            {
                foreach (PedigreeIndividual kid in pc.children)
                {
                    kid.bloodRelative = true;
                    MarkChildrenAsBloodRelatives(kid);
                }
            }
        }
        public void SetNewMotherFatherId(PedigreeIndividual child, int newMotherId, int newFatherId)
        {
            foreach (PedigreeIndividual pi in individuals)
            {
                if (pi.HraPerson.relativeID == child.HraPerson.relativeID)
                {
                    pi.HraPerson.motherID = newMotherId;
                    pi.HraPerson.fatherID = newFatherId;
                }
            }
        }
        private void AddIndividualSet(int generationalLevel, PedigreeIndividualSet individualSet)
        {
            if (!individualSets.ContainsKey(generationalLevel))
                individualSets[generationalLevel] = new List<PedigreeIndividualSet>();
            individualSets[generationalLevel].Add(individualSet);
        }

        //internal void FindBloodRelatives()
        //{
        //    foreach (PedigreeIndividual pi in individuals)
        //    {
        //        pi.bloodRelative = false;
        //    }
        //    if (individualsDictionary.ContainsKey(1))
        //        SetBloodRelatives(individualsDictionary[1]);
        //}
    }
    public class CoupleID
    {
        public readonly int fatherId;
        public readonly int motherId;

        public CoupleID(PedigreeCouple couple)
        {
            fatherId = couple.father.HraPerson.relativeID;
            motherId = couple.mother.HraPerson.relativeID;
        }

        public CoupleID(int fatherID, int motherID)
        {
            this.fatherId = fatherID;
            this.motherId = motherID;
        }
        public override int GetHashCode()
        {
            return motherId ^ fatherId;
        }
        public override bool Equals(object obj)
        {
            CoupleID c = (CoupleID)obj;
            return c.fatherId == fatherId && c.motherId == motherId;
        }
        public override String ToString()
        {
            return "father:" + fatherId+ " mother:" + motherId;
        }
    }
}
