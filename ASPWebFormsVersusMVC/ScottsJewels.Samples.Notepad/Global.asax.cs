using System.Web.Mvc;
using System.Web.Routing;

namespace ScottsJewels.Samples.Notepad
{
    public class Notepad : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "MvcNotesIndex",
                "Notepad/MvcNotepad",
                new { controller = "Notepad", action = "IndexNote" });

            routes.MapRoute(
                "MvcGetNotesByAuthor",
                "Notepad/MvcGetNotesByAuthor",
                new { controller = "Notepad", action = "GetNotes" });

            routes.MapPageRoute(
                "WebFormsNotesIndex",
                "Notepad/WebFormsNotepad",
                "~/WebFormsNotepad.aspx");

            routes.MapRoute(
                "AjaxAddNote",
                "Notepad/AddNote",
                new { controller = "Notepad", action = "AddNote" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}