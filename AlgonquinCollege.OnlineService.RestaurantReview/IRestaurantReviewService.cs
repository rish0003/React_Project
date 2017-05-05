using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AlgonquinCollege.RestaurantReview
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IResturantReviewService
    {
        [OperationContract]
        Restaurants GetRestaurantsFromXML();

        [OperationContract]
        List<string> GetRestaurantNames();

        [OperationContract]
        RestaurantInfo GetRestaurantByName(string name);

        [OperationContract]
        List<RestaurantInfo> GetRestaurantsByRating(int rating);

        [OperationContract]
        bool SaveRestaurant(RestaurantInfo restInfo);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CompositeType
    //{
    //    bool boolValue = true;
    //    string stringValue = "Hello ";

    //    [DataMember]
    //    public bool BoolValue
    //    {
    //        get { return boolValue; }
    //        set { boolValue = value; }
    //    }

    //    [DataMember]
    //    public string StringValue
    //    {
    //        get { return stringValue; }
    //        set { stringValue = value; }
    //    }
    //}

    public class RestaurantInfo
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public int Rating { get; set; }
        [DataMember]
        public Address Location { get; set; }
    }

    public class Address
    {
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public string PostalCode { get; set; }
    }
}
