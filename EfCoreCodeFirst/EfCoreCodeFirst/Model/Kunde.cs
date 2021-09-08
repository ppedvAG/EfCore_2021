namespace EfCoreCodeFirst.Model
{
    public class Kunde : Person
    {
        public string Kundennummer { get; set; }
        public virtual Mitarbeiter Ansprechpartner { get; set; }
    }
}
