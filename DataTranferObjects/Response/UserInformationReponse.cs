namespace DataTranferObjects.Response;

public class UserInformationReponse
{
    public Guid UserId { get; set; }
    public string[] UserRole { get; set; } = null!;
    
    public string Token { get; set; }

}