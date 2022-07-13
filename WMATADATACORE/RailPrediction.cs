namespace WMATADATACORE;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RailPrediction
    {
        
        public static string DUPONT_STATION_CODE = "A03";
        public static string FARRAGUT_WEST_STATION_CODE = "C03";
        public static string FOGGY_BOTTOM_STATION_CODE = "C04";
        public static string METRO_CENTER_STATION_CODE = "A01";
        public static List<String> WALKABLE_STATIONS = new List<string>{ DUPONT_STATION_CODE, FARRAGUT_WEST_STATION_CODE, FOGGY_BOTTOM_STATION_CODE };
        public static List<String> GREENSBORO_OR_BEYOND = new List<string> { "N03", "N04", "NO5", "N06" };
        public static List<String> METROCENTER_AND_BEYOND = new List<string> { "B11","B10","B09", "B08", "B07", "B06", "B05", "B04", "B03", "B02", "B01", "A01" };
        
        
        public List<Train> Trains { get; set; }

        public List<Train> getTrainsToWorkOnly()
        {
            return Trains.FindAll(t =>
                GREENSBORO_OR_BEYOND.Contains(t.DestinationCode) || METROCENTER_AND_BEYOND.Contains(t.DestinationCode));
        }

        public List<Train> getWalkableTrainsOnly()
        {
          return this.Trains.Where(t=> 
                (t.LocationCode == DUPONT_STATION_CODE)
                || (t.LocationCode == FARRAGUT_WEST_STATION_CODE) 
                || (t.LocationCode == FOGGY_BOTTOM_STATION_CODE)).ToList();
        }
        public List<Train> getWalkableTrainsInTimeOnly()
        {
            return this.Trains.Where(t=> 
                (t.LocationCode == DUPONT_STATION_CODE && int.Parse(t.Min) > 4)
                || (t.LocationCode == FARRAGUT_WEST_STATION_CODE && int.Parse(t.Min) > 14) 
                || (t.LocationCode == FOGGY_BOTTOM_STATION_CODE && int.Parse(t.Min) > 14)).ToList();
        }
        
        
        
    }

    public class Train
    {
        public string Car { get; set; }
        public string Destination { get; set; }
        public string DestinationCode { get; set; }
        public string DestinationName { get; set; }
        public string Group { get; set; }
        public string Line { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string Min { get; set; }
    }
