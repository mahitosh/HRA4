using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using HRA4.Services;
using HRA4.ViewModels;
using System.Reflection;
namespace HRA4.Web.Controllers
{
    public class HighRiskFollowupController : BaseController
    {


        
        // GET: HighRiskFollowup
        public ActionResult Index(string type)
        {
            Session["type"] = type;
            
            /*
            List<HighRiskLifetimeBreast> lst = new List<HighRiskLifetimeBreast>();
            lst = _applicationContext.ServiceContext.RiskClinicServices.GetPatients(type);
            return View(lst);
             */
            return View();
        }

        public ActionResult Search(string txtSearch, bool chkExcludeCancerGeneticsPatients, bool chkExcludeDoNotContactPatients, bool chkExcludepatientswithgenetictesting)
        {

            string type = Session["type"].ToString();



           // List<HighRiskLifetimeBreast> lst = new List<HighRiskLifetimeBreast>();
            var lst = _applicationContext.ServiceContext.RiskClinicServices.GetPatients(type);

            if (chkExcludeCancerGeneticsPatients == true)
            {
                lst = (from c in lst where c.isRCPt.GetValueOrDefault() == 0 select c).ToList(); 
            }
            if (chkExcludeDoNotContactPatients == true)
            {
                lst = (from c in lst where c.DoNotContact.GetValueOrDefault() == 0 select c).ToList(); 
            }
            if (chkExcludepatientswithgenetictesting == true)
            {
                lst = (from c in lst where c.genTested.GetValueOrDefault() == 0 select c).ToList(); 
            }
            
           

                if (type.Trim().ToLower() == "LBC".Trim().ToLower())
                {
                    if (txtSearch.Length > 0)
                    {
                        lst = GetFilterdListForLBC(lst, txtSearch.Trim().ToLower());
                    }
                    return PartialView("_HighRiskLifetimeBreastGrid", lst);
                }
                else
                {
                    if (txtSearch.Length > 0)
                    {
                        lst = GetFilterdListForBRCA(lst, txtSearch.Trim().ToLower());
                    }
                }

               
                return PartialView("_HighRiskBrcaGrid", lst);

        }

        public List<HighRisk> GetFilterdListForLBC(List<HighRisk> lst, string txtSearch)
        {

            lst = lst.Where(c =>
                                (c.patientName != null && 
                                 c.patientName.Trim().ToLower().Contains(txtSearch)
                                )
                               || 
                               (
                                 c.unitnum != null &&
                                 c.unitnum.Trim().ToLower().Contains(txtSearch)
                                )
                               || 
                               (
                                
                                c.MaxLifetimeScore.ToString().Trim().ToLower().Contains(txtSearch)
                               )
                               || 
                               (
                               c.LastCompApptDate != null &&
                               c.LastCompApptDate.ToString().Trim().ToLower().Contains(txtSearch)
                               )
                               || 
                               (
                               c.LastMRI != null &&
                                c.LastMRI.ToString().Trim().ToLower().Contains(txtSearch)
                               )
                               ||
                                (
                                 c.Diseases != null &&
                                 c.Diseases.ToString().Trim().ToLower().Contains(txtSearch)
                                )
                               ||
                               (
                                c.dob != null &&
                                c.dob.ToString().Trim().ToLower().Contains(txtSearch)
                                )
                                ).ToList();

            return lst;
        }

        public List<HighRisk> GetFilterdListForBRCA(List<HighRisk> lst, string txtSearch)
        {
            /*
            lst = lst.Where(c =>( c.patientName != null
                               || c.unitnum != null
                               || c.geneNames != null
                               || c.Diseases != null
                               || c.dob != null
                               )
                               &&
                               (
                                   c.patientName.Trim().ToLower().Contains(txtSearch)
                               || c.unitnum.Trim().ToLower().Contains(txtSearch)
                               || c.MaxBRCAScore.ToString().Trim().ToLower().Contains(txtSearch)
                               || c.geneNames.ToString().Trim().ToLower().Contains(txtSearch)
                               || c.Diseases.ToString().Trim().ToLower().Contains(txtSearch)
                               || c.dob.ToString().Trim().ToLower().Contains(txtSearch)
                               )
                                ).ToList();
            */

            lst = lst.Where(c => 
                                (
                                c.patientName != null &&
                                c.patientName.Trim().ToLower().Contains(txtSearch)
                                )
                                ||
                                (
                                 c.unitnum != null &&
                                 c.unitnum.Trim().ToLower().Contains(txtSearch)
                                )
                                ||
                                (
                                 c.geneNames != null &&
                                 c.geneNames.ToString().Trim().ToLower().Contains(txtSearch)
                                )
                                ||
                                (
                                c.Diseases != null &&
                                 c.Diseases.ToString().Trim().ToLower().Contains(txtSearch)
                                )
                                ||
                                (
                                c.dob != null &&
                                c.dob.ToString().Trim().ToLower().Contains(txtSearch)
                                )
                                ||
                                (
                                    c.MaxBRCAScore.ToString().Trim().ToLower().Contains(txtSearch)
                                )
                          
                          
                           ).ToList();

            return lst;
        }



        public JsonResult GetPatientDetails(string unitnum)
        {
        
            var obj = _applicationContext.ServiceContext.RiskClinicServices.GetPatientDetails(unitnum, -1);
            string PedigreeImagePath = @"data:image/png;base64," + obj.PedigreeImage + "";
            var obj1 = new { obj, ImageUrl = PedigreeImagePath };
            return Json(obj1);

        }




        

    }
}