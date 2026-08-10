namespace AssignmentManagement.IRepository
{
    public interface IEmailService
    {
        Task SendVerificationEmail(string email,string name,string verificationLink);
    }
}
