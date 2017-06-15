using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

//Libraries needed for HTTP GET
using System.Net;
using System.Net.Http;
using System.IO;


namespace DMRCodePlugger
{

    static public class Repeaters
    {

        public const string LHDataSfx = "cgi-bin/trbo-database/datadump.cgi?table=repeaters&format=json&header=1%20";


        static public Repeater[] Get_Repeaters()
        {

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                //put the url of the api here, so the base part of the url address
                client.BaseAddress = new Uri("http://www.dmr-marc.net/");

                //Add suffix to end of address.. add current unix timestamp here..
                HttpResponseMessage response = client.GetAsync(LHDataSfx).Result;

                response.EnsureSuccessStatusCode();

                //Get result out as string, return it from function
                string result = response.Content.ReadAsStringAsync().Result;

                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(result);

                //Repeater[] parsedObj = JsonConvert.DeserializeObject<Rootobject>(result);
                Repeater[] parsedObj = root.repeaters;

                return parsedObj;
            }
        }

        public class Rootobject
        {
            public Repeater[] repeaters { get; set; }
        }

        public class Repeater
        {
            public string country { get; set; }
            public string locator { get; set; }
            public string callsign { get; set; }
            public string map_info { get; set; }
            public string state { get; set; }
            public string color_code { get; set; }
            public string frequency { get; set; }
            public string map { get; set; }
            public string trustee { get; set; }
            public string ts_linked { get; set; }
            public string city { get; set; }
            public string assigned { get; set; }
            public string ipsc_network { get; set; }
            public string offset { get; set; }
        }
    }
}


public class Rootobject
{
    public Repeater[] repeaters { get; set; }
}

public class Repeater
{
    public string country { get; set; }
    public string locator { get; set; }
    public string callsign { get; set; }
    public string map_info { get; set; }
    public string state { get; set; }
    public string color_code { get; set; }
    public string frequency { get; set; }
    public string map { get; set; }
    public string trustee { get; set; }
    public string ts_linked { get; set; }
    public string city { get; set; }
    public string assigned { get; set; }
    public string ipsc_network { get; set; }
    public string offset { get; set; }
}
