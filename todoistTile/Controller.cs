using System;
using System.Collections.Generic;
using System.Globalization;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

public class Item
{
    public string Id { get; set; }
    public string Content { get; set; }
    public Brush Colour { get; set; }
}

public class Controller
{
    private Random _random = new Random((int)DateTime.Now.Ticks);

    private Color FromString(string value)
    {
        return Color.FromArgb(
        Byte.Parse(value.Substring(0, 2), NumberStyles.HexNumber),
        Byte.Parse(value.Substring(2, 2), NumberStyles.HexNumber),
        Byte.Parse(value.Substring(4, 2), NumberStyles.HexNumber),
        Byte.Parse(value.Substring(6, 2), NumberStyles.HexNumber));
    }

    public async void Init(ListBox display)
    {
        display.Items.Clear();
        IReadOnlyList<SecondaryTile> list = await SecondaryTile.FindAllAsync();
        foreach (SecondaryTile item in list)
        {
            display.Items.Add(new Item
            {
                Id = item.TileId,
                Content = item.DisplayName,
                Colour = new SolidColorBrush(item.VisualElements.BackgroundColor)
            });
        }
    }

    public async void Add(ListBox display, TodoistItem item, object selection)
    {
        String id = item.getId();
        String value = item.getContent(); 

        SecondaryTile tile = new SecondaryTile(id, value, id, new Uri("ms-appx:///"), TileSize.Wide310x150);
        Color background = FromString(item.getColour());

        tile.VisualElements.BackgroundColor = background;
        tile.VisualElements.ForegroundText = ForegroundText.Light;
        tile.VisualElements.ShowNameOnSquare150x150Logo = true;
        tile.VisualElements.ShowNameOnSquare310x310Logo = true;
        tile.VisualElements.ShowNameOnWide310x150Logo = true;
        
        await tile.RequestCreateAsync();
        display.Items.Add(new Item { Id = tile.TileId, Content = value, Colour = new SolidColorBrush(background) });
    }

    public void Notify( List<TodoistItem> items , String userName, String userEmail)
    {
        var template = TileTemplateType.TileWide310x150BlockAndText01;
        var tileXML = TileUpdateManager.GetTemplateContent(template);

        String title = userName + " " + userEmail; 

        var tileTextAttributes = tileXML.GetElementsByTagName("text");
        tileTextAttributes[0].AppendChild(tileXML.CreateTextNode(userName));
        tileTextAttributes[0].AppendChild(tileXML.CreateTextNode(userEmail));

        TileNotification tileNotif = new TileNotification(tileXML);
        TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotif); 
    }


    public async void Remove(ListBox display)
    {
        if (display.SelectedIndex > -1)
        {
            string id = ((Item)display.SelectedItem).Id;
            if (SecondaryTile.Exists(id))
            {
                SecondaryTile tile = new SecondaryTile(id);
                await tile.RequestDeleteAsync();
            }
            display.Items.RemoveAt(display.SelectedIndex);
        }
    }
}