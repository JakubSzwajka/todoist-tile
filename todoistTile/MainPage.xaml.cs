using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace todoistTile
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<TodoistItem> tasks = new List<TodoistItem>();


        public MainPage()
        {
            this.InitializeComponent();
        }

        Library library = new Library();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            library.Init(Display);
            TodoistConnector.obtainTasksFromApi("baaa494629a69edc5e8274e9be151ef34f3ce6ae");
            tasks = TodoistConnector.getTasks(); 
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            library.Add(Display, Value.Text, Colour, sender);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            library.Remove(Display);
        }

        private void UpdateList(object sender, RoutedEventArgs e)
        {
            foreach (var task in tasks)
            {
                library.Add(Display, task , sender); 
            }
        }
    }
}
