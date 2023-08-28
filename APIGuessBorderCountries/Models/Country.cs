namespace APIGuessBorderCountries.Models
{
    public class Flags
    {
        public string png { get; set; }
        public string svg { get; set; }
        public string alt { get; set; }
    }

    public class NativeName
    {
        public Official official { get; set; }
        public string common { get; set; }
    }

    public class Official
    {
        public string official { get; set; }
        public string common { get; set; }
    }

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }
        public NativeName nativeName { get; set; }
    }

    public class Root
    {
        public Flags flags { get; set; }
        public Name name { get; set; }
        public string cca3 { get; set; }
        public string cioc { get; set; }
        public List<string> borders { get; set; }
    }
}
