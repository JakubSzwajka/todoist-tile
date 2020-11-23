using System;

public class TodoistItem
{
    String id;
    String content;
    DateTime dateAdded;
    DateTime dueDate;
    String colour; 

    public TodoistItem( String id, String content, DateTime dateAdded, DateTime dueDate)
    {
        this.id = id;
        this.content = content;
        this.dateAdded = dateAdded;
        this.dueDate = dueDate;
        this.colour = "004E2E2E"; 
    }

    public String getContent()
    {
        return this.content; 
    }

    public String getId()
    {
        return this.id;
    }

    public String getColour()
    {
        return this.colour; 
    }
}