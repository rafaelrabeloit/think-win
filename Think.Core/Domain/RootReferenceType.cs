using System;
using System.Text;
using System.Collections.Generic;


namespace Think.Core.Domain {
    
    public class RootReferenceType {
        public RootReferenceType() {
			Quotes = new List<Quote>();
			ReferenceTypes = new List<ReferenceType>();
			RootQuotes = new List<RootQuote>();
			RootReferenceTypes = new List<RootReferenceType>();
        }
        public virtual int Id { get; set; }
        public virtual RootReferenceType RootReferenceTypeVal { get; set; }
        public virtual int Usable { get; set; }
        public virtual IList<Quote> Quotes { get; set; }
        public virtual IList<ReferenceType> ReferenceTypes { get; set; }
        public virtual IList<RootQuote> RootQuotes { get; set; }
        public virtual IList<RootReferenceType> RootReferenceTypes { get; set; }
    }
}
