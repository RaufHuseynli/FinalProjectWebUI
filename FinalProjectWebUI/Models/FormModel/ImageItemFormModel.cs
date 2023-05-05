using Microsoft.AspNetCore.Http;

namespace FinalProjectWebUI.Models.FormModel
{

    public class ImageItemFormModel
    {
        public int? Id { get; set; }
        public bool IsMain { get; set; }
        public string TempPath { get; set; }
        public IFormFile File { get; set; }
    }
}