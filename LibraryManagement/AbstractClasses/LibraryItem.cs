namespace LibraryManagement.AbstractClasses;
public abstract class LibraryItem
{
    public abstract string ISBN { get; set; }
    public abstract string Title { get; set; }
    public abstract string Author { get; set; }
    public abstract bool IsAvailable { get; set; }
    public abstract DateOnly PublicationYear { get; set; }
    public abstract string Language { get; set; }
    public abstract string SmallDescribtion { get; set; }
}
