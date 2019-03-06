using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootAuthor {
        public RootAuthor() {
			Authors = new List<Author>();
			Quotes = new List<Quote>();
			RootAuthors = new List<RootAuthor>();
			RootAuthorImages = new List<RootAuthorImage>();
			RootQuotes = new List<RootQuote>();
        }
        public virtual int Id { get; set; }
        public virtual Language Language { get; set; }
        public virtual RootAuthor RootAuthorVal { get; set; }
        public virtual User User { get; set; }
        public virtual int? Fictional { get; set; }
        public virtual IList<Author> Authors { get; set; }
        public virtual IList<Quote> Quotes { get; set; }
        public virtual IList<RootAuthor> RootAuthors { get; set; }
        public virtual IList<RootAuthorImage> RootAuthorImages { get; set; }
        public virtual IList<RootQuote> RootQuotes { get; set; }
    }
}
