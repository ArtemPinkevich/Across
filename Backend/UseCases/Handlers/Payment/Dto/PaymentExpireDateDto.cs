namespace UseCases.Handlers.Payment.Dto
{
    public class PaymentExpireDateDto
    {
        /// <summary>
        /// Срок действия в формате "DD.MM.YYYY"
        /// </summary>
        public string PaymentExpireDate { set; get; }
    }
}
