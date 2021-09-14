namespace UberApp.Data
{
    public class Marker
    {
        public string Description { get; set; }

        //According to Google Maps, X is actually the second number, while Y is the first?
        public double X { get; set; }

        public double Y { get; set; }

        public bool ShowPopup { get; set; }
    }
}
