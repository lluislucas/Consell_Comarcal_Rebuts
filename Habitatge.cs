class Habitatge
{
   public int Identificador {get; set;}
    public string Adreca {get; set;}
    public string Poblacio {get; set;}
    public int CodiPostal {get; set;}
    public TipusImmoble TipusImmoble {get; set;}
    public int MetresQuadrats {get; set;}
    public int HabitantsImmoble {get; set;}
    public int NumMenorsImmoble {get; set;}
    public string? NomContribuent {get; set;}
    public string? DNIContribuent {get; set;}
    public bool EsFamiliaNumerosa  
    {
        get { return HabitantsImmoble > 5; }
    } 
}