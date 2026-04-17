
using System.Net.Http.Json;

using System.Text.Json;

//o using HttpClient client

//http client
using var client = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5238")
};

    //client.Dispose();


/*Console.WriteLine("digues un CP");
string cp = Console.ReadLine()??"";
if(cp=="")
{
    cp="17472";
} NO CALEN segons el enunciat*/

Console.WriteLine("digues el teu poble");
string poble = Console.ReadLine()??"";
if(poble=="")
{
    poble="l'Armentera";
}
//busquem un poble per nom o CP
//per cada persona del poble recorrem tots els habitatges

//fem peticio a l API1

/*var response = await httpClient.GetAsync("url-aqui");
var json = await response.Content.ReadAsStringAsync();
Console.WriteLine(json);*/ 

var llistatHabitatgesPoble = await client.GetFromJsonAsync<List<Habitatge>>("/PoblacioLlistaHabitatges/" + poble); //Figueres i Joan
//fer bucle response que miri els cp, si cp igual sum a metros 

double descomptefamNumerosa = 0.9;
double recarrecMenors = 1.05;

//poble => persona => tots habitatges // Persona => habitages

//foreach contribuents del poble
foreach(var habitatge in llistatHabitatgesPoble!) //! per dirli que no sera null
{
    //var response = await client.GetFromJsonAsync<List<Habitatge>>("/Habitatges/" + habitatge.DNIContribuent); 
    // pq Claude diu que "+dni" ha de ser habitatge.DNIContribuent i no dni, 
    // pero la ruta al navegador es localhost:XXXX/habitages/dni? pq no dni que ho he demanat per cosnola abans
    
    int num_casesTot =0;
    int num_pisosTot =0;
    int num_terrenysTot =0;
    bool descomptetassa = false;

    int m2casesTot =0;
    int m2pisosTot =0;
    int m2terrenysTot =0;

    double quotatotal = 0;

    /* Console.WriteLine("digues un dni");
    string dni = Console.ReadLine()?? "";
    if(dni=="")
    {
        dni="12345678A";
    }

    var llistatHabitatgesPropietariPoble = await client.GetFromJsonAsync<List<Habitatge>>("/Habitatges/" + dni);*/

    var llistatContribuentsPoble = await client.GetFromJsonAsync<List<Habitatge>>("/ContribuentsMateixPoble/{Poblacio}" + poble);
    foreach (var d in llistatContribuentsPoble!) //! per dirli que no sera null
    {
        var dni = d.DNIContribuent; 
        if(dni == llistatHabitatgesPoble.DNIContribuent)
        {
            switch (d.TipusImmoble)
            {
                case TipusImmoble.Casa:
                num_casesTot ++;
                m2casesTot += d.MetresQuadrats;
                double preuM2Casa = 0.998;
                if(d.EsFamiliaNumerosa == true)
                    {
                        preuM2Casa = preuM2Casa*descomptefamNumerosa;
                        descomptetassa = true;
                    }
                if(d.NumMenorsImmoble>0)
                    {
                        preuM2Casa = preuM2Casa*recarrecMenors;
                    }
                quotatotal = quotatotal + d.MetresQuadrats*preuM2Casa;
                break;

                case TipusImmoble.Pis:
                num_pisosTot ++;
                m2pisosTot += d.MetresQuadrats;
                double preuM2Pis = 0.996;
                if(d.EsFamiliaNumerosa == true)
                    {
                        preuM2Pis = preuM2Pis*descomptefamNumerosa;
                        descomptetassa = true;
                    }
                quotatotal = quotatotal + d.MetresQuadrats*preuM2Pis;
                break;

                case TipusImmoble.Terreny:
                num_terrenysTot ++;
                m2terrenysTot += d.MetresQuadrats;
                double preuM2terreny = 0.136;
                quotatotal = quotatotal + d.MetresQuadrats*preuM2terreny;
                break;

            } 
        }  
        } 
                Console.WriteLine("............................");

                Console.WriteLine("CONSELL COMARCAL - Taxa Especial sobre els Edificis Comarcals (TEEC)");

                Console.WriteLine($"Nom:{habitatge.NomContribuent} " );
                Console.WriteLine($"Població:{habitatge.Poblacio} " );
                Console.WriteLine($"Cases: {num_casesTot} - {m2casesTot}" );
                Console.WriteLine($"Pisos: {num_pisosTot} - {m2pisosTot}" );
                Console.WriteLine($"Terrenys: {num_terrenysTot} - {m2terrenysTot}" );

                if (descomptetassa == true)
                    {
                        Console.WriteLine($"S'ha aplicat descompte per familia numerosa del 10%");
                    }

                Console.WriteLine($"Quota: {quotatotal}€" );

                Console.WriteLine("............................");

                    num_casesTot =0;
                    num_pisosTot =0;
                    num_terrenysTot =0;
                    descomptetassa = false;

                    m2casesTot =0;
                    m2pisosTot =0;
                    m2terrenysTot =0;

                    quotatotal = 0;
    }

/*API => Gnt del poble

foreach (var x in gent)
{
    API = cases de x
    foreach ()
    {
        if(!poble)continue;
    
    
    
    }
}*/








