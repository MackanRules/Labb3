// Labb 3 Marcus Gustafsson
// Enkel WPF bokningsapplikation för en restaurang

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
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
using System.IO;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using File = System.IO.File;
using System.Threading;

namespace WPF_app_for_Labb3
{
    public partial class MainWindow : Window
    {
        DateTime selectedDate;
        DateTime selectedDateTime;
        ObservableCollection<Bokning> bokningar = new ObservableCollection<Bokning>();
        string fileName = "bokningsFil";

        public MainWindow()
        {
            InitializeComponent();

            //bokningar.Add(new Bokning(new DateTime(2022, 11, 26, 19, 00, 00), "19:00", "Marcus", 1));
            //bokningar.Add(new Bokning(new DateTime(2022, 11, 17, 18, 00, 00), "18:00", "Karl", 2));
            //bokningar.Add(new Bokning(new DateTime(2022, 11, 21, 20, 00, 00), "20:00", "Peter", 3));

            readBookingsAsync();

        }

        public void bookTable(object sender, RoutedEventArgs e)
        {
            selectedDateTime = (new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, Int32.Parse(timeSelect.Text.Substring(0, 2)), 00, 00));
            var temp = from b in bokningar
                       where b.date == selectedDateTime
                       where b.tableNumber == Int32.Parse(tableSelect.Text)
                       select b;

            if (temp.Count() >= 5)
            {
                MessageBox.Show("Fullbokat! Prova annat datum, tid eller bord!");
            }
            else
            {
                if (nameTextBox.Text.Equals(""))
                {
                    MessageBox.Show("Vänligen ange ett namn");
                }
                else
                {
                    try
                    {
                        bokningar.Add(new Bokning(selectedDateTime, timeSelect.Text, nameTextBox.Text, Int32.Parse(tableSelect.Text)));
                        writeBookingsAsync();
                        MessageBox.Show("Bokning genomförd!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex);
                    }
                }
                
            }
        }

        public void cancelBooking(object sender, RoutedEventArgs e)
        {
            if(listView.SelectedIndex >= 0)
            {
                bokningar.RemoveAt(listView.SelectedIndex);
                writeBookingsAsync();
                MessageBox.Show("Avbokat!");
            } else
            {
                MessageBox.Show("Klicka på en bokning i listan innan du klickar avboka!");
            }

        }

        public void toggleBookings(object sender, RoutedEventArgs e)
        {
            if(listView.Visibility == Visibility.Visible)
            {
                listView.Visibility = Visibility.Collapsed;
            } else
            {
                listView.Visibility = Visibility.Visible;
            }
        }

        private async void writeBookingsAsync()
        {
            using FileStream fs = File.Create(fileName);
            await JsonSerializer.SerializeAsync(fs, bokningar);
            await fs.DisposeAsync();

        }
       
        private async Task readBookingsAsync()
        {
            try
            {
                using FileStream openStream = File.OpenRead(fileName);
                bokningar = await JsonSerializer.DeserializeAsync<ObservableCollection<Bokning>>(openStream);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            
            listView.ItemsSource = bokningar;
        }

        private void DatePicker_SelectedDateChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedDate = (DateTime)((DatePicker)sender).SelectedDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            
        }

    }
}
