using System;
using System.Collections.Generic;
using System.Web.Services;
using ScottsJewels.Samples.Notepad.DataAccess;
using ScottsJewels.Samples.Notepad.DataModels;

namespace ScottsJewels.Samples.Notepad
{
    public partial class WebFormsNotepad : System.Web.UI.Page
    {
        protected List<Note> Model;

        protected void OnSearchAuthor(object sender, EventArgs e)
        {
            if (searchAuthor.Text != null && !string.IsNullOrEmpty(searchAuthor.Text.Trim()))
            {
                Model = NotepadDataAccess.GetInstance().GetNotesByAuthor(searchAuthor.Text);
            }
            else
            {
                Model = NotepadDataAccess.GetInstance().GetNotes();
            }
        }

        [WebMethod]
        static public void AddNote(Note note)
        {
            Console.WriteLine("Hello");
        }
    }
}