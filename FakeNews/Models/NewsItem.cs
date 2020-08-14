﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;

namespace FakeNews.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Headline { get; set; }
        public string Subhead { get; set; }
        public string DateLine { get; set; }
        public string Image { get; set; }
    }

    public class NewsManager
    {

        public static void GetNews(string category, ObservableCollection<NewsItem> newsItems)
        {
            var allItems = getNewsItems();
            var fillteredNewsItems = allItems.Where(p => p.Category == category).ToList();
            newsItems.Clear();
            fillteredNewsItems.ForEach(p => newsItems.Add(p));
        }
        private static List<NewsItem> getNewsItems()
        {
            var items = new List<NewsItem>();
            items.Add(new NewsItem()
            {
                Id = 1,
                Category = "Financial",
                Headline = "Lorem Ipsum",
                Subhead = " doro sit amet",
                DateLine = "Nunc tristique nec",
                Image = "Assets/Financial.png"
            });
            items.Add(new NewsItem()
            {
                Id = 2,
                Category = "Financial",
                Headline = "etiam ac felis viverra",
                Subhead = " vulputate nils ac, aliquet nisi",
                DateLine = "tortor porttitior , eu fermentum ante conque ",
                Image = "Assets/Financial2.png"
            });
            items.Add(new NewsItem()
            {
                Id = 3,
                Category = "Financial",
                Headline = "Interger sed  turpis erat",
                Subhead = "Sed quis hendtrerit lorem , quis interdum dolor ",
                DateLine = " in viverra metus facilisis sed",
                Image = "Assets/Financial3.png"
            });
            items.Add(new NewsItem()
            {
                Id = 4,
                Category = "Financial",
                Headline = "Proin sem neque ",
                Subhead = "aliquet quis ipsum tincidunt ",
                DateLine = "nterger eleifend ",
                Image = "Assets/Financial4.png"
            });
            items.Add(new NewsItem()
            {
                Id = 5,
                Category = "Financial",
                Headline = "Mauris bibendum non leo vitae tempor",
                Subhead = " In nisi tostor , eleifend sed ipsum eget",
                DateLine = " Curabiur dictum augue vitae elementum ultrices",
                Image = "Assets/Financial5.png"
            });
            items.Add(new NewsItem()
            {
                Id = 6,
                Category = "Food",
                Headline = "Lorem Ipsum",
                Subhead = " doro sit amet",
                DateLine = "Nunc tristique nec",
                Image = "Assets/Food1.png"
            });
            items.Add(new NewsItem()
            {
                Id = 7,
                Category = "Financial",
                Headline = "etiam ac felis viverra",
                Subhead = " vulputate nils ac, aliquet nisi",
                DateLine = "tortor porttitior , eu fermentum ante conque ",
                Image = "Assets/Food2.png"
            });
            items.Add(new NewsItem()
            {
                Id = 8,
                Category = "Financial",
                Headline = "Interger sed  turpis erat",
                Subhead = "Sed quis hendtrerit lorem , quis interdum dolor ",
                DateLine = " in viverra metus facilisis sed",
                Image = "Assets/Food3.png"

            });
            items.Add(new NewsItem()
            {
                Id = 9,
                Category = "Financial",
                Headline = "Proin sem neque ",
                Subhead = "aliquet quis ipsum tincidunt ",
                DateLine = "nterger eleifend ",
                Image = "Assets/Food4.png"
            });
            items.Add(new NewsItem()
            {
                Id = 10,
                Category = "Financial",
                Headline = "Mauris bibendum non leo vitae tempor",
                Subhead = " In nisi tostor , eleifend sed ipsum eget",
                DateLine = " Curabiur dictum augue vitae elementum ultrices",
                Image = "Assets/Food5.png"
            });
            return items;

        }
    }
}
