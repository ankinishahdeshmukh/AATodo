using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AATodo
{
    [DataContract]
    public class Entry
    {
        private readonly Guid id;
        private readonly DateTime dateCreated;

        public Guid Id
        {
            get { return this.id; }
        }

        public string Value
        {
            get;
            set;
        }

        public DateTime DateCreated
        {
            get { return dateCreated; }
        }

        public bool IsCompleted
        {
            get;
            set;
        }

        public Entry(Guid id, string value)
        {
            this.id = id;
            this.Value = value;
            this.dateCreated = DateTime.Now;
        }
    }
}