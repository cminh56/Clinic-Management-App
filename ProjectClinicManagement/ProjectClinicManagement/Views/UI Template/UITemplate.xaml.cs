using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectClinicManagement.Views.UI_Template
{
    /// <summary>
    /// Interaction logic for UITemplate.xaml
    /// </summary>
    public partial class UITemplate : Window
    {
        public UITemplate()
        {
            InitializeComponent();
            var converter = new BrushConverter();
            ObservableCollection<Member> members = new ObservableCollection<Member>();

            //Create Datagrid Items info
            members.Add(new Member { Number = "1", Character = "A", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "A", Position = "A", Email = "A", Phone = "1" });
            members.Add(new Member { Number = "2", Character = "S", BgColor = (Brush)converter.ConvertFromString("#1e88e5"), Name = "S", Position = "S", Email = "S", Phone = "2" });
            members.Add(new Member { Number = "3", Character = "D", BgColor = (Brush)converter.ConvertFromString("#ff8f00"), Name = "D", Position = "D", Email = "D", Phone = "3" });
            members.Add(new Member { Number = "4", Character = "F", BgColor = (Brush)converter.ConvertFromString("#ff5252"), Name = "F", Position = "F", Email = "F", Phone = "4" });
            members.Add(new Member { Number = "5", Character = "G", BgColor = (Brush)converter.ConvertFromString("#0ca678"), Name = "G", Position = "G", Email = "G", Phone = "5" });
            members.Add(new Member { Number = "6", Character = "H", BgColor = (Brush)converter.ConvertFromString("#6741d9"), Name = "H", Position = "H", Email = "H", Phone = "6" });
            members.Add(new Member { Number = "7", Character = "J", BgColor = (Brush)converter.ConvertFromString("#ff6d00"), Name = "J", Position = "J", Email = "J", Phone = "7" });
            members.Add(new Member { Number = "8", Character = "K", BgColor = (Brush)converter.ConvertFromString("#1098ad"), Name = "K", Position = "K", Email = "K", Phone = "8" });

            membersDataGrid.ItemsSource = members;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;
                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    IsMaximized = false;
                }
            }
        }
    }
    public class Member
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Brush BgColor { get; set; }
    }
}
