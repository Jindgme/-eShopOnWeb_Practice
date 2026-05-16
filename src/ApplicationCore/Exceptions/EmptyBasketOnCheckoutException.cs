namespace ApplicationCore.Exceptions
{
    public class EmptyBasketOnCheckoutException:Exception
    {
        public EmptyBasketOnCheckoutException()
            :base("结账时购物车不能为空。")
        {
        }
        public EmptyBasketOnCheckoutException(string message):base(message)
        {
            
        }
        public EmptyBasketOnCheckoutException(string message,Exception exception):base(message,exception)
        {
            
        }
    }
}
