using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    public class PedigreeCouple
    {
        public bool alreadyHandledHalfSibship = false;
        /// <summary>
        /// The generational level assigned by default before generational
        /// levels are computed.
        /// </summary>
        public const int UNDEFINED_GENERATION = -100000;

        /// <summary>
        /// The generational level of this individual. The generation level of
        /// parents is one greater than that of their grandparents.
        /// </summary>
        int generationalLevel = UNDEFINED_GENERATION;

        /// <summary>
        /// The position and velocity of this couple for use with couples graph layout.
        /// </summary>
        public readonly PointWithVelocity point = new PointWithVelocity();

        /// <summary>
        /// The mother in this couple.
        /// </summary>
        public readonly PedigreeIndividual mother;

        /// <summary>
        /// The father in this couple.
        /// </summary>
        public readonly PedigreeIndividual father;

        /// <summary>
        /// The children of this couple.
        /// </summary>
        public readonly List<PedigreeIndividual> children = new List<PedigreeIndividual>();


        //public double childOverlap = 0;

        /// <summary>
        /// Create a couple with the given father and mother
        /// </summary>
        /// <param name="mother"></param>
        /// <param name="father"></param>
        public PedigreeCouple(PedigreeIndividual mother, PedigreeIndividual father)
        {
            this.mother = mother;
            this.father = father;
        }

        /// <summary>
        /// The generational level of this individual. Parents have a generational level 
        /// one greater than that of their grandparents.
        /// </summary>
        public int GenerationalLevel
        {
            get { return generationalLevel; }
            set { generationalLevel = value; }
        }

        public override int GetHashCode()
        {
            return mother.HraPerson.relativeID ^ father.HraPerson.relativeID;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;
            PedigreeCouple couple = obj as PedigreeCouple;
            if (couple == null)
                return false;
            return father.HraPerson.relativeID == couple.father.HraPerson.relativeID && mother.HraPerson.relativeID == couple.mother.HraPerson.relativeID;
        }

        internal double GetSibshipCenterX(PedigreeModel model)
        {
            //compute the center for the couple's sibship
            double sibshipMinX = double.MaxValue;
            double sibshipMaxX = -double.MaxValue;
            foreach (PedigreeIndividual child in children)
            {
                if (child.point.x < sibshipMinX)
                    sibshipMinX = (int)child.point.x;
                if (child.point.x > sibshipMaxX)
                    sibshipMaxX = (int)child.point.x;
            }
            double sibshipCenterX = (sibshipMinX + sibshipMaxX) / 2;
            return sibshipCenterX;
        }
    }
}
