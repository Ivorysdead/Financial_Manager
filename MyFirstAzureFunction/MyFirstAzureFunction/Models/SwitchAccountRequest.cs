namespace MyFirstAzureFunction.Models
{
    public class SwitchAccountRequest
    {
        public string UserId { get; set; }
        public int AccountId { get; set; }
    }
}