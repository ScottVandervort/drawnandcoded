using System.Collections.Generic;
using ScottsJewels.Samples.Notepad.DataModels;

namespace ScottsJewels.Samples.Notepad.DataAccess
{
    public interface INotepadDataAccess
    {
        /// <summary>
        /// Gets a list of notes.
        /// </summary>
        /// <returns>A list of notes.</returns>
        List<Note> GetNotes();

        /// <summary>
        /// Gets a list of notes for the specified author.
        /// </summary>
        /// <param name="author">The author whose notes to return.</param>
        /// <returns>A list of notes by the specified author.</returns>
        List<Note> GetNotesByAuthor(string author);

        /// <summary>
        /// Submits a new note.
        /// </summary>
        /// <param name="note">The note.</param>
        void SubmitNote(Note note);
    }
}
