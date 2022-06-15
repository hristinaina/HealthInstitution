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
    /// Interaction logic for NotificationSnackbar.xaml
    /// </summary>
    public partial class NotificationSnackbar : UserControl { 
    



        public double NotificationHeight
        {
            get { return (double)GetValue(NotificationHeightProperty); }
            set { SetValue(NotificationHeightProperty, value); }
        }

        public static readonly DependencyProperty NotificationHeightProperty =
            DependencyProperty.Register("NotificationHeight", typeof(double), typeof(NotificationSnackbar), new PropertyMetadata(default(double)));



        public string ActionContent { get => Notification.Message.ActionContent.ToString(); set => Notification.Message.ActionContent = value; }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(NotificationSnackbar), new PropertyMetadata(false));

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(NotificationSnackbar), new PropertyMetadata(""));



        public Object ActionCommand
        {
            get { return (Object)GetValue(ActionCommandProperty); }
            set { SetValue(ActionCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionCommandProperty =
            DependencyProperty.Register("ActionCommand", typeof(Object), typeof(NotificationSnackbar), new PropertyMetadata());



        public NotificationSnackbar()
        {
            InitializeComponent();
        }
    }
}
