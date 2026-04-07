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

int num_cases =0;
int num_pisos =0;
int num_terrenys =0;

int m2cases =0;
int m2pisos =0;
int m2terrenys =0;

foreach (var h in response)
{
   if ( cp == h.CodiPostal)
    {
        switch (h.TipusImmoble)
        {
            case "Casa":
            num_cases ++;
            m2cases += h.MetresQuadrats;
            break;
            case "Pis":
            num_pisos ++;
            m2pisos += h.MetresQuadrats;
            break;
            case "Terreny":
            num_terrenys ++;
            m2terrenys += h.MetresQuadrats;
            break;

        }
        
    } 

}

//Calcul quota a pagar, millor calculkar quota individual per habitatge dins el foreach

double quotaCasa = 0.998;
double quotaPis = 0.996;
double quotaTerreny = 0.136;


double quotaTitularCasa = m2cases *quotaCasa;
double quotaTitularPis = m2pisos *quotaPis;
double quotaTitularTerreny = m2terrenys *quotaTerreny;

