namespace ECommerce.Web.Service.IService
{
    public interface ITokenProvider
    {
        void SetToken(string token); // Token'i ekle
        string? GetToken(); // Token'i getir
        void ClearToken(); // Token'i sil
    }
}
