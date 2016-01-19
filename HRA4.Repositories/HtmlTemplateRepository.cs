﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Repositories
{
    public class HtmlTemplateRepository : Interfaces.IHtmlTemplateRepository
    {
        dynamic dbContext;

        public HtmlTemplateRepository(dynamic database)
        {
            dbContext = database;
        }

        public void Insert(Entities.HtmlTemplate template)
        {
            dbContext.HtmlTemplate.Insert(template);
        }

        public void Save(Entities.HtmlTemplate template)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate GetTemplateById(int instituionId, int templateId)
        {
            return dbContext.HtmlTemplate.FindByInstitutionIdAndId(instituionId, templateId);
        }

        public List<Entities.HtmlTemplate> GetAllTemplates(int institutionId)
        {
            return dbContext.HtmlTemplate.FindAllByInstitutionId(institutionId);
        }
        public int UpdateHtmlTemplate(Entities.HtmlTemplate HtmlTemplate)
        {
            return dbContext.HtmlTemplate.Update(HtmlTemplate);
        }
        

    }
}