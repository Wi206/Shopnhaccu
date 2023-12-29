using musicShop.Models;



namespace musicShop.Logic
{
    public interface IMailLogic
    {
        Task SendEmail (MailInfo mailInfo);
        Task SendEmailDatHangThanhCong (DatHang datHang, MailInfo mailInfo);
    }
}
