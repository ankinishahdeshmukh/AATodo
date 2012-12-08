using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AATodo
{
    [DataContract]
    public class Note
    {
        private readonly DateTime createdDate;
        private readonly IList<Entry> noteEntries;
        
        public string Name
        {
            set;
            get;
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
        }

        public bool IsCompleted
        {
            // TODO : use linq and iterate over the list
            get;
            set;
        }

        public IList<Entry> NoteEntries
        {
            get { return this.noteEntries; }
        }
        
        public Note(string name)
        {
            this.Name = name;
            this.noteEntries = new List<Entry>();
            this.createdDate = DateTime.Now;
        }
    }
}
