
using System.Net.Http.Json;

using System.Text.Json;

//o using HttpClient client

//http client
using var client = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5238")
};

    //client.Dispose();


/*Console.WriteLine("digues un dni");
string dni = Console.ReadLine()?? "";
if(dni=="")
{
    dni="12345678A";
}

Console.WriteLine("digues un CP");
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

var llistatHabitatges = await client.GetFromJsonAsync<List<Habitatge>>("/PoblacioLlistaHabitatges/" + poble); //Figueres i Joan
 
//fer bucle response que miri els cp, si cp igual sum a metros 

double descomptefamNumerosa = 0.9;
double recarrecMenors = 1.05;

//poble => persona => tots habitatges // Persona => habitages

//foreach contribuents del poble
foreach(var habitatge in llistatHabitatges!) //! per dirli que no sera null
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

    //lista habitatges de un mateix contribuent en un sol poble
    foreach (var h in response!) //! per dirli que no sera null
    {
            switch (h.TipusImmoble)
            {
                case TipusImmoble.Casa:
                num_casesTot ++;
                m2casesTot += h.MetresQuadrats;
                double preuM2Casa = 0.998;
                if(h.EsFamiliaNumerosa == true)
                    {
                        preuM2Casa = preuM2Casa*descomptefamNumerosa;
                        descomptetassa = true;
                    }
                if(h.NumMenorsImmoble>0)
                    {
                        preuM2Casa = preuM2Casa*recarrecMenors;
                    }
                quotatotal = quotatotal + h.MetresQuadrats*preuM2Casa;
                break;

                case TipusImmoble.Pis:
                num_pisosTot ++;
                m2pisosTot += h.MetresQuadrats;
                double preuM2Pis = 0.996;
                if(h.EsFamiliaNumerosa == true)
                    {
                        preuM2Pis = preuM2Pis*descomptefamNumerosa;
                        descomptetassa = true;
                    }
                quotatotal = quotatotal + h.MetresQuadrats*preuM2Pis;
                break;

                case TipusImmoble.Terreny:
                num_terrenysTot ++;
                m2terrenysTot += h.MetresQuadrats;
                double preuM2terreny = 0.136;
                quotatotal = quotatotal + h.MetresQuadrats*preuM2terreny;
                break;

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
    }

    








