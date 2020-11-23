using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace todoistTile { 
    class TodoistConnector
    {

        private static List<TodoistItem> todaysTasks = new List<TodoistItem>();

        public static async void obtainTasksFromApi(string token)
        {
            // 1000 tasks for one day is enought 
            var client = new HttpClient();
            String json = "";
            String resource_types = "[\"all\"]";
            DateTime todayDate = DateTime.Today;

            // Create the HttpContent for the form to be posted.
            var requestContent = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("token", token),
                    new KeyValuePair<string, string>("sync_token", "*"),
                    new KeyValuePair<string, string>("resource_types", resource_types),
                });

            // Get the response.
            HttpResponseMessage response = await client.PostAsync(
                "https://api.todoist.com/sync/v8/sync",
                requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new System.IO.StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                json += await reader.ReadToEndAsync();
            }

            dynamic items = TodoistConnector.getJson(json, "items");

            foreach (var item in items)
            {
                if (item.due != null)
                {
                    DateTime itemDate = DateTime.Parse(item.due.date);
                    Console.WriteLine(itemDate);

                    if (itemDate <= todayDate)
                    {
                        String id = Convert.ToString(item.id); 
                        String content = Convert.ToString(item.content);
                        DateTime date_added = DateTime.Parse(Convert.ToString(item.date_added)); 
                        DateTime due_date = DateTime.Parse(Convert.ToString(item.due.date)); 

                        TodoistItem newItem = new TodoistItem(id, content, date_added, due_date); 
                        TodoistConnector.todaysTasks.Add(newItem);
                        Console.WriteLine(item.content);
                    }
                }
            }
        }
        private static dynamic getJson(String json, String itemName)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic items = serializer.Deserialize<object>(json);
            return items[itemName];
        }

        public static List<TodoistItem> getTasks()
        {
            return TodoistConnector.todaysTasks; 
        }
    }
}