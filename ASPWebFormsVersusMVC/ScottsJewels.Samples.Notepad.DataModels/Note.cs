using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ScottsJewels.Samples.Notepad.DataModels
{
    [Serializable]
    public class Note
    {
        [Required]
        [DisplayName("Author")]
        public string Author { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Text")]
        public string Text { get; set; }
    }
}