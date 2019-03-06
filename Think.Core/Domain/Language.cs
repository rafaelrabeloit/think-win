using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class Language {
        public Language() {
			Authors = new List<Author>();
			Categories = new List<Category>();
			Quotes = new List<Quote>();
			References = new List<Reference>();
			ReferenceTypes = new List<ReferenceType>();
			RootAuthors = new List<RootAuthor>();
			RootQuotes = new List<RootQuote>();
			Users = new List<User>();
        }
        public virtual int Id { get; set; }
        public virtual string Lang { get; set; }
        public virtual IList<Author> Authors { get; set; }
        public virtual IList<Category> Categories { get; set; }
        public virtual IList<Quote> Quotes { get; set; }
        public virtual IList<Reference> References { get; set; }
        public virtual IList<ReferenceType> ReferenceTypes { get; set; }
        public virtual IList<RootAuthor> RootAuthors { get; set; }
        public virtual IList<RootQuote> RootQuotes { get; set; }
        public virtual IList<User> Users { get; set; }
    }
}
