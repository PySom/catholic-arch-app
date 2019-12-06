using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LagosArch.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public string Image { get; set; }
        public string Date { get; set; }
    }

    public class NewsPage
    {
        public News News { get; set; }
        public ObservableCollection<News> Others { get; set; }
    }
}
