namespace SampleApplication.Models
{
    public class SimpleReportViewModel
    {
        public int Quantity { get; set; }
        public string DimensionOne { get; set; }
    }

    public class StackedViewModel
    {
        public string StackedDimensionOne { get; set; }

        public List<SimpleReportViewModel> LstData { get; set; }
    }
}