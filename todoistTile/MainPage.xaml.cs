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
        
        public MainPage()
        {
            this.InitializeComponent();
        }
        
        Controller controller = new Controller();
        TodoistConnector connector = new TodoistConnector(); 

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            controller.Init(Display);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            controller.Remove(Display);
        }

        private void UpdateList(object sender, RoutedEventArgs e)
        {
            List<TodoistItem> tasks = new List<TodoistItem>();

            connector.obtainDataFromApi(token.Text);
            tasks = connector.getTasks();

            // Send main notification 
            controller.Notify( tasks, connector.getUserName(), connector.getUserEmail());

            // Add each item
            foreach (var task in tasks)
            {
                //controller.Add(Display, task , sender); 
            }
        }
    }
}
