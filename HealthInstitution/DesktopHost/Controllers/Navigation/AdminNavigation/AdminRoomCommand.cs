﻿using HealthInstitution.Commands;
using HealthInstitution.Core;
using HealthInstitution.MVVM.ViewModels.AdminViewModels;
using HealthInstitution.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.MVVM.ViewModels.Commands.AdminCommands
{
    class AdminRoomCommand : BaseCommand
    {
        private readonly Institution _institution;
        private readonly NavigationStore _navigation;

        public AdminRoomCommand()
        {
            _institution = Institution.Instance();
            _navigation = NavigationStore.Instance();
        }
        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new AdminRoomViewModel();
        }
    }
}
