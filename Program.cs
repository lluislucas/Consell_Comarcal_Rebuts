using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

//o using HttpClient client

//http client
HttpClient client = new()
{
    BaseAddress = new Uri("http://localhost:5238")
};

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


//fem peticio
var response = await client.GetFromJsonAsync<List<Habitatge>>("/Habitatges/" + dni);

Console.WriteLine(response);


//fer bucle response que miri els cp, si cp igual sum a metros 

foreach (var h in habitatges)
{
   //if cp= 
}....

