using Microsoft.AspNetCore.Http;

namespace app.Models.ViewModel
{
    public class FileUploadApi
    {
        public IFormFile files {get;set;}
        public string UserId {get;set;}
    }
}