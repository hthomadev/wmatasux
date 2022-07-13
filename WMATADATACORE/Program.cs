
using System.Net.Mime;
using Newtonsoft.Json;
using Terminal.Gui;
using WMATADATACORE;

static class Program{

    
    static void Main()
    {
        Application.Init();
        
        var label = new Label ("WMATA EInk Train Display") {
            X = Pos.Center (),
            Y = Pos.Percent(20),
            Height = 3,
        };
        
        var label2 = new Label ("by hthoma") {
            X = Pos.Center (),
            Y = Pos.Percent(40),
            Height = 1,
        };
        
        Application.Top.Add (label);
        Application.Top.Add (label2);
        Application.Run ();
        Application.Shutdown();
        MakeRequest();
        

        Console.WriteLine("Hit ENTER to exit...");
        Console.ReadLine();
    }

    static async void MakeRequest()
    {
        var client = new HttpClient();

        while (true)
        {
            // Request headers
            client.DefaultRequestHeaders.Add("api_key", "eca071b8985a4560aaa66f86d5307efc");


            var uri =
                $"https://api.wmata.com/StationPrediction.svc/json/GetPrediction/{RailPrediction.DUPONT_STATION_CODE},{RailPrediction.FOGGY_BOTTOM_STATION_CODE},{RailPrediction.METRO_CENTER_STATION_CODE},{RailPrediction.FARRAGUT_WEST_STATION_CODE}?";

            var response = await client.GetAsync(uri);

            RailPrediction rp = JsonConvert.DeserializeObject<RailPrediction>(response.Content.ReadAsStringAsync().Result);

            rp.getWalkableTrainsOnly().ForEach(t =>
            {
                Console.WriteLine("Train at {0} to {1} is {2} minutes away", t.LocationName, t.DestinationName, t.Min);
            });


            Thread.Sleep(30000);
        }

    }


}

