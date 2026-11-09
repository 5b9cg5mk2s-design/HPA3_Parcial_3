using System;
using Newtonsoft.Json;



var persona = new
{
    Nombre = "Johandry",
    Edad = 25,
    Carrera = "Sistemas y computación"
};

string json = 
    JsonConvert.SerializeObject(persona);

Console.WriteLine(json);
