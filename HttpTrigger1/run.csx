#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];
    string mood = req.Query["mood"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    name = name ?? data?.name;
    mood = mood ?? data?.mood;

    string responseMessage;
        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mood)){
            responseMessage = "This HTTP triggered function executed successfully. Pass a name and what you feel right now in the query string or in the request body for a personalized response.";
        } else {
            string movie = "";
            if(mood == "happy") movie = "Daddy's Home 2 (2017)"; 
            else if(mood == "neutral") movie = "The Transporter (2002)";
            else if(mood == "sad") movie = "Skyfall (2012)";
            else movie = "Taken (2008)";
            responseMessage = $"Hello, {name}. This HTTP triggered function executed successfully.\nYou are feeling {mood}.\nWe recommend you watch {movie}.";
        }
    /*
    string responseMessage = string.IsNullOrEmpty(name) || string.IsNullOrEmpty(mood)
        ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : {
                    $"Hello, {name}. This HTTP triggered function executed successfully.\n";
                    $"You are feeling {mood}.\n";
                    $"We recommend you watch ";
                    (mood == "happy") ? $"Daddy's Home 2 (2017)."
                        : (mood == "neutral") ? $"The Transporter (2002)."
                            : (mood == "sad") ? $"Skyfall (2012)";
                }
    */

            return new OkObjectResult(responseMessage);
}