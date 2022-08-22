// Code by: @andrelosse

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkVPN.MVVM.ViewModel
{
    internal class SettingsViewModel
    {

        public static GlobalViewModel Instance { get; } = new GlobalViewModel();
        public SettingsViewModel()
        {
        }
    }
}
