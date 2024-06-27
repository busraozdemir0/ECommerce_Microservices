namespace ECommerce.Web.Models
{
    public class CartDTO // Sepeti barindiriyor
    {
        public CartHeaderDTO CartHeader { get; set; } // Bir sepet bir kisiye ait olacagi icin bu nesnede sepetin kullanicisinin bilgileri yer almaktadir.
        public IEnumerable<CartDetailsDTO>? CartDetails { get; set; } // Bir sepette kac adet urun varsa listelenecek
    }
}
