using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootQuote {
        public RootQuote() {
			Quotes = new List<Quote>();
			RootQuoteReferences = new List<RootQuoteReference>();
			UserQuotes = new List<UserQuote>();
        }
        public virtual int Id { get; set; }
        public virtual RootCategory RootCategory { get; set; }
        public virtual RootReferenceType RootReferenceType { get; set; }
        public virtual RootReference RootReference { get; set; }
        public virtual RootAuthor RootAuthor { get; set; }
        public virtual Language Language { get; set; }
        public virtual User User { get; set; }
        public virtual string Status { get; set; }
        public virtual IList<Quote> Quotes { get; set; }
        public virtual IList<RootQuoteReference> RootQuoteReferences { get; set; }
        public virtual IList<UserQuote> UserQuotes { get; set; }
    }
}
