namespace EfCoreCodeFirst.Model
{
    public class Kunde : Person
    {
        public string Kundennummer { get; set; }
        public Mitarbeiter Ansprechpartner { get; set; }
    }
}
