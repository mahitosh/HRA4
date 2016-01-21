﻿using HRA4.Services.Interfaces;
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
        Interfaces.IHraSessionManager _hraSessionManager;
        public ServiceContext(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        public ServiceContext(IRepositoryFactory repositoryFactory, string user)
        {
            this._repositoryFactory = repositoryFactory;
            _hraSessionManager = new HraSessionManager(_repositoryFactory, user);
            this._username = user;
        }

        public ServiceContext(string user)
        {
            this._username = user;
        }

        public IAppointmentService AppointmentService
        {
            get { return new AppointmentService(_repositoryFactory, _hraSessionManager); }
        }

        public IUserService UserService
        {
            get { return new UserService(); }
        }


        public IAdminService AdminService
        {
            get { return new AdminService(_repositoryFactory); }
        }


        public IExportImportService ExportImportService
        {
            get { return new ExportImportService(_repositoryFactory, _hraSessionManager); }
        }

        public ITemplateService TemplateService
        {
            get { return new TemplateService(_repositoryFactory, _hraSessionManager); }
        }

        public IRiskClinicServices RiskClinicServices
        {
            get { return new RiskClinicServices(_repositoryFactory, _hraSessionManager); }
        }



    }
}
