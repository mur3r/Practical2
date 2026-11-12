using System;

namespace BookStackLibrary;

public sealed class Book : IEquatable<Book>
{
    // властивості зберігають дані книги
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }

    public Book()
        : this("Невідома назва", "Невідомий автор", 0)
    {
    }

    public Book(string title, string author, int year)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Потрібно вказати назву.", nameof(title));
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException(
                "Потрібно вказати автора.", nameof(author));

        Title = title.Trim();
        Author = author.Trim();
        Year = year;
    }

    public Book(Book other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        Title = other.Title;
        Author = other.Author;
        Year = other.Year;
    }

    // зчитує дані книги з консолі
    public static Book ReadFromConsole()
    {
        Console.Write("Назва книги: ");
        string title = Console.ReadLine() ?? string.Empty;
        Console.Write("Автор: ");
        string author = Console.ReadLine() ?? string.Empty;
        Console.Write("Рік видання: ");
        string yearText = Console.ReadLine()
            ?? string.Empty;

        if (!int.TryParse(yearText, out int year))
            throw new FormatException(
                "Рік має бути цілим числом.");

        return new Book(title, author, year);
    }

    public override string ToString() =>
        $"{Title} — {Author} ({Year})";

    public bool Equals(Book? other) =>
        other is not null
        && string.Equals(Title, other.Title,
            StringComparison.OrdinalIgnoreCase)
        && string.Equals(Author, other.Author,
            StringComparison.OrdinalIgnoreCase)
        && Year == other.Year;

    public override bool Equals(object? obj) =>
        obj is Book other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(
            StringComparer.OrdinalIgnoreCase.GetHashCode(Title),
            StringComparer.OrdinalIgnoreCase.GetHashCode(Author),
            Year);

    public static bool operator ==(Book? left, Book? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        return left is not null && left.Equals(right);
    }

    public static bool operator !=(
        Book? left, Book? right) =>
        !(left == right);
}
