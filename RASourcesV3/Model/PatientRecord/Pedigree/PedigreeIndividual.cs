using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    public class PedigreeIndividual
    {
        //public PedigreeIndividual(Person b) : base(b.relativeID, b.motherID, b.fatherID, b.gender, b, b.Diseases, b.Panels, b.GeneticTests, b.initial_x_fraction, b.initial_y_fraction) { }
        public const string GENDER_MALE = "Male";
        public const string GENDER_FEMALE = "Female";

        public Person HraPerson;

        public PedigreeIndividual(Person b)
        {
            HraPerson = b;
        }

        public List<PedigreeIndividual> Group;

        public readonly PointWithVelocity point = new PointWithVelocity();

        PedigreeIndividual mother = null;

        PedigreeIndividual father = null;

        PedigreeCouple parents = null;

        public int numberOfAncestors = 0;

        public bool Selected = false;

        public bool bloodRelative = false;

        //Temporary debugging variable
        //public bool mustBeMoved = false;
        public bool wasMoved = false;

        public double leftEdge = 0;
        public double rightEdge = 0;
        /// <summary>
        /// The couples in which this individual participates. The size of this
        /// list is usually 0 or 1, but can be 2 in the case of half siblings.
        /// </summary>
        public readonly List<PedigreeCouple> spouseCouples = new List<PedigreeCouple>();

        /// <summary>
        /// The mother of this individual.
        /// This property is immutable but not known at construction time.
        /// It can be set only once, otherwise an exception is thrown.
        /// </summary>
        public PedigreeIndividual Mother
        {
            get { return mother; }
            set
            {
                if (mother == null)
                    mother = value;
                else
                    throw new Exception("Attempted to set mother twice!");
            }
        }

        //CJL Added 
        public bool hasChildren()
        {
            foreach (PedigreeCouple spouseCouple in spouseCouples)
            {
                if (spouseCouple.children != null && spouseCouple.children.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        //CJL Added 
        public bool hasParent()
        {
            if (mother != null)
            {
                return true;
            }
            if (father != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// The father of this individual.
        /// This property is immutable but not known at construction time.
        /// It can be set only once, otherwise an exception is thrown.
        /// </summary>
        public PedigreeIndividual Father
        {
            get { return father; }
            set
            {
                if (father == null)
                    father = value;
                else
                    throw new Exception("Attempted to set father twice!");
            }
        }

        /// <summary>
        /// Computes and returns the spouse of this individual. Before calling
        /// this, make sure that individual.spouseCouples.Count == 1, otherwise
        /// an exception will be thrown.
        /// </summary>
        //public PedigreeIndividual Spouse
        //{
        //    get
        //    {
        //        if (spouseCouples.Count == 1)
        //            if (id == spouseCouples[0].mother.relativeID)
        //                return spouseCouples[0].father;
        //            else
        //                return spouseCouples[0].mother;
        //        else
        //            throw new Exception("Attempted to access the spouse of an individual with "+
        //                (spouseCouples.Count ==0?"no spouse!":"more than one spouse!"));
        //    }
        //}




        /// <summary>
        /// The couple which represents the parents of this individual.
        /// This property is immutable but not known at construction time.
        /// It can be set only once, otherwise an exception is thrown.
        /// </summary>
        public PedigreeCouple Parents
        {
            get { return parents; }
            set
            {
                if (parents == null)
                    parents = value;
                else
                    throw new Exception("Attempted to set parents twice!");
            }
        }

        public bool HasSpouse()
        {
            return spouseCouples.Count == 1;
        }
    }
}
