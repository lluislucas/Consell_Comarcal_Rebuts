using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

//o using HttpClient client

//http client
using var client = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5238")
};

    //client.Dispose();


Console.WriteLine("digues un dni");
string dni = Console.ReadLine();
if(dni=="")
{
    dni="12345678A";
}

Console.WriteLine("digues un CP");
string cp = Console.ReadLine();
if(cp=="")
{
    cp="17472";
}


//fem peticio a l API1
var response = await client.GetFromJsonAsync<List<Habitatge>>("/Habitatges/" + dni);


//fer bucle response que miri els cp, si cp igual sum a metros 

int num_casesTot =0;
int num_pisosTot =0;
int num_terrenysTot =0;

int m2casesTot =0;
int m2pisosTot =0;
int m2terrenysTot =0;

double descomptefamNumerosa = 0.9;
double recarrecMenors = 1.05;

double quotaCases = 0;
double quotaPisos = 0;
double quotaTerrenys = 0;


foreach (var h in response)
{
    bool descomptetassa = false;
   if ( cp == h.CodiPostal )
    {
        switch (h.TipusImmoble)
        {
            case "Casa":
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
             quotaCases = h.MetresQuadrats*preuM2Casa;
            break;

            case "Pis":
            num_pisosTot ++;
            m2pisosTot += h.MetresQuadrats;
            double preuM2Pis = 0.996;
             if(h.EsFamiliaNumerosa == true)
                {
                    preuM2Pis = preuM2Pis*descomptefamNumerosa;
                     descomptetassa = true;
                }
            quotaPisos = h.MetresQuadrats*preuM2Pis;
            break;

            case "Terreny":
            num_terrenysTot ++;
            m2terrenysTot += h.MetresQuadrats;
            double preuM2terreny = 0.136;
            quotaTerrenys = h.MetresQuadrats*preuM2terreny;
            break;

        }

        
    } 

}

//Calcul quota a pagar




