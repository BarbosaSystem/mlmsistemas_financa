namespace app.Models.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        
        /* public ChangePasswordViewModel(string id, string oldPassowrd, string newPassword, string confirmPassord)
        {
            if(isValid(id)){
                this.Id = id
            }
            if(isValid(oldPassowrd)){
                this.OldPassword = oldPassowrd
            }
            if(isValid(newPassword) && isValid(confirmPassord)){
                if(CheckValues(newPassword, confirmPassord))
                this.ConfirmPassword = confirmPassord;
                this.NewPassword = newPassword
            }
        }

        public static bool isValid(string Id){
            return Id.length > 0;
        }
        public static bool CheckValues(string pass, string confirm){
            return pass == confirm;
        } */
    }
}