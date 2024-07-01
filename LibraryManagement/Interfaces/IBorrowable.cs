using LibraryManagement.AbstractClasses;

namespace LibraryManagement.Interfaces;
public interface IBorrowable
{
    LibraryItem Borrow(string query);
    LibraryItem Return(string query);
}
