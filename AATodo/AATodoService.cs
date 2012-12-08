using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace AATodo
{
    // GET  : read operation when resource name is known
    // PUT  : write operation when the resource name is known
    // POST : write operation when the actual value for resource is assigned by the service
    
    [ServiceContract]
    public interface IAATodoService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Note/Create?name={name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateNote(string name);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Note/{noteName}/Create?value={value}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Guid CreateEntry(string noteName, string value);
    }
    
    [ServiceBehavior(ConcurrencyMode=ConcurrencyMode.Single, InstanceContextMode=InstanceContextMode.Single)]
    public class AATodoService : IAATodoService
    {
        private static string StoreLocation = @"C:\temp\AAStore\";
        private static string FileExtension = ".txt";
        private readonly IList<Note> noteList;
        public AATodoService()
        {
            // TODO : Read from store and hydrate the state
            this.noteList = new List<Note>();
        }

        public string CreateNote(string name)
        {
            Note note = new Note(name);
            noteList.Add(note);
            
            string filePath = Path.Combine(AATodoService.StoreLocation, note.Name + AATodoService.FileExtension);
            if(!File.Exists(filePath))
            {
                File.Create(filePath);
            }

            return "Successfully created Note - " + name;
        }

        public void EditNote(string originalName, string newName)
        {
            foreach (Note note in noteList)
            {
                if (note.Name.Equals(originalName))
                {
                    note.Name = newName;
                    break;
                }
            }
        }

        public void DeleteNote(string name)
        {
            foreach (Note note in noteList)
            {
                if (note.Name.Equals(name))
                {
                    noteList.Remove(note);
                    break;
                }
            }
        }

        public Note GetNote(string name)
        {
            foreach (Note note in noteList)
            {
                if (note.Name.Equals(name))
                {
                    return note;
                }
            }
            return null;
        }

        public Guid CreateEntry(string noteName, string value)
        {
            Guid id = new Guid();
            Entry entry = new Entry(id, value);
            Note note = GetNote(noteName);

            note.NoteEntries.Add(entry);
            //// TODO : Find better way to flush contents to store
            string filePath = Path.Combine(AATodoService.StoreLocation, note.Name + AATodoService.FileExtension);
            File.Delete(filePath);
            note.NoteEntries.ForEach((e) => { File.AppendAllText(filePath, e.Value + Environment.NewLine); });

            return id;
        }

        public void EditEntry(string noteName, Guid id, string value)
        {
            Note note = GetNote(noteName);
            foreach (Entry entry in note.NoteEntries)
            {
                if (entry.Id == id)
                {
                    entry.Value = value;
                }
            }
        }

        public void DeleteEntry(string noteName, Guid id)
        {
            Note note = GetNote(noteName);
            foreach (Entry entry in note.NoteEntries)
            {
                if (entry.Id == id)
                {
                    note.NoteEntries.Remove(entry);
                }
            }
        }

        public Entry GetEntry(string noteName, Guid id)
        {
            Note note = GetNote(noteName);

            foreach (Entry entry in note.NoteEntries)
            {
                if (entry.Id == id)
                {
                    return entry;
                }                
            }
            return null;
        }

        public IList<Entry> GetAllEntries(string noteName)
        {
            Note note = GetNote(noteName);
            return note.NoteEntries;
        }
    }
}
