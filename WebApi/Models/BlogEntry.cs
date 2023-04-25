namespace Lab6.WebApi.Models;

public class BlogEntry
{
    public uint BlogEntryId { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime PostedTime { get; set; } = DateTime.Now;

}