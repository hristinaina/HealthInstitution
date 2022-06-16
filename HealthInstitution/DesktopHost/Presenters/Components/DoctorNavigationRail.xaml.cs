using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthInstitution.MVVM.Views.Components
{
    /// <summary>
    /// Interaction logic for DoctorNavigationRail.xaml
    /// </summary>
    public partial class DoctorNavigationRail : UserControl
    {
        public int SelectedIndex { get => navigation.SelectedIndex; set => navigation.SelectedIndex = value; }
        public DoctorNavigationRail()
        {
            InitializeComponent();
        }
    }
}
