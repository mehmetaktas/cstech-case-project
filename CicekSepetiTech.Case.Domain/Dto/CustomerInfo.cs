namespace CicekSepetiTech.Case.Domain.Dto
{
    public class CustomerInfo
    {
        /// <summary>
        /// Login olmuş ya da id'si bilinen müşteri için bu alan doldurulmalıdır.
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Login olmamış müşteri için cookie'e yazılan kod gönderilmelidir.
        /// Detay: Müşteri siteye girdiği andan itibaren ona özel oluşturulup cookie'ye yazılan bir kod var. Bu cookie kodu sayesinde müşterinin hareketlerini izleyebiliyoruz. Mesela sepete ekle yaptı ve siteden ayrıldı. Tekrar geldiği zaman sepetini hatırlayabiliyoruz. Müşteri login olana kadar işlemleri cookie koduyla tutuluyor. Login olduğu andan itibaren verileri CustomerId değeriyle eşleştiriliyor.
        /// </summary>
        public string CustomerCode { get; set; }
    }
}