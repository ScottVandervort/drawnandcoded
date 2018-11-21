using System.Collections.Generic;
using System.Linq;
using ScottsJewels.Samples.Notepad.DataModels;

namespace ScottsJewels.Samples.Notepad.DataAccess
{
    public class NotepadDataAccess : INotepadDataAccess
    {
        private static readonly List<Note> _notes;

        static NotepadDataAccess()
        {
            _notes = new List<Note>();
        }

        private NotepadDataAccess()
        { }

        static public NotepadDataAccess GetInstance()
        {
            return new NotepadDataAccess();
        }

        /// <summary>
        /// Gets a list of notes.
        /// </summary>
        /// <returns>A list of notes.</returns>
        public List<Note> GetNotes()
        {
            return _notes;
        }

        /// <summary>
        /// Gets a list of notes for the specified author.
        /// </summary>
        /// <param name="author">The author whose notes to return.</param>
        /// <returns>A list of notes by the specified author.</returns>
        public List<Note> GetNotesByAuthor(string author)
        {
            List<Note> result;

            if (author == null)
            {
                result = new List<Note>();
            }
            else
            {
                result = _notes.Where(x => (x.Author == null ? string.Empty : x.Author.Trim().ToUpper()) == author.Trim().ToUpper()).ToList();
            }

            return result;
        }

        /// <summary>
        /// Submits a new note.
        /// </summary>
        /// <param name="note">The note.</param>
        public void SubmitNote(Note note)
        {
            _notes.Add(note);
        }
    }
}
