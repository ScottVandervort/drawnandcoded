using System.Web.Mvc;
using ScottsJewels.Samples.Notepad.DataAccess;
using ScottsJewels.Samples.Notepad.DataModels;
using ScottsJewels.Services;

namespace ScottsJewels.Samples.Notepad.Controllers
{
    // All Controllers must be named with the Controller suffix.    
    public class NotepadController : Controller
    {
        [HttpGet]
        public ActionResult IndexNote()
        {
            return View("Notepad");
        }


        [HttpPost]
        public ActionResult GetNotes(string button, string searchAuthor)
        {
            ActionResult result;

            switch (button)
            {
                case "GetNotesByAuthor":

                    if (searchAuthor != null && !string.IsNullOrEmpty(searchAuthor.Trim()))
                    {
                        result = View("Notepad", NotepadDataAccess.GetInstance().GetNotesByAuthor(searchAuthor));
                    }
                    else
                    {
                        result = View("Notepad", NotepadDataAccess.GetInstance().GetNotes());
                    }

                    break;

                default:

                    result = IndexNote();

                    break;
            }

            return result;
        }

        [HttpPost]
        public JsonResult AddNote(Note note)
        {
            JsonResult result = new JsonResult();

            if ((!string.IsNullOrEmpty(note.Author)) &&
                (!string.IsNullOrEmpty(note.Title)) &&
                (!string.IsNullOrEmpty(note.Text)))
            {
                NotepadDataAccess.GetInstance().SubmitNote(
                    new Note
                    {
                        Author = note.Author,
                        Text = note.Text,
                        Title = note.Title
                    });

                result.Data = new ClientResponse<bool>(true, true, string.Empty);
            }
            else
            {
                result.Data = new ClientResponse<bool>(false, false, "Author, Title, and Text must not be empty!");
            }

            return result;
        }
    }
}