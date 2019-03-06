using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootReference {
        public RootReference() {
			Quotes = new List<Quote>();
			References = new List<Reference>();
			RootQuotes = new List<RootQuote>();
			RootQuoteReferences = new List<RootQuoteReference>();
        }
        public virtual int Id { get; set; }
        public virtual IList<Quote> Quotes { get; set; }
        public virtual IList<Reference> References { get; set; }
        public virtual IList<RootQuote> RootQuotes { get; set; }
        public virtual IList<RootQuoteReference> RootQuoteReferences { get; set; }
    }
}
