﻿using RiskApps3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services.Interfaces
{
    public interface IAppointmentService
    {
        List<HraObject> GetAppointments();
    }
}
