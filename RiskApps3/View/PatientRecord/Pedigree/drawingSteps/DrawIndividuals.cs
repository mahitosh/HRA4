using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Model.PatientRecord;
using System.Windows.Forms;
using RiskApps3.Utilities;
using System.Linq;
using System.Drawing.Drawing2D;

namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    /// <summary>
    /// Draws individuals.
    /// </summary>
    internal class DrawIndividuals
    {
        public readonly DrawingStep step;


        private static Pen selectionPen = new Pen(Brushes.Red, 3.0F);
        private static Pen multiSelectionPen = new Pen(Brushes.GreenYellow, 3.0F);

        private Rectangle tempRect = new Rectangle(0, 0, 10, 10);

        private Pen individualsPen = new Pen(Brushes.Black);
        private Font idLabelFont = new Font("Tahoma", 12);

        private Point[] unknownGenderPoints = new Point[4];
        private Point[] adoptedLeftPoints = new Point[4];
        private Point[] adoptedRightPoints = new Point[4];

        private char[] trimChars = {',', ' '};

        public DrawIndividuals(PedigreeModel model)
        {
            step = delegate(Graphics g)
                       {
                           try
                           {
                               Font individualFont = new Font("Tahoma", model.parameters.annotationFontSize);
                               //individualsPen = model.couplesGraphEdgesCross() ? new Pen(Brushes.Red) : new Pen(Brushes.Black);

                               int halfIndividualSize = model.parameters.individualSize/2;

                               foreach (PedigreeIndividual individual in model.individuals)
                               {
                                   if (individual.HraPerson.relativeID <= 0)
                                       break;

                                   if (!model.parameters.hideNonBloodRelatives || individual.bloodRelative)
                                   {
                                       if (individual.HraPerson.relationship == "Self")
                                       {
                                           PointF[] arrow = new PointF[3];

                                           float centerX = (float) (individual.point.x);
                                           float centerY = (float) (individual.point.y);
                                           int glyphSize = model.parameters.individualSize;
                                           int arrowSize = 12;
                                           arrow[0] = new PointF(centerX - glyphSize/2 - arrowSize,
                                                                 centerY + glyphSize/2);
                                           arrow[1] = new PointF(centerX - glyphSize/2, centerY + glyphSize/2);
                                           arrow[2] = new PointF(centerX - glyphSize/2,
                                                                 centerY + glyphSize/2 + arrowSize);

                                           g.FillPolygon(new SolidBrush(Color.Black), arrow);

                                           g.DrawLine(new Pen(Color.Black, 5), centerX - glyphSize/2 - arrowSize,
                                                      centerY + glyphSize/2 + arrowSize,
                                                      centerX - glyphSize/2 - arrowSize/2,
                                                      centerY + glyphSize/2 + arrowSize/2);
                                       }


                                       ////////////////////////////////////////////////////////////////////////////////////////
                                       tempRect.X = (int) individual.point.x - halfIndividualSize;
                                       tempRect.Y = (int) individual.point.y - halfIndividualSize;
                                       tempRect.Width = tempRect.Height = model.parameters.individualSize;

                                       if (individual.HraPerson.gender == PedigreeIndividual.GENDER_FEMALE)
                                           g.FillEllipse(model.parameters.BackgroundBrush, tempRect);
                                       else
                                           g.FillRectangle(model.parameters.BackgroundBrush, tempRect);

                                       /////////////////////////////////////////////////////////////////////////////////////////


                                       foreach (ClincalObservation co in individual.HraPerson.PMH.Observations)
                                       {
                                           if (co.ClinicalObservation_diseaseIconType == "Fill")
                                           {
                                               Brush b;
                                               if (co.ClinicalObservation_diseaseIconColor != null)
                                               {
                                                   b =
                                                       new SolidBrush(
                                                           Color.FromName(co.ClinicalObservation_diseaseIconColor));
                                               }
                                               else
                                               {
                                                   b = new SolidBrush(Color.Transparent);
                                               }
                                               Rectangle fillArea = tempRect;
                                               switch (co.ClinicalObservation_diseaseIconArea)
                                               {
                                                   case "All":
                                                       if (individual.HraPerson.gender ==
                                                           PedigreeIndividual.GENDER_FEMALE)
                                                           g.FillEllipse(b, fillArea);
                                                       else
                                                           g.FillRectangle(b, fillArea);
                                                       break;
                                                   default:
                                                       break;
                                               }
                                           }
                                       }
                                       foreach (ClincalObservation co in individual.HraPerson.PMH.Observations)
                                       {
                                           if (co.ClinicalObservation_diseaseIconType == "Fill")
                                           {
                                               Brush b;
                                               if (co.ClinicalObservation_diseaseIconColor != null)
                                               {
                                                   b =
                                                       new SolidBrush(
                                                           Color.FromName(co.ClinicalObservation_diseaseIconColor));
                                               }
                                               else
                                               {
                                                   b = new SolidBrush(Color.Transparent);
                                               }
                                               Rectangle fillArea = tempRect;
                                               switch (co.ClinicalObservation_diseaseIconArea)
                                               {
                                                   case "UL":
                                                       fillArea.Height = halfIndividualSize;
                                                       fillArea.Width = halfIndividualSize;

                                                       if (individual.HraPerson.gender ==
                                                           PedigreeIndividual.GENDER_FEMALE)
                                                           g.FillPie(b, tempRect, 180, 90);
                                                       else
                                                           g.FillRectangle(b, fillArea);

                                                       break;
                                                   case "UR":
                                                       fillArea.X += halfIndividualSize;
                                                       fillArea.Height = halfIndividualSize;
                                                       fillArea.Width = halfIndividualSize;

                                                       if (individual.HraPerson.gender ==
                                                           PedigreeIndividual.GENDER_FEMALE)
                                                           g.FillPie(b, tempRect, 270, 90);
                                                       else
                                                           g.FillRectangle(b, fillArea);

                                                       break;
                                                   case "LL":
                                                       fillArea.Y += halfIndividualSize;
                                                       fillArea.Height = halfIndividualSize;
                                                       fillArea.Width = halfIndividualSize;

                                                       if (individual.HraPerson.gender ==
                                                           PedigreeIndividual.GENDER_FEMALE)
                                                           g.FillPie(b, tempRect, 90, 90);
                                                       else
                                                           g.FillRectangle(b, fillArea);

                                                       break;
                                                   case "LR":
                                                       fillArea.Y += halfIndividualSize;
                                                       fillArea.X += halfIndividualSize;
                                                       fillArea.Height = halfIndividualSize;
                                                       fillArea.Width = halfIndividualSize;

                                                       if (individual.HraPerson.gender ==
                                                           PedigreeIndividual.GENDER_FEMALE)
                                                           g.FillPie(b, tempRect, 0, 90);
                                                       else
                                                           g.FillRectangle(b, fillArea);

                                                       break;
                                                   default:
                                                       break;
                                               }
                                           }
                                       }
                                       foreach (ClincalObservation co in individual.HraPerson.PMH.Observations)
                                       {
                                           if (co.ClinicalObservation_diseaseIconType == "Dot")
                                           {
                                               Brush b;
                                               if (co.ClinicalObservation_diseaseIconColor != null)
                                               {
                                                   b =
                                                       new SolidBrush(
                                                           Color.FromName(co.ClinicalObservation_diseaseIconColor));
                                               }
                                               else
                                               {
                                                   b = new SolidBrush(Color.Transparent);
                                               }
                                               Rectangle fillArea = tempRect;
                                               fillArea.Width = tempRect.Width/2;
                                               fillArea.Height = tempRect.Height/2;
                                               switch (co.ClinicalObservation_diseaseIconArea)
                                               {
                                                   case "All":
                                                       break;
                                                   case "UL":
                                                       break;
                                                   case "UR":
                                                       fillArea.X += tempRect.Width/2;
                                                       break;
                                                   case "LL":
                                                       fillArea.Y += tempRect.Height/2;
                                                       break;
                                                   case "LR":
                                                       fillArea.X += tempRect.Width/2;
                                                       fillArea.Y += tempRect.Height/2;
                                                       break;
                                                   default:
                                                       break;
                                               }
                                               g.FillEllipse(b, fillArea);
                                           }
                                       }


                                       /////////////////////////////////////////////////////////////////////////////////////
                                       if (model.parameters.annotation_areas != null)
                                           foreach (AnnotationContainer area in model.parameters.annotation_areas)
                                           {
                                               if (area.active)
                                               {
                                                   Point areaOrigin = tempRect.Location;
                                                   int areaMargin = 3;
                                                   int verticalFudge = 8;
                                                   int horizontalFudge = 6;
                                                   int midLevelYOffset = 7;
                                                   switch ((int) area.Position)
                                                   {
                                                       case ((int) AnnotationContainer.IconRelation.LOWER_CENTER):
                                                           areaOrigin = new Point((int) individual.point.x,
                                                                                  (int) individual.point.y +
                                                                                  halfIndividualSize + areaMargin);
                                                           break;
                                                       case ((int) AnnotationContainer.IconRelation.MID_LEFT_UPPER):
                                                           areaOrigin =
                                                               new Point(
                                                                   (int) individual.point.x - halfIndividualSize -
                                                                   horizontalFudge,
                                                                   (int) individual.point.y - verticalFudge -
                                                                   midLevelYOffset);
                                                           break;
                                                       case ((int) AnnotationContainer.IconRelation.MID_LEFT_LOWER):
                                                           areaOrigin =
                                                               new Point(
                                                                   (int) individual.point.x - halfIndividualSize -
                                                                   horizontalFudge,
                                                                   (int) individual.point.y + verticalFudge -
                                                                   midLevelYOffset);
                                                           break;
                                                       case ((int) AnnotationContainer.IconRelation.UPPER_LEFT):
                                                           areaOrigin =
                                                               new Point(
                                                                   (int) individual.point.x - areaMargin -
                                                                   horizontalFudge,
                                                                   (int) individual.point.y - halfIndividualSize -
                                                                   areaMargin - (2*verticalFudge));
                                                           break;
                                                       case ((int) AnnotationContainer.IconRelation.UPPER_RIGHT):
                                                           areaOrigin =
                                                               new Point(
                                                                   (int) individual.point.x + areaMargin +
                                                                   horizontalFudge,
                                                                   (int) individual.point.y - halfIndividualSize -
                                                                   areaMargin - (2*verticalFudge));
                                                           break;
                                                       case ((int) AnnotationContainer.IconRelation.MID_RIGHT_UPPER):
                                                           areaOrigin =
                                                               new Point(
                                                                   (int) individual.point.x + halfIndividualSize +
                                                                   areaMargin + horizontalFudge,
                                                                   (int) individual.point.y - verticalFudge -
                                                                   midLevelYOffset);
                                                           break;
                                                       case ((int) AnnotationContainer.IconRelation.MID_RIGHT_LOWER):
                                                           areaOrigin =
                                                               new Point(
                                                                   (int) individual.point.x + halfIndividualSize +
                                                                   areaMargin + horizontalFudge,
                                                                   (int) individual.point.y + verticalFudge -
                                                                   midLevelYOffset);
                                                           break;
                                                       case ((int) AnnotationContainer.IconRelation.CENTER_TOP):
                                                           //if (individual.simpleIndividual.motherID > 0 && individual.simpleIndividual.fatherID > 0)
                                                           //    areaOrigin = new Point((int)individual.point.x, (int)individual.point.y - (4* halfIndividualSize));
                                                           //else
                                                           areaOrigin = new Point((int) individual.point.x,
                                                                                  (int) individual.point.y -
                                                                                  (2*halfIndividualSize));
                                                           break;
                                                   }
                                                   foreach (AnnotationContainerSlot slot in area.slots)
                                                   {
                                                       if (slot.resident != null)
                                                       {
                                                           switch (slot.resident.label)
                                                           {
                                                               case "Name":
                                                                   string renderedText = individual.HraPerson.name;
                                                                   //.Substring(0, model.parameters.nameWidth); ;
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       if (model.parameters.nameWidth > 0 &&
                                                                           renderedText.Length >
                                                                           model.parameters.nameWidth)
                                                                       {
                                                                           renderedText = renderedText.Substring(0,
                                                                                                                 model
                                                                                                                     .parameters
                                                                                                                     .nameWidth);
                                                                       }
                                                                       g.DrawString(renderedText,
                                                                                    individualFont,
                                                                                    Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "Name and Age":
                                                                   string name_part = individual.HraPerson.name;
                                                                   if (string.IsNullOrEmpty(name_part) == false)
                                                                   {
                                                                       if (model.parameters.nameWidth > 0 &&
                                                                           name_part.Length >
                                                                           model.parameters.nameWidth)
                                                                       {
                                                                           name_part = name_part.Substring(0, model.parameters.nameWidth);
                                                                       }
                                                                   }
                                                                   string age_part = individual.HraPerson.age;
                                                                   int i_age = -1;
                                                                   if (int.TryParse(age_part, out i_age))
                                                                   {
                                                                       if (i_age == 0)
                                                                       {
                                                                           DateTime dt_age;
                                                                           if (DateTime.TryParse(individual.HraPerson.dob, out dt_age))
                                                                           {
                                                                               TimeSpan ts = individual.HraPerson.owningFHx.proband.apptdatetime - dt_age;
                                                                               age_part = "~ " + Math.Round((ts.TotalDays / 30)).ToString() + " Mos";
                                                                           }
                                                                       }
                                                                   }
                                                                   renderedText = name_part + " " + age_part;
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    individualFont,
                                                                                    Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "Age":
                                                                   renderedText = individual.HraPerson.age;
                                                                   i_age = -1;
                                                                   if (int.TryParse(renderedText, out i_age))
                                                                   {
                                                                       if (i_age == 0)
                                                                       {
                                                                           DateTime dt_age;
                                                                           if (
                                                                               DateTime.TryParse(
                                                                                   individual.HraPerson.dob, out dt_age))
                                                                           {
                                                                               TimeSpan ts =
                                                                                   individual.HraPerson.owningFHx
                                                                                             .proband.apptdatetime -
                                                                                   dt_age;
                                                                               renderedText = "~ " +
                                                                                              Math.Round((ts.TotalDays/
                                                                                                          30))
                                                                                                  .ToString() + " Mos";
                                                                           }
                                                                       }
                                                                   }
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    individualFont,
                                                                                    Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "Date Of Birth":
                                                                   renderedText = individual.HraPerson.dob;
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    individualFont,
                                                                                    Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "First Name":
                                                                   renderedText = individual.HraPerson.firstName;
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    individualFont,
                                                                                    Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "Last Name":
                                                                   renderedText = individual.HraPerson.lastName;
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    individualFont,
                                                                                    Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "Comment":
                                                                   renderedText = individual.HraPerson.comment;
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    individualFont,
                                                                                    Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "Ethnicity":
                                                                   renderedText =
                                                                       individual.HraPerson.Ethnicity.GetSummary();
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       if (model.parameters.limitedEthnicity == false ||
                                                                           (individual.Mother == null &&
                                                                            individual.Father == null))
                                                                       {
                                                                           g.DrawString(renderedText,
                                                                                        individualFont,
                                                                                        Brushes.Black,
                                                                                        GetLabelPoint(areaOrigin,
                                                                                                      area.justification,
                                                                                                      renderedText));
                                                                           areaOrigin.Y +=
                                                                               (model.parameters.annotationFontSize + 4);
                                                                       }
                                                                   }
                                                                   break;
                                                               case "Nationality":
                                                                   renderedText =
                                                                       individual.HraPerson.Nationality.GetSummary();
                                                                   if (
                                                                       string.IsNullOrEmpty(
                                                                           individual.HraPerson.isAshkenazi) == false)
                                                                   {
                                                                       if (
                                                                           string.Compare(
                                                                               individual.HraPerson.isAshkenazi, "Yes",
                                                                               true) == 0)
                                                                           renderedText += ", Ashkenazi";
                                                                   }
                                                                   if (
                                                                       string.IsNullOrEmpty(
                                                                           individual.HraPerson.isHispanic) == false)
                                                                   {
                                                                       if (
                                                                           string.Compare(
                                                                               individual.HraPerson.isHispanic, "Yes",
                                                                               true) == 0)
                                                                           renderedText += ", Hispanic";
                                                                   }
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       renderedText = renderedText.Trim(trimChars);
                                                                       if (model.parameters.limitedNationality == false ||
                                                                           (individual.Mother == null &&
                                                                            individual.Father == null))
                                                                       {
                                                                           g.DrawString(renderedText,
                                                                                        individualFont,
                                                                                        Brushes.Black,
                                                                                        GetLabelPoint(areaOrigin,
                                                                                                      area.justification,
                                                                                                      renderedText));
                                                                           areaOrigin.Y +=
                                                                               (model.parameters.annotationFontSize + 4);
                                                                       }
                                                                   }
                                                                   break;
                                                               case "Accession #":
                                                                   renderedText = "";
                                                                   if (model.FamilialVariants != null)
                                                                   {
                                                                       lock (model.FamilialVariants)
                                                                       {
                                                                           foreach (
                                                                               GeneticTest gt in
                                                                                   individual.HraPerson.PMH.GeneticTests
                                                                               )
                                                                           {
                                                                               if (string.IsNullOrEmpty(gt.accession) ==
                                                                                   false)
                                                                               {
                                                                                   if (renderedText.Length > 0)
                                                                                   {
                                                                                       renderedText += ", ";
                                                                                   }
                                                                                   renderedText += gt.accession;
                                                                               }
                                                                               g.DrawString(renderedText,
                                                                                            individualFont,
                                                                                            Brushes.Black,
                                                                                            GetLabelPoint(areaOrigin,
                                                                                                          area
                                                                                                              .justification,
                                                                                                          renderedText));
                                                                               areaOrigin.Y += 12;
                                                                           }
                                                                       }
                                                                   }
                                                                   break;
                                                               case "Panel Name":
                                                                   //foreach (PanelData pd in individual.Panels)
                                                                   //{
                                                                   //    renderedText = pd.panelShortName;
                                                                   //    if (string.IsNullOrEmpty(renderedText) == false)
                                                                   //    {
                                                                   //        g.DrawString(renderedText,
                                                                   //                     individualFont,
                                                                   //                     Brushes.Black,
                                                                   //                     GetLabelPoint(areaOrigin, area.justification, renderedText));
                                                                   //        areaOrigin.Y += 12;
                                                                   //    }
                                                                   //}
                                                                   break;
                                                               case "Test Code":
                                                                   //foreach (PanelData pd in individual.Panels)
                                                                   //{
                                                                   //    renderedText = pd.TEST_CODE;
                                                                   //    if (string.IsNullOrEmpty(renderedText) == false)
                                                                   //    {
                                                                   //        g.DrawString(renderedText,
                                                                   //                     individualFont,
                                                                   //                     Brushes.Black,
                                                                   //                     GetLabelPoint(areaOrigin, area.justification, renderedText));
                                                                   //        areaOrigin.Y += 12;
                                                                   //    }
                                                                   //}
                                                                   break;
                                                               case "Test Result":
                                                                   if (model.FamilialVariants != null)
                                                                   {
                                                                       lock (model.FamilialVariants)
                                                                       {
                                                                           foreach (
                                                                               GeneticTest gt in
                                                                                   individual.HraPerson.PMH.GeneticTests
                                                                                             .Where(
                                                                                                 t =>
                                                                                                 (((GeneticTest) t)
                                                                                                      .GeneticTest_status !=
                                                                                                  "Pending") &&
                                                                                                 ((GeneticTest) t)
                                                                                                     .GeneticTest_status !=
                                                                                                 "Cancelled" &&
                                                                                                 (((GeneticTest) t)
                                                                                                      .GeneticTest_panelID !=
                                                                                                  26)))
                                                                           {
                                                                               bool allNegative = true;
                                                                               foreach (
                                                                                   GeneticTestResult gtr in
                                                                                       gt.GeneticTestResults)
                                                                               {
                                                                                   if (
                                                                                       string.IsNullOrEmpty(
                                                                                           gtr.resultSignificance) ==
                                                                                       false)
                                                                                   {
                                                                                       if (
                                                                                           !string.IsNullOrEmpty(
                                                                                               gtr.mutationName) ||
                                                                                           !((gtr.resultSignificance ==
                                                                                              "Negative") ||
                                                                                             (gtr.resultSignificance ==
                                                                                              "NEG") ||
                                                                                             (gtr.resultSignificance ==
                                                                                              "Benign")))
                                                                                       {
                                                                                           allNegative = false;
                                                                                       }
                                                                                   }
                                                                               }
                                                                               if (allNegative)
                                                                               {
                                                                                   if (
                                                                                       string.IsNullOrEmpty(
                                                                                           gt.panelShortName))
                                                                                   {
                                                                                       renderedText = gt.panelName +
                                                                                                      " -";
                                                                                   }
                                                                                   else
                                                                                   {
                                                                                       renderedText =
                                                                                           gt.panelShortName + " -";
                                                                                   }
                                                                                   if (
                                                                                       string.IsNullOrEmpty(renderedText) ==
                                                                                       false)
                                                                                   {
                                                                                       g.DrawString(renderedText,
                                                                                                    individualFont,
                                                                                                    Brushes.Black,
                                                                                                    GetLabelPoint(
                                                                                                        areaOrigin,
                                                                                                        area
                                                                                                            .justification,
                                                                                                        renderedText));
                                                                                       areaOrigin.Y += 12;
                                                                                   }
                                                                               }
                                                                           }
                                                                       }
                                                                   }
                                                                   break;
                                                               case "Variant":
                                                                   if (model.FamilialVariants != null)
                                                                   {
                                                                       lock (model.FamilialVariants)
                                                                       {
                                                                           if (individual.HraPerson.PMH.GeneticTests.Count > 0)
                                                                           {
                                                                               foreach (GeneticTestResult familialVariant in model.FamilialVariants.Keys)
                                                                               {

                                                                                   if (string.Compare(familialVariant.resultSignificance, "Negative", true) == 0 ||
                                                                                   string.Compare(familialVariant.resultSignificance, "Neg", true) == 0)
                                                                                   {
                                                                                       continue;
                                                                                   }

                                                                                   renderedText = "";
                                                                                   foreach (GeneticTest gt in individual.HraPerson.PMH.GeneticTests.Where (t =>((GeneticTest) t).GeneticTest_status != "Pending" &&
                                                                                                                                                            ((GeneticTest) t).GeneticTest_status != "Cancelled"))
                                                                                   {
                                                                                       foreach (GeneticTestResult individualResult in gt.GeneticTestResults)
                                                                                       {
                                                                                           if (model.FamilialVariants[familialVariant].Any(person => person.relativeID == individual.HraPerson.relativeID))
                                                                                           {
                                                                                               if (familialVariant.geneName == individualResult.geneName)
                                                                                               {
                                                                                                   if (familialVariant.mutationName == individualResult.ASOMutationName)
                                                                                                   {
                                                                                                       if (individualResult.ASOResult == "Found")
                                                                                                       {
                                                                                                           renderedText = model.parameters.VariantFoundText;
                                                                                                       }
                                                                                                       else if (individualResult.ASOResult == "Not Found")
                                                                                                       {
                                                                                                           renderedText = model.parameters.VariantNotFoundText;
                                                                                                       }
                                                                                                       else if (individualResult.ASOResult == "Not Tested")
                                                                                                       {
                                                                                                           renderedText = model.parameters.VariantNotTestedText;
                                                                                                       }
                                                                                                       else if (individualResult.ASOResult == "Unknown")
                                                                                                       {
                                                                                                           renderedText = model.parameters.VariantUnknownText;
                                                                                                       }
                                                                                                   }
                                                                                                   else if (familialVariant.mutationName == individualResult.mutationName)
                                                                                                   {
                                                                                                       renderedText = model.parameters.VariantFoundText;
                                                                                                   }
                                                                                                   if (renderedText == model.parameters.VariantFoundText)
                                                                                                   {
                                                                                                       if (string.IsNullOrEmpty(familialVariant.GeneticTestResult_allelicState) == false)
                                                                                                       {
                                                                                                           if (string.Compare(familialVariant.GeneticTestResult_allelicState, "Hemizygous", true) == 0 ||
                                                                                                               string.Compare(familialVariant.GeneticTestResult_allelicState, "Heterozygous", true) == 0)
                                                                                                           {
                                                                                                               renderedText = model.parameters.VariantHeteroText;
                                                                                                           }
                                                                                                       }
                                                                                                       string meaning = individualResult.getMeaning();
                                                                                                       if (meaning == "VUS")
                                                                                                       {
                                                                                                           renderedText = model.parameters.VariantFoundVusText;
                                                                                                       }
                                                                                                       else if (meaning == "Unknown")
                                                                                                       {
                                                                                                           renderedText = model.parameters.VariantUnknownText;
                                                                                                       }
                                                                                                       else if (
                                                                                                           meaning == "Favor Polymorphism" || meaning == "Negative")
                                                                                                       {
                                                                                                           renderedText
                                                                                                               =
                                                                                                               model
                                                                                                                   .parameters
                                                                                                                   .VariantNotFoundText;
                                                                                                       }
                                                                                                   }
                                                                                               }
                                                                                           }
                                                                                           else
                                                                                           {
                                                                                               if (!string.IsNullOrEmpty(individualResult.mutationName))
                                                                                               {
                                                                                                   renderedText = model.parameters.VariantNotTestedText;
                                                                                               }
                                                                                           }
                                                                                           
                                                                                       }
                                                                                   }
                                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                                   {
                                                                                       g.DrawString(renderedText,individualFont,Brushes.Black,GetLabelPoint(areaOrigin, area.justification, renderedText));
                                                                                       areaOrigin.Y += 12;
                                                                                   }
                                                                               }
                                                                           }
                                                                       }
                                                                   }
                                                                   break;

                                                               case "Disease List":
                                                                   foreach (
                                                                       ClincalObservation co in
                                                                           individual.HraPerson.PMH.Observations)
                                                                   {
                                                                       string shortString =
                                                                           co.ClinicalObservation_diseaseShortName;
                                                                       if (string.IsNullOrEmpty(shortString))
                                                                           shortString = co.disease;
                                                                       if (string.IsNullOrEmpty(shortString))
                                                                           shortString = co.ClinicalObservation_Problem;

                                                                       if (string.IsNullOrEmpty(co.ageDiagnosis) ==
                                                                           false)
                                                                           shortString += (" " + co.ageDiagnosis);

                                                                       if (string.IsNullOrEmpty(shortString) == false)
                                                                       {
                                                                           g.DrawString(shortString, individualFont,
                                                                                        Brushes.Black,
                                                                                        GetLabelPoint(areaOrigin,
                                                                                                      area.justification,
                                                                                                      shortString));
                                                                           areaOrigin.Y +=
                                                                               (model.parameters.annotationFontSize + 4);
                                                                       }
                                                                   }
                                                                   break;

                                                               case "Dx + Comment":
                                                                   foreach (
                                                                       ClincalObservation co in
                                                                           individual.HraPerson.PMH.Observations)
                                                                   {
                                                                       string shortString =
                                                                           co.ClinicalObservation_diseaseShortName;
                                                                       if (string.IsNullOrEmpty(shortString))
                                                                           shortString = co.disease;
                                                                       if (string.IsNullOrEmpty(shortString))
                                                                           shortString = co.ClinicalObservation_Problem;

                                                                       if (string.IsNullOrEmpty(co.ageDiagnosis) == false)
                                                                           shortString += (" " + co.ageDiagnosis);

                                                                       if (string.IsNullOrEmpty(co.comments) == false)
                                                                           shortString += (" " + co.comments);

                                                                       if (string.IsNullOrEmpty(shortString) == false)
                                                                       {
                                                                           g.DrawString(shortString, individualFont,
                                                                                        Brushes.Black,
                                                                                        GetLabelPoint(areaOrigin,
                                                                                                      area.justification,
                                                                                                      shortString));
                                                                           areaOrigin.Y +=
                                                                               (model.parameters.annotationFontSize + 4);
                                                                       }
                                                                   }
                                                                   break;

                                                               case "Brca Score":
                                                                   double score =
                                                                       Math.Round(
                                                                           individual.HraPerson.RP
                                                                                     .RiskProfile_BrcaPro_1_2_Mut_Prob ??
                                                                           -1, 0);
                                                                   renderedText = (score >= 1)
                                                                                      ? String.Format("{0:0}", score) +
                                                                                        "%"
                                                                                      : "";
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    new Font(individualFont,
                                                                                             FontStyle.Bold),
                                                                                    (score >= 10)
                                                                                        ? Brushes.Red
                                                                                        : Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText,
                                                                                                  FontStyle.Bold, true,
                                                                                                  g));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "MMR Score":
                                                                   double mmrscore =
                                                                       Math.Round(
                                                                           individual.HraPerson.RP
                                                                                     .RiskProfile_MmrPro_1_2_6_Mut_Prob ??
                                                                           -1, 0);
                                                                   renderedText = (mmrscore >= 1)
                                                                                      ? String.Format("{0:0}", mmrscore) +
                                                                                        "%"
                                                                                      : "";
                                                                   if (string.IsNullOrEmpty(renderedText) == false)
                                                                   {
                                                                       g.DrawString(renderedText,
                                                                                    new Font(individualFont,
                                                                                             FontStyle.Bold),
                                                                                    (mmrscore >= 10)
                                                                                        ? Brushes.Red
                                                                                        : Brushes.Black,
                                                                                    GetLabelPoint(areaOrigin,
                                                                                                  area.justification,
                                                                                                  renderedText,
                                                                                                  FontStyle.Bold, true,
                                                                                                  g));
                                                                       areaOrigin.Y +=
                                                                           (model.parameters.annotationFontSize + 4);
                                                                   }
                                                                   break;
                                                               case "Relationship":
                                                                   //only show relationship for unlinked relatives
                                                                   if (individual != null &&
                                                                       individual.HraPerson != null)
                                                                   {
                                                                       renderedText =
                                                                           individual.HraPerson.GetShortRelationship();

                                                                       if (string.IsNullOrEmpty(renderedText) == false)
                                                                       {
                                                                           renderedText = renderedText.Trim(trimChars);
                                                                           if (individual.hasChildren() == false &&
                                                                               individual.hasParent() == false)
                                                                           {
                                                                               g.DrawString(renderedText,
                                                                                            individualFont,
                                                                                            Brushes.Black,
                                                                                            GetLabelPoint(areaOrigin,
                                                                                                          area
                                                                                                              .justification,
                                                                                                          renderedText));
                                                                               areaOrigin.Y +=
                                                                                   (model.parameters.annotationFontSize +
                                                                                    4);
                                                                           }
                                                                       }
                                                                   }


                                                                   break;
                                                               default:
                                                                   break;
                                                           }

                                                           //Size sz = TextRenderer.MeasureText(renderedText, individualFont);
                                                           //g.FillRectangle(new SolidBrush(Color.FromArgb(128, 255, 255, 255)), areaOrigin.X, areaOrigin.Y, sz.Width, sz.Height);
                                                       }
                                                   }
                                               }
                                           }
                                       ////////////////////////////////////////////////////////////////////////////////

                                       if (individual.Selected)
                                       {
                                           Rectangle r = new Rectangle(tempRect.X - 5, tempRect.Y - 5,
                                                                       tempRect.Width + 10, tempRect.Height + 10);
                                           //g.DrawEllipse(selectionPen, tempRect.X - 5, tempRect.Y - 5, tempRect.Width + 10, tempRect.Height + 10);
                                           if (model.Selected.Count > 1)
                                           {
                                               DrawIndividualGlyph(g, multiSelectionPen, r, individual.HraPerson.gender);
                                           }
                                           else
                                           {
                                               DrawIndividualGlyph(g, selectionPen, r, individual.HraPerson.gender);
                                           }
                                       }
                                       //else

                                       {
                                           DrawIndividualGlyph(g, individualsPen, tempRect, individual.HraPerson.gender);
                                       }

                                       if (individual.HraPerson.adopted == "Yes")
                                       {
                                           adoptedLeftPoints[0] = new Point(tempRect.X + tempRect.Width/5,
                                                                            tempRect.Y - 8);
                                           adoptedLeftPoints[1] = new Point(tempRect.X - 8, tempRect.Y - 8);
                                           adoptedLeftPoints[2] = new Point(tempRect.X - 8,
                                                                            tempRect.Y + tempRect.Height + 8);
                                           adoptedLeftPoints[3] = new Point(tempRect.X + tempRect.Width/5,
                                                                            tempRect.Y + tempRect.Height + 8);
                                           g.DrawLines(Pens.Black, adoptedLeftPoints);

                                           adoptedRightPoints[0] =
                                               new Point(tempRect.X + tempRect.Width - tempRect.Width/5, tempRect.Y - 8);
                                           adoptedRightPoints[1] = new Point(tempRect.X + tempRect.Width + 8,
                                                                             tempRect.Y - 8);
                                           adoptedRightPoints[2] = new Point(tempRect.X + tempRect.Width + 8,
                                                                             tempRect.Y + tempRect.Height + 8);
                                           adoptedRightPoints[3] =
                                               new Point(tempRect.X + tempRect.Width - tempRect.Width/5,
                                                         tempRect.Y + tempRect.Height + 8);
                                           g.DrawLines(Pens.Black, adoptedRightPoints);
                                       }

                                       ////////////////////////////////////////////////////////////////////////////////
                                       if (individual.Group != null)
                                       {
                                           string num = individual.Group.Count.ToString();
                                           Size sz = TextRenderer.MeasureText(num, individualFont);

                                           g.DrawString(num, individualFont, Brushes.Black,
                                                        (float) (individual.point.x - (sz.Width/2)),
                                                        (float) (individual.point.y - (sz.Height/2)));
                                       }

                                       ////////////////////////////////////////////////////////////////////////////////
                                       if (individual.HraPerson.vitalStatus == "Dead")
                                       {
                                           g.DrawLine(individualsPen,
                                                      new Point(tempRect.X - 5, tempRect.Y - 5),
                                                      new Point(tempRect.X + tempRect.Width + 5,
                                                                tempRect.Y + tempRect.Height + 5));
                                       }
                                       ////////////////////////////////////////////////////////////////////////////////
                                       //int count = model.getIndividualCountUnderContact(individual);
                                       //if (count > 1)
                                       //{
                                       //    g.DrawString(count.ToString(), individualFont, Brushes.Black, new PointF((int)individual.point.x - 5, (int)individual.point.y - 5));
                                       //}

                                       int x = (int) (individual.point.x - 7);
                                       int y = (int) (individual.point.y - 9);

                                       if (model.parameters.drawIdLabels)
                                           g.DrawString(individual.HraPerson.relativeID.ToString(), idLabelFont,
                                                        Brushes.Black, x, y);
                                   } /**/
                               }
                           }
                           catch (Exception ex)
                           {
                               Logger.Instance.WriteToLog(ex.ToString());
                               g.DrawString(ex.ToString(), idLabelFont, Brushes.Black, 10, 10);
                           }
                       };
        }

        private void DrawIndividualGlyph(Graphics g, Pen pen, Rectangle tempRect, string gender)
        {
            if (gender == PedigreeIndividual.GENDER_FEMALE)
                g.DrawEllipse(pen, tempRect);
            else if (gender == PedigreeIndividual.GENDER_MALE)
                g.DrawRectangle(pen, tempRect);
            else
            {
                unknownGenderPoints[0] = new Point(tempRect.X + tempRect.Width/2, tempRect.Y);
                unknownGenderPoints[1] = new Point(tempRect.X + tempRect.Width, tempRect.Y + tempRect.Height/2);
                unknownGenderPoints[2] = new Point(tempRect.X + tempRect.Width/2, tempRect.Y + tempRect.Height);
                unknownGenderPoints[3] = new Point(tempRect.X, tempRect.Y + tempRect.Height/2);
                g.DrawPolygon(pen, unknownGenderPoints);
            }
        }

        private static PointF GetLabelPoint(Point areaOrigin, int p, string renderedText)
        {
            Font individualFont = new Font("Tahoma", 10);
            PointF retval = new PointF(areaOrigin.X, areaOrigin.Y);
            Size s = TextRenderer.MeasureText(renderedText, individualFont);

            switch (p)
            {
                case ((int) AnnotationContainer.Alignements.LEFT_JUSTIFIED):
                    break;
                case ((int) AnnotationContainer.Alignements.CENTER_JUSTIFIED):
                    retval.X -= (s.Width/2);
                    break;
                case ((int) AnnotationContainer.Alignements.RIGHT_JUSTIFIED):
                    retval.X -= (s.Width);
                    break;
            }
            return retval;
        }

        private static PointF GetLabelPoint(Point areaOrigin, int p, string renderedText, FontStyle fStyle,
                                            bool drawWhiteBackground, Graphics g)
        {
            Font individualFont = new Font("Tahoma", 10, fStyle);
            PointF retval = new PointF(areaOrigin.X, areaOrigin.Y);
            Size s = TextRenderer.MeasureText(renderedText, individualFont);

            if (drawWhiteBackground)
            {
                g.FillRectangle(Brushes.White, retval.X, retval.Y, s.Width, s.Height);
            }

            switch (p)
            {
                case ((int) AnnotationContainer.Alignements.LEFT_JUSTIFIED):
                    break;
                case ((int) AnnotationContainer.Alignements.CENTER_JUSTIFIED):
                    retval.X -= (s.Width/2);
                    break;
                case ((int) AnnotationContainer.Alignements.RIGHT_JUSTIFIED):
                    retval.X -= (s.Width);
                    break;
            }
            return retval;
        }
    }
}

/*int halfIndividualSize = model.parameters.individualSize / 2;
                foreach (PedigreeIndividual individual in model.individuals)
                {
                    tempRect.X = (int)individual.point.x - halfIndividualSize;
                    tempRect.Y = (int)individual.point.y - halfIndividualSize;
                    tempRect.Width = tempRect.Height = model.parameters.individualSize;

                    //highlight moved individuals in red for visual debugging
                    //Brush brush = individual.wasMoved ? Brushes.Red : Brushes.White;
                    Brush brush = Brushes.White;

                    if (individual.gender == Person.GENDER_FEMALE)
                    {
                        g.FillEllipse(brush, tempRect);
                        g.DrawEllipse(individualsPen, tempRect);
                    }
                    else
                    {
                        g.FillRectangle(brush, tempRect);
                        g.DrawRectangle(individualsPen, tempRect);
                    }
                    if (model.parameters.drawIdLabels)
                    {
                        int x = (int)(individual.point.x - 7);
                        int y = (int)(individual.point.y - 9);

                        g.DrawString("" + individual.relativeID, idLabelFont, Brushes.Black, x, y);
                        //g.DrawString("" + individual.leftEdge.ToString() + ", " + individual.rightEdge.ToString(), idLabelFont, Brushes.Black, x, y);
                    }
                }

                
                //Font idLabelFont = new Font("Tahoma", 12);
                int sCount = 0;
                for (int generationalLevel = 0;
                    generationalLevel < model.individualSets.Count;
                    generationalLevel++)
                {
                    //sort the individual sets of the current generation
                    List<PedigreeIndividualSet> levelIndividualSets = model.individualSets[generationalLevel];
                    levelIndividualSets.Sort(delegate(PedigreeIndividualSet a, PedigreeIndividualSet b)
                    {
                        double ax = a.associatedCouple.point.x;
                        double bx = b.associatedCouple.point.x;
                        return ax.CompareTo(bx);
                    });

                    foreach (PedigreeIndividualSet s in levelIndividualSets)
                    {
                        sCount++;
                        foreach (PedigreeIndividual p in s)
                        {
                            g.DrawString(sCount.ToString(), idLabelFont, Brushes.Green, (int)(p.point.x), (int)(p.point.y));
                        }
                    }
                }

              


            };
        }
    }
}

         
         */