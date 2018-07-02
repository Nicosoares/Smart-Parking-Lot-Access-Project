using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class RegionsOfInterest
    {
        public int height { get; set; }
        public int width { get; set; }
        public int y { get; set; }
        public int x { get; set; }
    }

    public class VehicleRegion
    {
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int x { get; set; }
    }

    public class BodyType
    {
        public string name { get; set; }
        public double confidence { get; set; }
    }

    public class Year
    {
        public double confidence { get; set; }
        public string name { get; set; }
    }

    public class MakeModel
    {
        public double confidence { get; set; }
        public string name { get; set; }
    }

    public class Make
    {
        public double confidence { get; set; }
        public string name { get; set; }
    }

    public class Color
    {
        public string name { get; set; }
        public double confidence { get; set; }
    }

    public class Orientation
    {
        public string name { get; set; }
        public double confidence { get; set; }
    }

    public class Vehicle
    {
        public List<BodyType> body_type { get; set; }
        public List<Year> year { get; set; }
        public List<MakeModel> make_model { get; set; }
        public List<Make> make { get; set; }
        public List<Color> color { get; set; }
        public List<Orientation> orientation { get; set; }
    }

    public class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class Candidate
    {
        public int matches_template { get; set; }
        public double confidence { get; set; }
        public string plate { get; set; }
    }

    public class Result
    {
        public int plate_index { get; set; }
        public VehicleRegion vehicle_region { get; set; }
        public double processing_time_ms { get; set; }
        public Vehicle vehicle { get; set; }
        public int matches_template { get; set; }
        public string plate { get; set; }
        public int requested_topn { get; set; }
        public List<Coordinate> coordinates { get; set; }
        public int region_confidence { get; set; }
        public string region { get; set; }
        public List<Candidate> candidates { get; set; }
        public double confidence { get; set; }
    }

    public class ProcessingTime
    {
        public double total { get; set; }
        public double plates { get; set; }
        public double vehicles { get; set; }
    }

    public class RootObject
    {
        public string uuid { get; set; }
        public List<RegionsOfInterest> regions_of_interest { get; set; }
        public int credits_monthly_used { get; set; }
        public int credit_cost { get; set; }
        public int img_height { get; set; }
        public bool error { get; set; }
        public long epoch_time { get; set; }
        public int version { get; set; }
        public List<Result> results { get; set; }
        public long credits_monthly_total { get; set; }
        public int img_width { get; set; }
        public string data_type { get; set; }
        public ProcessingTime processing_time { get; set; }
    }
}
