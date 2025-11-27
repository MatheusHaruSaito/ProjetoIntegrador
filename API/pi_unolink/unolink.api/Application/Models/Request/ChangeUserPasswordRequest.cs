namespace unolink.api.Application.Models.Request
{
    public class ChangeUserPasswordRequest
    {
        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }



    }
}
