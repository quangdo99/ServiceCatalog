namespace MongoExample.Models
{
    public class MessageProduct
    {
        public string code { get; set; }
        public int price { get; set; }
        /// <summary>
        /// 0: create, 1: update, -1: delete
        /// </summary>
        public int status { get; set; }
        public MessageProduct(string code, int price, int status)
        {
            this.code = code;
            this.price = price;
            this.status = status;
        }

        public MessageProduct(Product product)
        {
            code = product.code;
            price = product.price;
            status = product.status;
        }
    }
}
