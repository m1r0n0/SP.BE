namespace SP.Customer.API.ViewModels
{
    public class ModelErrorVM
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Errors Errors { get; set; }
    }

    public class Errors
    {
        public string[] ExampleProperty { get; set; }
    }
}
