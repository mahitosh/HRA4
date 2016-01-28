using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.PatientRecord.FHx
{
    public enum RelationshipEnum
    {
        SELF,
        MOTHER,
        FATHER,
        GRANDMOTHER,
        GRANDFATHER,
        AUNT,
        UNCLE,
        COUSIN,
        DAUGHTER,
        SON,
        BROTHER,
        SISTER,
        NIECE,
        NEPHEW,
        SPOUSE,
        UNKNOWN,
        PLACEHOLDER,
        COUSIN_FEMALE,
        COUSIN_MALE,
        OTHER,
        OTHER_FEMALE,
        OTHER_MALE,
        GRANDFATHERS_FATHER,
        GRANDFATHERS_MOTHER,
        GRANDMOTHERS_FATHER,
        GRANDMOTHERS_MOTHER,
        HALF_BROTHER,
        HALF_SISTER,
        BROTHER_OF_PATERNAL_GF,
        SISTER_OF_PATERNAL_GF,
        BROTHER_OF_PATERNAL_GM,
        SISTER_OF_PATERNAL_GM,
        BROTHER_OF_MATERNAL_GF,
        SISTER_OF_MATERNAL_GF,
        BROTHER_OF_MATERNAL_GM,
        SISTER_OF_MATERNAL_GM,
        HALF_AUNT,
        HALF_UNCLE,
        FETUS
    } ;

    public class Relationship
    {
        
        public static RelationshipEnum Parse(String relationshipString)
        {
            if (string.IsNullOrEmpty(relationshipString))
                return RelationshipEnum.UNKNOWN;

            relationshipString = relationshipString.ToUpper();
            if (relationshipString.Equals("SELF"))
            {
                return RelationshipEnum.SELF;
            }
            if (relationshipString.Equals("MOTHER"))
            {
                return RelationshipEnum.MOTHER;
            }
            if (relationshipString.Equals("FATHER"))
            {
                return RelationshipEnum.FATHER;
            }
            if (relationshipString.Equals("GRANDMOTHER"))
            {
                return RelationshipEnum.GRANDMOTHER;
            }
            if (relationshipString.Equals("GRANDFATHER"))
            {
                return RelationshipEnum.GRANDFATHER;
            }
            if (relationshipString.Equals("AUNT"))
            {
                return RelationshipEnum.AUNT;
            }
            if (relationshipString.Equals("UNCLE"))
            {
                return RelationshipEnum.UNCLE;
            }
            if (relationshipString.Equals("COUSIN"))
            {
                return RelationshipEnum.COUSIN;
            }
            if (relationshipString.Equals("COUSIN (FEMALE)"))
            {
                return RelationshipEnum.COUSIN_FEMALE;
            }
            if (relationshipString.Equals("COUSIN (MALE)"))
            {
                return RelationshipEnum.COUSIN_MALE;
            }
            if (relationshipString.Equals("DAUGHTER"))
            {
                return RelationshipEnum.DAUGHTER;
            }
            if (relationshipString.Equals("SON"))
            {
                return RelationshipEnum.SON;
            }
            if (relationshipString.Equals("BROTHER"))
            {
                return RelationshipEnum.BROTHER;
            }
            if (relationshipString.Equals("SISTER"))
            {
                return RelationshipEnum.SISTER;
            }
            if (relationshipString.Equals("NIECE"))
            {
                return RelationshipEnum.NIECE;
            }
            if (relationshipString.Equals("NEPHEW"))
            {
                return RelationshipEnum.NEPHEW;
            }
            if (relationshipString.Equals("OTHER"))
            {
                return RelationshipEnum.OTHER;
            }
            if (relationshipString.Equals("OTHER (FEMALE)"))
            {
                return RelationshipEnum.OTHER_FEMALE;
            }
            if (relationshipString.Equals("OTHER (MALE)"))
            {
                return RelationshipEnum.OTHER_MALE;
            }
            if (relationshipString.Equals("GRANDMOTHER'S MOTHER"))
            {
                return RelationshipEnum.GRANDMOTHERS_MOTHER;
            }
            if (relationshipString.Equals("GRANDMOTHER'S FATHER"))
            {
                return RelationshipEnum.GRANDMOTHERS_FATHER;
            }
            if (relationshipString.Equals("GRANDFATHER'S MOTHER"))
            {
                return RelationshipEnum.GRANDFATHERS_MOTHER;
            }
            if (relationshipString.Equals("GRANDFATHER'S FATHER"))
            {
                return RelationshipEnum.GRANDFATHERS_FATHER;
            }
            if (relationshipString.Equals("HALF BROTHER"))
            {
                return RelationshipEnum.HALF_BROTHER;
            }
            if (relationshipString.Equals("HALF SISTER"))
            {
                return RelationshipEnum.HALF_SISTER;
            }
            if (relationshipString.Equals("BROTHER OF PATERNAL GF"))
            {
                return RelationshipEnum.BROTHER_OF_PATERNAL_GF;
            }
            if (relationshipString.Equals("SISTER OF PATERNAL GF"))
            {
                return RelationshipEnum.SISTER_OF_PATERNAL_GF;
            }
            if (relationshipString.Equals("BROTHER OF PATERNAL GM"))
            {
                return RelationshipEnum.BROTHER_OF_PATERNAL_GM;
            }
            if (relationshipString.Equals("SISTER OF PATERNAL GM"))
            {
                return RelationshipEnum.SISTER_OF_PATERNAL_GM;
            }
            if (relationshipString.Equals("BROTHER OF MATERNAL GF"))
            {
                return RelationshipEnum.BROTHER_OF_MATERNAL_GF;
            }
            if (relationshipString.Equals("SISTER OF MATERNAL GF"))
            {
                return RelationshipEnum.SISTER_OF_MATERNAL_GF;
            }
            if (relationshipString.Equals("BROTHER OF MATERNAL GM"))
            {
                return RelationshipEnum.BROTHER_OF_MATERNAL_GM;
            }
            if (relationshipString.Equals("SISTER OF MATERNAL GM"))
            {
                return RelationshipEnum.SISTER_OF_MATERNAL_GM;
            }
            if (relationshipString.Equals("FETUS"))
            {
                return RelationshipEnum.FETUS;
            }
            //if (relationshipString.Equals("FOP RELATIVE"))
            //{
            //    return RelationshipEnum.FOP_RELATIVE;
            //}

            return RelationshipEnum.UNKNOWN;
        }

        public static bool hasBloodline(RelationshipEnum relationship)
        {
            switch (relationship)
            {
                case RelationshipEnum.SELF:
                case RelationshipEnum.MOTHER:
                case RelationshipEnum.FATHER:
                case RelationshipEnum.DAUGHTER:
                case RelationshipEnum.SON:
                case RelationshipEnum.BROTHER:
                case RelationshipEnum.SISTER:
                case RelationshipEnum.NIECE:
                case RelationshipEnum.NEPHEW:
                    return false;
                default:
                    return true;
            }
        }

        public static bool canDelete(RelationshipEnum relationship)
        {
            switch (relationship)
            {
                case RelationshipEnum.SELF:
                case RelationshipEnum.MOTHER:
                case RelationshipEnum.FATHER:
                case RelationshipEnum.GRANDMOTHER:
                case RelationshipEnum.GRANDFATHER:
                case RelationshipEnum.GRANDFATHERS_FATHER:
                case RelationshipEnum.GRANDFATHERS_MOTHER:
                case RelationshipEnum.GRANDMOTHERS_FATHER:
                case RelationshipEnum.GRANDMOTHERS_MOTHER:
                    return false;
                default:
                    return true;
            }
        }

        public static string toString(RelationshipEnum relationship)
        {
            switch (relationship)
            {
                case RelationshipEnum.SELF:
                    return "Self";
                case RelationshipEnum.MOTHER:
                    return "Mother";
                case RelationshipEnum.FATHER:
                    return "Father";
                case RelationshipEnum.GRANDMOTHER:
                    return "Grandmother";
                case RelationshipEnum.GRANDFATHER:
                    return "Grandfather";
                case RelationshipEnum.AUNT:
                    return "Aunt";
                case RelationshipEnum.UNCLE:
                    return "Uncle";
                case RelationshipEnum.COUSIN:
                    return "Cousin";
                case RelationshipEnum.DAUGHTER:
                    return "Daughter";
                case RelationshipEnum.SON:
                    return "Son";
                case RelationshipEnum.BROTHER:
                    return "Brother";
                case RelationshipEnum.SISTER:
                    return "Sister";
                case RelationshipEnum.NIECE:
                    return "Niece";
                case RelationshipEnum.NEPHEW:
                    return "Nephew";
                case RelationshipEnum.OTHER:
                    return "Other";
                case RelationshipEnum.SPOUSE:
                    return "Spouse";
                case RelationshipEnum.PLACEHOLDER:
                    return "Placeholder";
                case RelationshipEnum.COUSIN_FEMALE:
                    return "Cousin (Female)";
                case RelationshipEnum.COUSIN_MALE:
                    return "Cousin (Male)";
                case RelationshipEnum.OTHER_MALE:
                    return "Other (Male)";
                case RelationshipEnum.OTHER_FEMALE:
                    return "Other (Female)";
                case RelationshipEnum.GRANDFATHERS_FATHER:
                    return "Grandfather's Father";
                case RelationshipEnum.GRANDFATHERS_MOTHER:
                    return "Grandfather's Mother";
                case RelationshipEnum.GRANDMOTHERS_FATHER:
                    return "Grandmother's Father";
                case RelationshipEnum.GRANDMOTHERS_MOTHER:
                    return "Grandmother's Mother";
                case RelationshipEnum.HALF_BROTHER:
                    return "Half Brother";
                case RelationshipEnum.HALF_SISTER:
                    return "Half Sister";
                case RelationshipEnum.BROTHER_OF_PATERNAL_GF:
                    return "Brother of paternal GF";
                case RelationshipEnum.SISTER_OF_PATERNAL_GF:
                    return "Sister of paternal GF";
                case RelationshipEnum.BROTHER_OF_PATERNAL_GM:
                    return "Brother of paternal GM";
                case RelationshipEnum.SISTER_OF_PATERNAL_GM:
                    return "Sister of paternal GM";
                case RelationshipEnum.BROTHER_OF_MATERNAL_GF:
                    return "Brother of maternal GF";
                case RelationshipEnum.SISTER_OF_MATERNAL_GF:
                    return "Sister of maternal GF";
                case RelationshipEnum.BROTHER_OF_MATERNAL_GM:
                    return "Brother of maternal GM";
                case RelationshipEnum.SISTER_OF_MATERNAL_GM:
                    return "Sister of maternal GM";
                case RelationshipEnum.FETUS:
                    return "Fetus";
                //case RelationshipEnum.FOP_RELATIVE:
                //    return "FOB Relative";

                default:
                    return "Unknown";
            }
        }

        public static String getFullRelationship(String relationship, String bloodline)
        {
            String fullRelationship = bloodline;
            if (fullRelationship.Length > 0)
            {
                fullRelationship = fullRelationship + " ";
            }
            fullRelationship = fullRelationship + relationship;

            return fullRelationship;
        }

        public static GenderEnum getGenderFromRelationshipType(RelationshipEnum relationship)
        {
            switch (relationship)
            {
                //case RelationshipEnum.SELF:
                //    //return GenderEnum.Female;
                //case RelationshipEnum.SPOUSE:
                //    //return GenderEnum.Male;
                case RelationshipEnum.MOTHER:
                case RelationshipEnum.GRANDMOTHER:
                case RelationshipEnum.AUNT:
                case RelationshipEnum.DAUGHTER:
                case RelationshipEnum.SISTER:
                case RelationshipEnum.NIECE:
                case RelationshipEnum.COUSIN_FEMALE:
                case RelationshipEnum.OTHER_FEMALE:
                case RelationshipEnum.GRANDFATHERS_MOTHER:
                case RelationshipEnum.GRANDMOTHERS_MOTHER:
                case RelationshipEnum.HALF_SISTER:
                case RelationshipEnum.SISTER_OF_PATERNAL_GF:
                case RelationshipEnum.SISTER_OF_PATERNAL_GM:
                case RelationshipEnum.SISTER_OF_MATERNAL_GF:
                case RelationshipEnum.SISTER_OF_MATERNAL_GM:
                    return GenderEnum.Female;
                case RelationshipEnum.FATHER:
                case RelationshipEnum.GRANDFATHER:
                case RelationshipEnum.UNCLE:
                case RelationshipEnum.SON:
                case RelationshipEnum.BROTHER:
                case RelationshipEnum.NEPHEW:
                case RelationshipEnum.COUSIN_MALE:
                case RelationshipEnum.OTHER_MALE:
                case RelationshipEnum.GRANDFATHERS_FATHER:
                case RelationshipEnum.GRANDMOTHERS_FATHER:
                case RelationshipEnum.HALF_BROTHER:
                case RelationshipEnum.BROTHER_OF_PATERNAL_GF:
                case RelationshipEnum.BROTHER_OF_PATERNAL_GM:
                case RelationshipEnum.BROTHER_OF_MATERNAL_GF:
                case RelationshipEnum.BROTHER_OF_MATERNAL_GM:
                    return GenderEnum.Male;
                default:
                    return GenderEnum.Unknown;
            }
        }

        public static RelationshipEnum getAlternateGenderRelationship(RelationshipEnum relationship)
        {
            switch (relationship)
            {

                case RelationshipEnum.MOTHER:
                    return RelationshipEnum.FATHER;
                case RelationshipEnum.GRANDMOTHER:
                    return RelationshipEnum.GRANDFATHER;
                case RelationshipEnum.AUNT:
                    return RelationshipEnum.UNCLE;
                case RelationshipEnum.DAUGHTER:
                    return RelationshipEnum.SON;
                case RelationshipEnum.SISTER:
                    return RelationshipEnum.BROTHER;
                case RelationshipEnum.NIECE:
                    return RelationshipEnum.NEPHEW;
                case RelationshipEnum.COUSIN_FEMALE:
                    return RelationshipEnum.COUSIN_MALE;
                case RelationshipEnum.OTHER_FEMALE:
                    return RelationshipEnum.OTHER_MALE;
                case RelationshipEnum.GRANDFATHERS_MOTHER:
                    return RelationshipEnum.GRANDFATHERS_FATHER;
                case RelationshipEnum.GRANDMOTHERS_MOTHER:
                    return RelationshipEnum.GRANDMOTHERS_FATHER;
                case RelationshipEnum.HALF_SISTER:
                    return RelationshipEnum.HALF_BROTHER;
                case RelationshipEnum.SISTER_OF_PATERNAL_GF:
                    return RelationshipEnum.BROTHER_OF_PATERNAL_GF;
                case RelationshipEnum.SISTER_OF_PATERNAL_GM:
                    return RelationshipEnum.BROTHER_OF_PATERNAL_GM;
                case RelationshipEnum.SISTER_OF_MATERNAL_GF:
                    return RelationshipEnum.BROTHER_OF_MATERNAL_GF;
                case RelationshipEnum.SISTER_OF_MATERNAL_GM:
                    return RelationshipEnum.BROTHER_OF_MATERNAL_GM;

                case RelationshipEnum.FATHER:
                    return RelationshipEnum.MOTHER;
                case RelationshipEnum.GRANDFATHER:
                    return RelationshipEnum.GRANDMOTHER;
                case RelationshipEnum.UNCLE:
                    return RelationshipEnum.AUNT;
                case RelationshipEnum.SON:
                    return RelationshipEnum.DAUGHTER;
                case RelationshipEnum.BROTHER:
                    return RelationshipEnum.SISTER;
                case RelationshipEnum.NEPHEW:
                    return RelationshipEnum.NIECE;
                case RelationshipEnum.COUSIN_MALE:
                    return RelationshipEnum.COUSIN_FEMALE;
                case RelationshipEnum.OTHER_MALE:
                    return RelationshipEnum.OTHER_FEMALE;
                case RelationshipEnum.GRANDFATHERS_FATHER:
                    return RelationshipEnum.GRANDFATHERS_MOTHER;
                case RelationshipEnum.GRANDMOTHERS_FATHER:
                    return RelationshipEnum.GRANDMOTHERS_MOTHER;
                case RelationshipEnum.HALF_BROTHER:
                    return RelationshipEnum.HALF_SISTER;
                case RelationshipEnum.BROTHER_OF_PATERNAL_GF:
                    return RelationshipEnum.SISTER_OF_PATERNAL_GF;
                case RelationshipEnum.BROTHER_OF_PATERNAL_GM:
                    return RelationshipEnum.SISTER_OF_PATERNAL_GM;
                case RelationshipEnum.BROTHER_OF_MATERNAL_GF:
                    return RelationshipEnum.SISTER_OF_MATERNAL_GF;
                case RelationshipEnum.BROTHER_OF_MATERNAL_GM:
                    return RelationshipEnum.SISTER_OF_MATERNAL_GM;

                default:
                    return RelationshipEnum.OTHER;
            }
        }

        public static bool isGrandAuntorUncle(RelationshipEnum relationship)
        {
            return
                (relationship == RelationshipEnum.BROTHER_OF_PATERNAL_GF ||
                 relationship == RelationshipEnum.SISTER_OF_PATERNAL_GF ||
                 relationship == RelationshipEnum.BROTHER_OF_PATERNAL_GM ||
                 relationship == RelationshipEnum.SISTER_OF_PATERNAL_GM ||
                 relationship == RelationshipEnum.BROTHER_OF_MATERNAL_GF ||
                 relationship == RelationshipEnum.SISTER_OF_MATERNAL_GF ||
                 relationship == RelationshipEnum.BROTHER_OF_MATERNAL_GM ||
                 relationship == RelationshipEnum.SISTER_OF_MATERNAL_GM);
        }


        public static bool isHalfSibling(RelationshipEnum relationship)
        {
            return
                (relationship == RelationshipEnum.HALF_BROTHER ||
                 relationship == RelationshipEnum.HALF_SISTER);
        }


        public static bool isGreatGrandparent(RelationshipEnum relationship)
        {
            return
                (relationship == RelationshipEnum.GRANDFATHERS_MOTHER ||
                 relationship == RelationshipEnum.GRANDMOTHERS_MOTHER ||
                 relationship == RelationshipEnum.GRANDFATHERS_FATHER ||
                 relationship == RelationshipEnum.GRANDMOTHERS_FATHER);
        }

        public static bool isAuntOrUncle(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.AUNT || relationship == RelationshipEnum.UNCLE);
        }

        public static bool isOffspring(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.SON || relationship == RelationshipEnum.DAUGHTER);
        }

        public static bool isSibling(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.BROTHER || relationship == RelationshipEnum.SISTER);
        }

        public static bool isNieceOrNephew(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.NIECE || relationship == RelationshipEnum.NEPHEW);
        }

        public static bool isCousin(RelationshipEnum relationship)
        {
            return
                (relationship == RelationshipEnum.COUSIN || relationship == RelationshipEnum.COUSIN_MALE ||
                 relationship == RelationshipEnum.COUSIN_FEMALE);
        }

        public static bool isProband(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.SELF);
        }

        public static bool isParent(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.MOTHER || relationship == RelationshipEnum.FATHER);
        }

        public static bool isGrandparent(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.GRANDMOTHER || relationship == RelationshipEnum.GRANDFATHER);
        }

        public static bool isSpouse(RelationshipEnum relationship)
        {
            return (relationship == RelationshipEnum.SPOUSE);
        }

        public static bool isUnknown(RelationshipEnum relationship)
        {
            return
                (relationship == RelationshipEnum.UNKNOWN || relationship == RelationshipEnum.OTHER ||
                 relationship == RelationshipEnum.OTHER_FEMALE || relationship == RelationshipEnum.OTHER_MALE);
        }


        public static void SetRelationshipByChildType(GenderEnum child_gender, RelationshipEnum parentType, RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum parent_bloodline, out string relationshipOfChild, out string relationshipOther, out string bloodlineOfChild)
        {
            relationshipOfChild = Relationship.toString(RelationshipEnum.OTHER);
            relationshipOther = "";
            bloodlineOfChild = Bloodline.toString(parent_bloodline);

            if (child_gender == GenderEnum.Male)
            {
                switch (parentType)
                {
                    case RelationshipEnum.SELF:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.SON);
                        break;

                    case RelationshipEnum.MOTHER:
                    case RelationshipEnum.FATHER:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.BROTHER);
                        bloodlineOfChild = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Both);
                        break;

                    case RelationshipEnum.GRANDMOTHER:
                    case RelationshipEnum.GRANDFATHER:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.UNCLE);
                        break;

                    case RelationshipEnum.AUNT:
                    case RelationshipEnum.UNCLE:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.COUSIN_MALE);
                        break;

                    case RelationshipEnum.DAUGHTER:
                    case RelationshipEnum.SON:
                        relationshipOther = "Grandson";
                        break;

                    case RelationshipEnum.BROTHER:
                    case RelationshipEnum.SISTER:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.NEPHEW);
                        break;

                    case RelationshipEnum.NIECE:
                    case RelationshipEnum.NEPHEW:
                        relationshipOther = "Grandnephew";
                        break;

                    case RelationshipEnum.COUSIN_FEMALE:
                    case RelationshipEnum.COUSIN_MALE:
                    case RelationshipEnum.COUSIN:
                        relationshipOther = "First cousin once removed";
                        break;

                    case RelationshipEnum.HALF_BROTHER:
                    case RelationshipEnum.HALF_SISTER:
                        relationshipOther = "Half Nephew";
                        break;

                    case RelationshipEnum.GRANDFATHERS_FATHER:
                    case RelationshipEnum.GRANDFATHERS_MOTHER:
                        if (parent_bloodline == RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal)
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.BROTHER_OF_MATERNAL_GF);
                        }
                        else
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.BROTHER_OF_PATERNAL_GF);
                        }
                        break;

                    case RelationshipEnum.GRANDMOTHERS_FATHER:
                    case RelationshipEnum.GRANDMOTHERS_MOTHER:
                        if (parent_bloodline == RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal)
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.BROTHER_OF_MATERNAL_GM);
                        }
                        else
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.BROTHER_OF_PATERNAL_GM);
                        }
                        break;
                    case RelationshipEnum.BROTHER_OF_PATERNAL_GF:
                    case RelationshipEnum.SISTER_OF_PATERNAL_GF:
                    case RelationshipEnum.BROTHER_OF_PATERNAL_GM:
                    case RelationshipEnum.SISTER_OF_PATERNAL_GM:
                    case RelationshipEnum.BROTHER_OF_MATERNAL_GF:
                    case RelationshipEnum.SISTER_OF_MATERNAL_GF:
                    case RelationshipEnum.BROTHER_OF_MATERNAL_GM:
                    case RelationshipEnum.SISTER_OF_MATERNAL_GM:
                        relationshipOther = "Son of " +
                                            Relationship.toString(parentType);
                        break;

                    case RelationshipEnum.OTHER_MALE:
                    case RelationshipEnum.OTHER_FEMALE:
                    case RelationshipEnum.OTHER:
                    case RelationshipEnum.SPOUSE:
                    case RelationshipEnum.PLACEHOLDER:
                    case RelationshipEnum.UNKNOWN:
                        relationshipOther = "Son of Other";
                        break;
                    default:
                        break;
                }
            }
            else if (child_gender == GenderEnum.Female)
            {
                switch (parentType)
                {
                    case RelationshipEnum.SELF:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.DAUGHTER);
                        break;

                    case RelationshipEnum.MOTHER:
                    case RelationshipEnum.FATHER:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.SISTER);
                        bloodlineOfChild = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Both);
                        break;

                    case RelationshipEnum.GRANDMOTHER:
                    case RelationshipEnum.GRANDFATHER:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.AUNT);
                        break;

                    case RelationshipEnum.AUNT:
                    case RelationshipEnum.UNCLE:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.COUSIN_FEMALE);
                        break;

                    case RelationshipEnum.DAUGHTER:
                    case RelationshipEnum.SON:
                        relationshipOther = "Grandson";
                        break;

                    case RelationshipEnum.BROTHER:
                    case RelationshipEnum.SISTER:
                        relationshipOfChild = Relationship.toString(RelationshipEnum.NIECE);
                        break;

                    case RelationshipEnum.NIECE:
                    case RelationshipEnum.NEPHEW:
                        relationshipOther = "Grandniece";
                        break;

                    case RelationshipEnum.COUSIN_FEMALE:
                    case RelationshipEnum.COUSIN_MALE:
                    case RelationshipEnum.COUSIN:
                        relationshipOther = "First cousin once removed";
                        break;

                    case RelationshipEnum.HALF_BROTHER:
                    case RelationshipEnum.HALF_SISTER:
                        relationshipOther = "Half Niece";
                        break;

                    case RelationshipEnum.GRANDFATHERS_FATHER:
                    case RelationshipEnum.GRANDFATHERS_MOTHER:
                        if (parent_bloodline == RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal)
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.SISTER_OF_MATERNAL_GF);
                        }
                        else
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.SISTER_OF_PATERNAL_GF);
                        }
                        break;

                    case RelationshipEnum.GRANDMOTHERS_FATHER:
                    case RelationshipEnum.GRANDMOTHERS_MOTHER:
                        if (parent_bloodline == RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal)
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.SISTER_OF_MATERNAL_GM);
                        }
                        else
                        {
                            relationshipOfChild = Relationship.toString(RelationshipEnum.SISTER_OF_PATERNAL_GM);
                        }
                        break;
                    case RelationshipEnum.BROTHER_OF_PATERNAL_GF:
                    case RelationshipEnum.SISTER_OF_PATERNAL_GF:
                    case RelationshipEnum.BROTHER_OF_PATERNAL_GM:
                    case RelationshipEnum.SISTER_OF_PATERNAL_GM:
                    case RelationshipEnum.BROTHER_OF_MATERNAL_GF:
                    case RelationshipEnum.SISTER_OF_MATERNAL_GF:
                    case RelationshipEnum.BROTHER_OF_MATERNAL_GM:
                    case RelationshipEnum.SISTER_OF_MATERNAL_GM:
                        relationshipOther = "Daughter of " +
                                            Relationship.toString(parentType);
                        break;

                    case RelationshipEnum.OTHER_MALE:
                    case RelationshipEnum.OTHER_FEMALE:
                    case RelationshipEnum.OTHER:
                    case RelationshipEnum.SPOUSE:
                    case RelationshipEnum.PLACEHOLDER:
                    case RelationshipEnum.UNKNOWN:
                        relationshipOther = "Daughter of Other";
                        break;
                    default:
                        break;
                }
            }
        }


        public static void SetRelationshipByParentType(GenderEnum parent_gender, RelationshipEnum childType, RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum child_bloodline, out string relationshipOfParent, out string relationshipOther, out string bloodlineOfParent)
        {
            relationshipOfParent = Relationship.toString(RelationshipEnum.OTHER);
            relationshipOther = "";
            bloodlineOfParent = Bloodline.toString(child_bloodline);

            if (parent_gender == GenderEnum.Male)
            {
                switch (childType)
                {
                    case RelationshipEnum.SELF:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.FATHER);
                        bloodlineOfParent = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Paternal);
                        break;

                    case RelationshipEnum.MOTHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDFATHER);
                        bloodlineOfParent = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal);
                        break;

                    case RelationshipEnum.FATHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDFATHER);
                        bloodlineOfParent = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Paternal);
                        break;

                    case RelationshipEnum.GRANDMOTHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDMOTHERS_FATHER);
                        bloodlineOfParent = Bloodline.toString(child_bloodline);
                        break;

                    case RelationshipEnum.GRANDFATHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDFATHERS_FATHER);
                        bloodlineOfParent = Bloodline.toString(child_bloodline);
                        break;

                    case RelationshipEnum.AUNT:
                    case RelationshipEnum.UNCLE:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDFATHER);
                        bloodlineOfParent = Bloodline.toString(child_bloodline);
                        break;

                    default:
                        break;
                }
            }
            else if (parent_gender == GenderEnum.Female)
            {
                switch (childType)
                {
                    case RelationshipEnum.SELF:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.MOTHER);
                        bloodlineOfParent = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal);
                        break;

                    case RelationshipEnum.MOTHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDMOTHER);
                        bloodlineOfParent = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Maternal);
                        break;

                    case RelationshipEnum.FATHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDMOTHER);
                        bloodlineOfParent = Bloodline.toString(RiskApps3.Model.PatientRecord.FHx.Bloodline.BloodlineEnum.Paternal);
                        break;

                    case RelationshipEnum.GRANDMOTHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDMOTHERS_MOTHER);
                        bloodlineOfParent = Bloodline.toString(child_bloodline);
                        break;

                    case RelationshipEnum.GRANDFATHER:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDFATHERS_MOTHER);
                        bloodlineOfParent = Bloodline.toString(child_bloodline);
                        break;

                    case RelationshipEnum.AUNT:
                    case RelationshipEnum.UNCLE:
                        relationshipOfParent = Relationship.toString(RelationshipEnum.GRANDMOTHER);
                        bloodlineOfParent = Bloodline.toString(child_bloodline);
                        break;

                    default:
                        break;
                }
            }
        }



    }
}