using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM = RiskApps3.Model.MetaData;

using VM = HRA4.ViewModels;
using RiskApps3.Model.Clinic;
namespace HRA4.Mapper
{
    public static class ProviderMapper
    {
        public static VM.Provider ToProvider(this Provider provider )
        {
            return new VM.Provider()
            {
                Id = provider.providerID,
                //ProviderName = provider.PrintableName
            };
        }

        //public static List<VM.Provider> ToProviderList(this RAM.AllProvider providers)
        //{
        //    List<VM.Provider> vmProviders = new List<VM.Provider>();
        //    vmProviders.AddRange(providers.Select(p => new VM.Provider()
        //    {
        //         Id= p.providerID,
        //         ProviderName = p.PrintableName
        //    }));
        //    return vmProviders;
        //}

        public static List<VM.Provider> ToProviderList()
        {
            List<VM.Provider> vmProviders = new List<VM.Provider>();
            /*
            vmProviders.AddRange(providers.Select(p => new VM.Provider()
            {
                Id = p.providerID,
                ProviderName = p.PrintableName
            }));
             */ 
            return vmProviders;
        }
    }
}
