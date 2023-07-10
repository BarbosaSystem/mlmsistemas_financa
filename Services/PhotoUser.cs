using System;

namespace app.Services
{
    public static class PhotoUser
    {
        public async static System.Threading.Tasks.Task<string> GetPhotoAsync(string userId)
        {
            if (System.IO.File.Exists("Upload/" + userId + ".jpg"))
            {
                return "data:image/jpeg;base64," + Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("Upload/" + userId + ".jpg"));
            }
            else{
                return "data:image/jpeg;base64," + Convert.ToBase64String(await System.IO.File.ReadAllBytesAsync("Upload/user.png"));
            }
        }
    }
}