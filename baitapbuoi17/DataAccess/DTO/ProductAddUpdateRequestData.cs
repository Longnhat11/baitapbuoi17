namespace baitapbuoi17.DataAccess.DTO
{
    public class ProductAddUpdateRequestData
    {
        public int ProductId { get; set; }  
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string GroupAttributesValue { get; set; }
        public string AttributesValue { get; set; }
    }
}
