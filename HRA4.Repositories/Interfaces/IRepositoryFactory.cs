using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        ISuperAdminRepository SuperAdminRepository { get; }
        ITenantMasterRepository TenantRepository { get; }
        IHtmlTemplateRepository HtmlTemplateRepository { get; }
        IMenuRepository MenuRepository { get; }
    }
}
