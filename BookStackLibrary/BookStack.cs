using System;
using System.Collections.Generic;

namespace BookStackLibrary;

public sealed class BookStack : IEquatable<BookStack>
{
    // вузол зберігає книгу та посилання на наступний елемент
    private sealed class Node
    {
        public Book Value { get; }
        public Node? Next { get; }

        public Node(Book value, Node? next)
        {
            Value = value;
            Next = next;
        }
    }

    private Node? _head;

    public int Count { get; private set; }
    public bool IsEmpty => _head is null;
    public Book? LastPopped { get; private set; }

    public Book this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(
                    nameof(index));

            Node? current = _head;
            for (int i = 0; i < index; i++)
                current = current!.Next;

            return current!.Value;
        }
    }

    public BookStack()
    {
    }

    public BookStack(IEnumerable<Book> books)
    {
        if (books is null)
            throw new ArgumentNullException(nameof(books));

        foreach (Book book in books)
            _ = this < book;
    }

    public BookStack(BookStack source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var values = new List<Book>();
        for (Node? node = source._head; node is not null;
             node = node.Next)
            values.Add(node.Value);

        for (int i = values.Count - 1; i >= 0; i--)
            _ = this < values[i];
    }

    // додає книгу на вершину стека
    public static bool operator <(
        BookStack stack, Book? value)
    {
        ArgumentNullException.ThrowIfNull(stack);
        if (value is null)
            return false;

        stack._head = new Node(value, stack._head);
        stack.Count++;
        stack.LastPopped = null;
        return true;
    }

    // вилучає вершину; книгу зберігає у LastPopped
    public static bool operator >(
        BookStack stack, Book? marker)
    {
        ArgumentNullException.ThrowIfNull(stack);
        _ = marker;

        if (stack._head is null)
        {
            stack.LastPopped = null;
            return false;
        }

        stack.LastPopped = stack._head.Value;
        stack._head = stack._head.Next;
        stack.Count--;
        return true;
    }

    // перевіряє, чи містить стек елементи
    public static bool operator true(BookStack? stack) =>
        stack is not null && !stack.IsEmpty;

    public static bool operator false(BookStack? stack) =>
        stack is null || stack.IsEmpty;

    public static implicit operator Book(BookStack stack)
    {
        ArgumentNullException.ThrowIfNull(stack);
        return stack._head?.Value
            ?? throw new InvalidOperationException(
                "Неможливо переглянути порожній стек.");
    }

    public static explicit operator int(BookStack stack)
    {
        ArgumentNullException.ThrowIfNull(stack);
        return stack.Count;
    }

    // порівнює вміст стеків від вершини донизу
    public bool Equals(BookStack? other)
    {
        if (other is null || Count != other.Count)
            return false;

        Node? left = _head;
        Node? right = other._head;
        while (left is not null && right is not null)
        {
            if (left.Value != right.Value)
                return false;
            left = left.Next;
            right = right.Next;
        }
        return left is null && right is null;
    }

    public override bool Equals(object? obj) =>
        obj is BookStack other && Equals(other);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        for (Node? node = _head; node is not null;
             node = node.Next)
            hash.Add(node.Value);
        return hash.ToHashCode();
    }

    public static bool operator ==(
        BookStack? left, BookStack? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        return left is not null && left.Equals(right);
    }

    public static bool operator !=(
        BookStack? left, BookStack? right) =>
        !(left == right);

    public override string ToString()
    {
        if (IsEmpty)
            return "Стек порожній";

        var lines = new List<string>();
        int index = 0;
        for (Node? node = _head; node is not null;
             node = node.Next)
            lines.Add($"{index++}: {node.Value}");

        return string.Join(Environment.NewLine, lines);
    }
}
