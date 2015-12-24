using HRA4.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApps3.Model.MetaData;
using HRA4.Repositories;
using HRA4.Repositories.Interfaces;
namespace HRA4.Services
{
    public class ServiceContext : IServiceContext
    {
        string _username;

        Repositories.Interfaces.IRepositoryFactory _repositoryFactory;

        public ServiceContext(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        public ServiceContext(IRepositoryFactory repositoryFactory, string user)
        {
            this._repositoryFactory = repositoryFactory;
            this._username = user;
        }

        public ServiceContext(string user)
        {
            this._username = user;
        }
        public IAppointmentService AppointmentService
        {
            get { return new AppointmentService(_repositoryFactory,this._username); }
        }

        public IUserService UserService
        {
            get { return new UserService(); }
        }


        public IAdminService AdminService
        {
            get { return new AdminService(_repositoryFactory); }
        }
    }
}
