﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services.Interfaces
{
    public interface IHraSessionManager
    {
        string Username { get; }
        void SetRaActiveUser(string user);
        void SetActivePatient(string mrn, int apptId);
        void SetConfig(string InstitutionId, string config);
    }
}