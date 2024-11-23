namespace Entities;

public class Payment : EntityBase
{
    public string UserId { set; get; }
    public User User { set; get; }
    
    public string Amount { set; get; }
    
    public string PaymentDate { set; get; }
    
    public string PaymentExpireDate { set; get; }
}