using System;
using System.Collections.Generic;

namespace Laboratory.LinqTest
{

    public class Publisher
    {
        public string Name { get; set; }
    }

    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Subject
    {
        public string Name { get; set; }
    }

    public class Book
    {
        public string Title { get; set; }
        public Publisher Publisher { get; set; }
        public List<Author> Authors { get; set; }
        public int PageCount { get; set; }
        public decimal Price { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Isbn { get; set; }
        public Subject Subject { get; set; }
    }

    public static class SampleData
    {
        static SampleData()
        {
            Publishers = new List<Publisher>() { 
                new Publisher {Name="FunBooks"},
                new Publisher {Name="Joe Publishing"},
                new Publisher {Name="I Publisher"}
            };

            Authors = new List<Author>() { 
                new Author {FirstName="Johnny", LastName="Good"},
                new Author {FirstName="Graziella", LastName="Simplegame"},
                new Author {FirstName="Octavio", LastName="Prince"},
                new Author {FirstName="Jeremy", LastName="Legrand"}
            };

            Subjects = new List<Subject>() {
                new Subject {Name="Software development"},
                new Subject {Name="Novel"},
                new Subject {Name="Science fiction"}
            };

            Books = new List<Book>() { 
                new Book {
                    Title="Funny Stories",
                    Publisher=Publishers[0],
                    Authors=new List<Author>(){Authors[0], Authors[1]},
                    PageCount=101,
                    Price=25.55M,
                    PublicationDate=new DateTime(2004, 11, 10),
                    Isbn="0-000-77777-2",
                    Subject=Subjects[0]
                },
                new Book {
                    Title="LINQ rules",
                    Publisher=Publishers[1],
                    Authors=new List<Author>() {Authors[2]},
                    PageCount=300,
                    Price=12M,
                    PublicationDate=new DateTime(2007, 9, 2),
                    Isbn="0-111-77777-2",
                    Subject=Subjects[0]
                },
                new Book {
                    Title="C# on Rails",
                    Publisher=Publishers[1],
                    Authors=new List<Author>() {Authors[2]},
                    PageCount=256,
                    Price=35.5M,
                    PublicationDate=new DateTime(2007, 4, 1),
                    Isbn="0-222-77777-2",
                    Subject=Subjects[0]
                },
                new Book {
                    Title="All your base are belong to us",
                    Publisher=Publishers[1],
                    Authors=new List<Author>() {Authors[3]},
                    PageCount=1205,
                    Price=35.5M,
                    PublicationDate=new DateTime(2006, 5, 5),
                    Isbn="0-333-77777-2",
                    Subject=Subjects[2]
                },
                new Book {
                    Title="Bonjour mon Amour",
                    Publisher=Publishers[0],
                    Authors=new List<Author>() {Authors[1], Authors[0]},
                    PageCount=50,
                    Price=29M,
                    PublicationDate=new DateTime(1973, 2, 18),
                    Isbn="2-444-77777-2",
                    Subject=Subjects[1]
                }
            };
        }

        public static List<Publisher> Publishers { get; set; }
        public static List<Author> Authors { get; set; }
        public static List<Subject> Subjects { get; set; }
        public static List<Book> Books { get; set; }
    }
}
