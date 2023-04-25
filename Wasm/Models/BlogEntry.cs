namespace Lab6.Wasm.Models;

public class BlogEntry
{
    public uint BlogEntryId { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime PostedTime { get; set; } = DateTime.Now;

}