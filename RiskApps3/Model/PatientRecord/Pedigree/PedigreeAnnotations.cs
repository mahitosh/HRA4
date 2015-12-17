using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    [CollectionDataContract]
    [KnownType(typeof(PedigreeAnnotation))]
    public class PedigreeAnnotationList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        [DataMember] string unitnum;

        public PedigreeAnnotationList() { }  // Default constructor for serialization

        public PedigreeAnnotationList(string p_unitnum)
        {
            unitnum = p_unitnum;
            constructor_args = new object[] { unitnum };
        }

        public override void BackgroundListLoad()
        {
            //SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.annotations;
            pc.Clear();
            
            pc.Add("unitnum", unitnum);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadPedigreeAnnotations",
                                                pc,
                                                typeof(PedigreeAnnotation),
                                                constructor_args);
            DoListLoad(lla);

            if (SessionManager.Instance != null)
            {
                lock (SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.annotations)
                {
                    foreach (PedigreeAnnotation sys_default in SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.annotations)
                    {
                        bool found = false;
                        foreach (PedigreeAnnotation local in this)
                        {
                            if (sys_default.annotation == local.annotation)
                                found = true;
                        }
                        if (found == false)
                        {
                            PedigreeAnnotation new_pa = new PedigreeAnnotation(unitnum);
                            new_pa.annotation = sys_default.annotation;
                            new_pa.area = sys_default.area;
                            new_pa.slot = sys_default.slot;
                            this.Add(new_pa);
                        }
                    }
                }
            }
        }

        public string GetUnitnum()
        {
            return unitnum;
        }

    }
    [DataContract]
    public class PedigreeAnnotation : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public string unitnum;

        [DataMember] [Hra] public string area;
        [DataMember] [Hra] public string annotation;
        [DataMember] [Hra] public int slot;

        #region custom_setters
        #endregion

        public PedigreeAnnotation() { } // Default constructor for serialization

        public PedigreeAnnotation(string p_unitnum)
        {
            unitnum = p_unitnum;
        }

        /**************************************************************************************************/

        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }


        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            var pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            DoPersistWithSpAndParams(e,
                                     "sp_3_Save_PedigreeAnnotations",
                                     ref pc);
        }
    }
}
