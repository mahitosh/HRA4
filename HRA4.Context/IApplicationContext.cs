using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApps3.Model.MetaData;
using HRA4.Services.Interfaces;
namespace HRA4.Context
{
    public interface IApplicationContext
    {
         IServiceContext ServiceContext { get; }
         
    }
}
