using System;
using System.Text;
using BookStackLibrary;

Console.OutputEncoding = Encoding.UTF8;

// демонструє основні операції зі стеком
BookStack stack = new();
Console.WriteLine("Додавання книг оператором <:");
Console.WriteLine("  Кобзар: " +
    (stack < new Book("Кобзар", "Тарас Шевченко", 1840)
        ? "додано" : "не додано"));
Console.WriteLine("  Лісова пісня: " +
    (stack < new Book("Лісова пісня", "Леся Українка", 1911)
        ? "додано" : "не додано"));
Console.WriteLine("  The Hobbit: " +
    (stack < new Book(
        "The Hobbit", "J. R. R. Tolkien", 1937)
        ? "додано" : "не додано"));

Console.WriteLine();
Console.WriteLine("Стек (від вершини донизу):");
Console.WriteLine(stack);

Book top = stack;
Console.WriteLine("Перегляд вершини без вилучення: " + top);
Console.WriteLine("Елемент з індексом [1]: " + stack[1]);
Console.WriteLine("Кількість елементів: " + (int)stack);
if (stack)
    Console.WriteLine(
        "Перевірка operator true: стек не порожній.");

bool removed = stack > null;
Console.WriteLine("Оператор > вилучив елемент: " +
    (removed ? "так" : "ні"));
Console.WriteLine("Вилучений об'єкт: " + stack.LastPopped);
Console.WriteLine("Нова вершина: " + (Book)stack);

BookStack copy = new(stack);
Console.WriteLine(
    "Копія дорівнює стеку: " + (stack == copy));
Console.WriteLine(
    "Копія не дорівнює стеку: " + (stack != copy));

BookStack empty = new();
if (empty)
    Console.WriteLine("Порожній стек має елементи.");
else
    Console.WriteLine(
        "Перевірка порожнього стека: елементів немає.");
Console.WriteLine("Вилучення з порожнього стека: " +
    (empty > null ? "так" : "ні"));
