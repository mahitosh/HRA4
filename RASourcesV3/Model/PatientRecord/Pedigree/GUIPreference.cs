using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class GUIPreference : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public Patient owningPatient;

        TypeConverter converter = TypeDescriptor.GetConverter(typeof(Font));

        /**************************************************************************************************/
        [DataMember] [HraAttribute (auditable=false)] protected DateTime modifiedDate;  // audit ??
        [DataMember] [HraAttribute (auditable=false)] protected string formName = "";      // audit ??
        [DataMember] [HraAttribute (auditable=false)] protected string parentName = "";  // audit ??
        [DataMember] [HraAttribute (auditable=false)] protected int width = 625;                        // audit ??
        [DataMember] [HraAttribute (auditable=false)] protected int height = 625;                       // audit ??
        [DataMember] [HraAttribute (auditable=false)] protected int pedigreeZoomValue = 90;
        [DataMember] [HraAttribute (auditable=false)] protected int pedigreeVerticalSpacing = 135;

        [DataMember] [Hra (auditable=false)] protected bool ShowRelIds = false;
        [DataMember] [Hra (auditable=false)] protected string PedigreeBackground = "White";
        [DataMember] [Hra (auditable=false)] protected int nameWidth = 8;
        [DataMember] [Hra (auditable=false)] protected bool limitedEthnicity = false;
        [DataMember] [Hra (auditable=false)] protected bool limitedNationality = true;
        [DataMember] [Hra (auditable=false)] protected bool ShowTitle = true;
        [DataMember] [Hra (auditable=false)] protected bool ShowName = true; 
        [DataMember] [Hra (auditable=false)] protected string NameFont = "Tahoma, 8.25pt, style=Bold";
        [DataMember] [Hra (auditable=false)] protected bool ShowUnitnum = true;
        [DataMember] [Hra (auditable=false)] protected string UnitnumFont = "Tahoma, 8.25pt";
        [DataMember] [Hra (auditable=false)] protected bool ShowDob = true;
        [DataMember] [Hra (auditable=false)] protected string DobFont = "Tahoma, 8.25pt";
        [DataMember] [Hra (auditable=false)] protected int TitleSpacing = 5;
        [DataMember] [Hra (auditable=false)] protected string TitleBackground = "White";
        [DataMember] [Hra (auditable=false)] protected string TitleBorder = "Single";
        [DataMember] [Hra (auditable=false)] protected bool ShowLegend = true;
        [DataMember] [Hra (auditable=false)] protected string LegendBackground = "White";
        [DataMember] [Hra (auditable=false)] protected string LegendBorder = "Single";
        [DataMember] [Hra (auditable=false)] protected int LegendRadius = 8;
        [DataMember] [Hra (auditable=false)] protected string LegendFont = "Tahoma, 8pt";
        [DataMember] [Hra (auditable=false)] protected bool ShowComment = false;
        [DataMember] [Hra (auditable=false)] protected string CommentBackground = "White";
        [DataMember] [Hra (auditable=false)] protected string CommentBorder = "Single";
        [DataMember] [Hra (auditable=false)] protected string CommentFont = "Tahoma, 10pt";

        [DataMember] [HraAttribute (auditable=false)] protected int LegendX = 220;
        [DataMember] [HraAttribute (auditable=false)] protected int LegendY = 30;
        [DataMember] [HraAttribute (auditable=false)] protected int LegendHeight = 86;
        [DataMember] [HraAttribute (auditable=false)] protected int LegendWidth = 933;
        [DataMember] [HraAttribute (auditable=false)] protected int TitleX = 30;
        [DataMember] [HraAttribute (auditable=false)] protected int TitleY = 30;
        [DataMember] [HraAttribute (auditable=false)] protected int TitleHeight = 86;
        [DataMember] [HraAttribute (auditable=false)] protected int TitleWidth = 170;
        [DataMember] [HraAttribute (auditable=false)] protected int CommentX = 30;
        [DataMember] [HraAttribute (auditable=false)] protected int CommentY = 300;
        [DataMember] [HraAttribute (auditable=false)] protected int CommentHeight = 150;
        [DataMember] [HraAttribute (auditable=false)] protected int CommentWidth = 300;

        [DataMember] [Hra (auditable=false)] public string VariantFoundText = "+";
        [DataMember] [Hra (auditable=false)] public string VariantFoundVusText = "vus";
        [DataMember] [Hra (auditable=false)] public string VariantNotFoundText = "-";
        [DataMember] [Hra (auditable=false)] public string VariantUnknownText = "?";
        [DataMember] [Hra (auditable=false)] public string VariantNotTestedText = "NT";
        [DataMember] [Hra (auditable=false)] public string VariantHeteroText = "+/-";

        [DataMember] [Hra (auditable=false)] public bool hideNonBloodRelatives = false;

        [DataMember] public PedigreeAnnotationList annotations;

        //[DataMember] [HraAttribute (auditable=false)] protected int xOffset;
        //[DataMember] [HraAttribute (auditable=false)] protected int yOffset;
        #region getters_setters
        /*****************************************************/
        public bool GUIPreference_hideNonBloodRelatives
        {
            get
            {
                return hideNonBloodRelatives;
            }
            set
            {
                if (value != hideNonBloodRelatives)
                {
                    hideNonBloodRelatives = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hideNonBloodRelatives"));
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public DateTime GUIPreference_modifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                if (value != modifiedDate)
                {
                    modifiedDate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("modifiedDate"));
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    SignalModelChanged(args);
                }
            }
        } 
        /*****************************************************/
        public string GUIPreference_formName
        {
            get
            {
                return formName;
            }
            set
            {
                if (value != formName)
                {
                    formName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    //extra updated members added due to Primary Key including more than just unitnum
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GUIPreference_parentName
        {
            get
            {
                return parentName;
            }
            set
            {
                if (value != parentName)
                {
                    parentName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    //extra updated members added due to Primary Key including more than just unitnum
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_width
        {
            get
            {
                return width;
            }
            set
            {
                if (value != width)
                {
                    width = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    //extra updated members added due to Primary Key including more than just unitnum
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_height
        {
            get
            {
                return height;
            }
            set
            {
                if (value != height)
                {
                    height = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    //extra updated members added due to Primary Key including more than just unitnum
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_zoomValue
        {
            get
            {
                return pedigreeZoomValue;
            }
            set
            {
                if (value != pedigreeZoomValue)
                {
                    pedigreeZoomValue = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    //extra updated members added due to Primary Key including more than just unitnum
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("pedigreeZoomValue"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_verticalSpacing
        {
            get
            {
                return pedigreeVerticalSpacing;
            }
            set
            {
                if (value != pedigreeVerticalSpacing)
                {
                    pedigreeVerticalSpacing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    //extra updated members added due to Primary Key including more than just unitnum
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("pedigreeVerticalSpacing"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_ShowRelIds
        {
            get
            {
                return ShowRelIds;
            }
            set
            {
                if (value != ShowRelIds)
                {
                    ShowRelIds = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("ShowRelIds"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Color GUIPreference_PedigreeBackground
        {
            get
            {
                if (PedigreeBackground != null)
                    return ColorTranslator.FromHtml(PedigreeBackground);
                else
                    return Color.White;
            }
            set
            {
                string s = ColorTranslator.ToHtml(value);
                if (s != PedigreeBackground)
                {
                    PedigreeBackground = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("PedigreeBackground"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_nameWidth
        {
            get
            {
                return nameWidth;
            }
            set
            {
                if (value != nameWidth)
                {
                    nameWidth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("nameWidth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_limitedEthnicity
        {
            get
            {
                return limitedEthnicity;
            }
            set
            {
                if (value != limitedEthnicity)
                {
                    limitedEthnicity = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("limitedEthnicity"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_ShowTitle
        {
            get
            {
                return ShowTitle;
            }
            set
            {
                if (value != ShowTitle)
                {
                    ShowTitle = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("ShowTitle"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_ShowName
        {
            get
            {
                return ShowName;
            }
            set
            {
                if (value != ShowName)
                {
                    ShowName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("ShowName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Font GUIPreference_NameFont
        {
            get
            {
                return (Font)converter.ConvertFromString(NameFont);
            }
            set
            {
                string s = converter.ConvertToString(value);
                if (s != NameFont)
                {
                    NameFont = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("NameFont"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_ShowUnitnum
        {
            get
            {
                return ShowUnitnum;
            }
            set
            {
                if (value != ShowUnitnum)
                {
                    ShowUnitnum = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("ShowUnitnum"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Font GUIPreference_UnitnumFont
        {
            get
            {
                return (Font)converter.ConvertFromString(UnitnumFont);
            }
            set
            {
                string s = converter.ConvertToString(value);
                if (s != UnitnumFont)
                {
                    UnitnumFont = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("UnitnumFont"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_ShowDob
        {
            get
            {
                return ShowDob;
            }
            set
            {
                if (value != ShowDob)
                {
                    ShowDob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("ShowDob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Font GUIPreference_DobFont
        {
            get
            {
                return (Font)converter.ConvertFromString(DobFont);
            }
            set
            {
                string s = converter.ConvertToString(value);
                if (s != DobFont)
                {
                    DobFont = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("DobFont"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_TitleSpacing
        {
            get
            {
                return TitleSpacing;
            }
            set
            {
                if (value != TitleSpacing)
                {
                    TitleSpacing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("TitleSpacing"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Color GUIPreference_TitleBackground
        {
            get
            {
                return ColorTranslator.FromHtml(TitleBackground);
            }
            set
            {
                string s = ColorTranslator.ToHtml(value);
                if (s != TitleBackground)
                {
                    TitleBackground = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("TitleBackground"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public BorderStyle GUIPreference_TitleBorder
        {
            get
            {
                switch (TitleBorder)
                {
                    case "Single":
                        return BorderStyle.FixedSingle;
                    case "None":
                        return BorderStyle.None;
                    case "3D":
                        return BorderStyle.Fixed3D;
                    default:
                        return BorderStyle.None;
                }
            }
            set
            {
                string s = "None";
                if (value == BorderStyle.FixedSingle)
                    s = "Single";
                if (value == BorderStyle.Fixed3D)
                    s = "3D";

                if (s != TitleBorder)
                {
                    TitleBorder = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("TitleBorder"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_ShowLegend
        {
            get
            {
                return ShowLegend;
            }
            set
            {
                if (value != ShowLegend)
                {
                    ShowLegend = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("ShowLegend"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Color GUIPreference_LegendBackground
        {
            get
            {
                return ColorTranslator.FromHtml(LegendBackground);
            }
            set
            {
                string s = ColorTranslator.ToHtml(value);
                if (s != LegendBackground)
                {
                    LegendBackground = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("LegendBackground"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public BorderStyle GUIPreference_LegendBorder
        {
            get
            {
                switch (LegendBorder)
                {
                    case "Single":
                        return BorderStyle.FixedSingle;
                    case "None":
                        return BorderStyle.None;
                    case "3D":
                        return BorderStyle.Fixed3D;
                    default:
                        return BorderStyle.None;
                }
            }
            set
            {
                string s = "None";
                if (value == BorderStyle.FixedSingle)
                    s = "Single";
                if (value == BorderStyle.Fixed3D)
                    s = "3D";
                if (s != LegendBorder)
                {
                    LegendBorder = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("LegendBorder"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_LegendRadius
        {
            get
            {
                return LegendRadius;
            }
            set
            {
                if (value != LegendRadius)
                {
                    LegendRadius = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("LegendRadius"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Font GUIPreference_LegendFont
        {
            get
            {
                return (Font)converter.ConvertFromString(LegendFont);
            }
            set
            {
                string s = converter.ConvertToString(value);
                if (s != LegendFont)
                {
                    LegendFont = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("LegendFont"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_ShowComment
        {
            get
            {
                return ShowComment;
            }
            set
            {
                if (value != ShowComment)
                {
                    ShowComment = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("ShowComment"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Color GUIPreference_CommentBackground
        {
            get
            {
                return ColorTranslator.FromHtml(CommentBackground);
            }
            set
            {
                string s = ColorTranslator.ToHtml(value);
                if (s != CommentBackground)
                {
                    CommentBackground = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("CommentBackground"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public BorderStyle GUIPreference_CommentBorder
        {
            get
            {
                switch (CommentBorder)
                {
                    case "Single":
                        return BorderStyle.FixedSingle;
                    case "None":
                        return BorderStyle.None;
                    case "3D":
                        return BorderStyle.Fixed3D;
                    default:
                        return BorderStyle.None;
                }
            }
            set
            {
                string s = "None";
                if (value == BorderStyle.FixedSingle)
                    s = "Single";
                if (value == BorderStyle.Fixed3D)
                    s = "3D";
                if (s != CommentBorder)
                {
                    CommentBorder = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("CommentBorder"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public Font GUIPreference_CommentFont
        {
            get
            {
                return (Font)converter.ConvertFromString(CommentFont);
            }
            set
            {
                string s = converter.ConvertToString(value);
                if (s != CommentFont)
                {
                    CommentFont = s;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("CommentFont"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_LegendX
        {
            get
            {
                return LegendX;
            }
            set
            {
                if (value != LegendX)
                {
                    LegendX = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("LegendX"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_LegendY
        {
            get
            {
                return LegendY;
            }
            set
            {
                if (value != LegendY)
                {
                    LegendY = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("LegendY"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_LegendHeight
        {
            get
            {
                return LegendHeight;
            }
            set
            {
                if (value != LegendHeight)
                {
                    LegendHeight = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("LegendHeight"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_LegendWidth
        {
            get
            {
                return LegendWidth;
            }
            set
            {
                if (value != LegendWidth)
                {
                    LegendWidth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("LegendWidth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_TitleX
        {
            get
            {
                return TitleX;
            }
            set
            {
                if (value != TitleX)
                {
                    TitleX = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("TitleX"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_TitleY
        {
            get
            {
                return TitleY;
            }
            set
            {
                if (value != TitleY)
                {
                    TitleY = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("TitleY"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_TitleHeight
        {
            get
            {
                return TitleHeight;
            }
            set
            {
                if (value != TitleHeight)
                {
                    TitleHeight = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("TitleHeight"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_TitleWidth
        {
            get
            {
                return TitleWidth;
            }
            set
            {
                if (value != TitleWidth)
                {
                    TitleWidth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("TitleWidth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_CommentX
        {
            get
            {
                return CommentX;
            }
            set
            {
                if (value != CommentX)
                {
                    CommentX = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("CommentX"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_CommentY
        {
            get
            {
                return CommentY;
            }
            set
            {
                if (value != CommentY)
                {
                    CommentY = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("CommentY"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_CommentHeight
        {
            get
            {
                return CommentHeight;
            }
            set
            {
                if (value != CommentHeight)
                {
                    CommentHeight = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height")); 
                    args.updatedMembers.Add(GetMemberByName("CommentHeight"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int GUIPreference_CommentWidth
        {
            get
            {
                return CommentWidth;
            }
            set
            {
                if (value != CommentWidth)
                {
                    CommentWidth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("CommentWidth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool GUIPreference_limitedNationality
        {
            get
            {
                return limitedNationality;
            }
            set
            {
                if (value != limitedNationality)
                {
                    limitedNationality = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("limitedNationality"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GUIPreference_VariantFoundText
        {
            get
            {
                return VariantFoundText;
            }
            set
            {
                if (value != VariantFoundText)
                {
                    VariantFoundText = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("VariantFoundText"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GUIPreference_VariantNotFoundText
        {
            get
            {
                return VariantNotFoundText;
            }
            set
            {
                if (value != VariantNotFoundText)
                {
                    VariantNotFoundText = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("VariantNotFoundText"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GUIPreference_VariantUnknownText
        {
            get
            {
                return VariantUnknownText;
            }
            set
            {
                if (value != VariantUnknownText)
                {
                    VariantUnknownText = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("VariantUnknownText"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GUIPreference_VariantNotTestedText
        {
            get
            {
                return VariantNotTestedText;
            }
            set
            {
                if (value != VariantNotTestedText)
                {
                    VariantNotTestedText = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("VariantNotTestedText"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GUIPreference_VariantHeteroText
        {
            get
            {
                return VariantHeteroText;
            }
            set
            {
                if (value != VariantHeteroText)
                {
                    VariantHeteroText = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("VariantHeteroText"));
                    SignalModelChanged(args);
                }
            }
        } 
        /*****************************************************/
        public string GUIPreference_VariantFoundVusText
        {
            get
            {
                return VariantFoundVusText;
            }
            set
            {
                if (value != VariantFoundVusText)
                {
                    VariantFoundVusText = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("formName"));
                    args.updatedMembers.Add(GetMemberByName("parentName"));
                    args.updatedMembers.Add(GetMemberByName("width"));
                    args.updatedMembers.Add(GetMemberByName("height"));
                    args.updatedMembers.Add(GetMemberByName("VariantFoundVusText"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/
        #endregion

        public enum Subtype
        {
            Normal,
            System,
            User
        }

        public Subtype PrefSubtype;

        /// <summary>
        /// For use with system default meta data construction
        /// </summary>
        public GUIPreference(bool readOnly = false)
        {
            this.ReadOnly = readOnly;

            this.PrefSubtype = Subtype.System;

            owningPatient = null;
            parentName = "System";
            formName = "Default";

            annotations = new PedigreeAnnotationList("-1");
            //annotations.BackgroundListLoad();
        }

        /// <summary>
        /// For use with user default meta data contruction
        /// </summary>
        /// <param name="u"></param>
        /// <param name="readOnly">should this object be locked against db persistance?</param>
        public GUIPreference(User u, bool readOnly = false)
        {
            this.ReadOnly = readOnly;

            this.PrefSubtype = Subtype.User;
            annotations = new PedigreeAnnotationList("-1");
            owningPatient = null;
            parentName = u.userLogin;
            formName = "Default";
        }

        public GUIPreference(Patient proband, bool readOnly = false)
        {
            this.ReadOnly = readOnly;

            this.PrefSubtype = Subtype.Normal;
            annotations = new PedigreeAnnotationList(proband.unitnum);
            owningPatient = proband;
        }

        /// <summary>
        /// For normal usage
        /// </summary>
        public GUIPreference(Patient proband, DateTime GUIPreference_modifiedDate, string GUIPreference_formName, string GUIPreference_parentName, int GUIPreference_width, int GUIPreference_height, bool readOnly = false)
            : this(proband, readOnly)
        {
            modifiedDate = GUIPreference_modifiedDate;
            formName = GUIPreference_formName;
            parentName = GUIPreference_parentName;
            width = GUIPreference_width;
            height = GUIPreference_height;

            ConsumeSettings(SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs);
            owningPatient = proband;
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            if (owningPatient != null)
            {
                pc.Add("unitnum", owningPatient.unitnum);
            }
            else
            {
                pc.Add("unitnum", -1);
            }

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_GUIPreference",
                                      ref pc);
        }

        public override void BackgroundLoadWork()
        {
            ParameterCollection p = new ParameterCollection();
            DoLoadWithSpAndParams("sp_3_LoadSystemWideGUIPrefs", p);

            annotations.BackgroundListLoad();
            //TODO add switch in case we ever add user based prefs and implement new sp
        }
        public PedigreeAnnotationList CopyAnnotations()
        {
            string unit = annotations.GetUnitnum();
            PedigreeAnnotationList retval = new PedigreeAnnotationList(unit);
            foreach (PedigreeAnnotation pa in annotations)
            {
                PedigreeAnnotation newPa = new PedigreeAnnotation(unit);
                newPa.annotation = pa.annotation;
                newPa.area = pa.area;
                newPa.slot = pa.slot;
                retval.Add(newPa);
            }
            retval.IsLoaded = true;
            return retval;
        }
        public void ConsumeSettings(GUIPreference preferences)
        {
            switch (this.PrefSubtype)
            {
                case Subtype.Normal:
                    this.owningPatient = preferences.owningPatient;
                    this.parentName = preferences.parentName;
                    this.formName = preferences.formName;
                    break;
                case Subtype.System:
                    this.owningPatient = null;
                    this.parentName = "System";
                    this.formName = "Default";
                    break;
                case Subtype.User:
                    this.owningPatient = null;
                    this.parentName = SessionManager.Instance.ActiveUser.userLogin;
                    this.formName = "Default";
                    break;
                default:
                    this.owningPatient = preferences.owningPatient;
                    this.parentName = preferences.parentName;
                    this.formName = preferences.formName;
                    break;
            }

            this.modifiedDate = DateTime.Now;
            this.width = preferences.width;
            this.height = preferences.height;
            this.pedigreeZoomValue = preferences.pedigreeZoomValue;
            this.pedigreeVerticalSpacing = preferences.pedigreeVerticalSpacing;
            
            this.ShowRelIds = preferences.ShowRelIds;
            this.PedigreeBackground = preferences.PedigreeBackground;
            this.nameWidth = preferences.nameWidth;
            this.limitedEthnicity = preferences.limitedEthnicity;
            this.ShowTitle = preferences.ShowTitle;
            this.ShowName = preferences.ShowName;
            this.NameFont = preferences.NameFont;
            this.ShowUnitnum = preferences.ShowUnitnum;
            this.UnitnumFont = preferences.UnitnumFont;
            this.ShowDob = preferences.ShowDob;
            this.DobFont = preferences.DobFont;
            this.TitleBackground = preferences.TitleBackground;
            this.TitleBorder = preferences.TitleBorder;
            this.ShowLegend = preferences.ShowLegend;
            this.LegendBackground = preferences.LegendBackground;
            this.LegendBorder = preferences.LegendBorder;
            this.LegendRadius = preferences.LegendRadius;
            this.LegendFont = preferences.LegendFont;
            this.ShowComment = preferences.ShowComment;
            this.CommentBackground = preferences.CommentBackground;
            this.CommentBorder = preferences.CommentBorder;
            this.CommentFont = preferences.CommentFont;

            this.LegendX = preferences.LegendX;
            this.LegendY = preferences.LegendY;
            this.LegendHeight = preferences.LegendHeight;
            this.LegendWidth = preferences.LegendWidth;
            this.TitleX = preferences.TitleX;
            this.TitleY = preferences.TitleY;
            this.TitleHeight = preferences.TitleHeight;
            this.TitleWidth = preferences.TitleWidth;
            this.CommentX = preferences.CommentX;
            this.CommentY = preferences.CommentY;
            this.CommentHeight = preferences.CommentHeight;
            this.CommentWidth = preferences.CommentWidth;


            this.VariantFoundText = preferences.VariantFoundText;
            this.VariantFoundVusText = preferences.VariantFoundVusText;
            this.VariantNotFoundText = preferences.VariantNotFoundText;
            this.VariantUnknownText = preferences.VariantUnknownText;
            this.VariantNotTestedText = preferences.VariantNotTestedText;
            this.VariantHeteroText = preferences.VariantHeteroText;

            this.hideNonBloodRelatives = preferences.hideNonBloodRelatives;


            foreach (PedigreeAnnotation pa in annotations)
            {
                foreach (PedigreeAnnotation target in preferences.annotations)
                {
                    if (pa.annotation == target.annotation)
                    {
                        if (pa.area != target.area)
                        {
                            pa.area = target.area;
                            pa.slot = target.slot;
                            pa.SignalModelChanged(new HraModelChangedEventArgs(null));
                        }
                        break;
                    }
                }
            }

            SignalModelChanged(new HraModelChangedEventArgs(null));
        }

        internal void SetParent(string p)
        {
            this.parentName = p;
        }

        internal void SetForm(string p)
        {
            this.formName = p;
        }
    }
}

