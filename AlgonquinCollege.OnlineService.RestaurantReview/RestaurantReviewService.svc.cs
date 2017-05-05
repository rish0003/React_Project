using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace AlgonquinCollege.RestaurantReview
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class RestaurantReviewService : IResturantReviewService
    {
        public RestaurantInfo GetRestaurantByName(string name)
        {
            var allRestaurants = GetRestaurantsFromXML();
            RestaurantInfo restInfo = new RestaurantInfo();
            Address addressInfo = new Address();
            if (allRestaurants != null)
            {
                foreach (RestaurantsRestaurant rest in allRestaurants.Restaurant)
                {
                    if (name == rest.name)
                    {
                        restInfo.Name = rest.name;
                        restInfo.Summary = rest.summary;
                        addressInfo.Street = rest.address.street.name;
                        addressInfo.City = rest.address.city;
                        addressInfo.Province = rest.address.province.ToString();
                        addressInfo.PostalCode = rest.address.postalCode;
                        restInfo.Location = addressInfo;
                        restInfo.Rating = rest.review.rating;
                    }
                }
            }
            return restInfo;
        }

        public List<string> GetRestaurantNames()
        {
            List<string> names = new List<string>();

            var allRestaurants = GetRestaurantsFromXML();
            if (allRestaurants != null)
            {
                foreach (RestaurantsRestaurant rest in allRestaurants.Restaurant)
                {
                    names.Add(rest.name);
                }
            }
            return names;
        }

        public Restaurants GetRestaurantsFromXML()
        {
            Restaurants allRestaurants = null;

            string xmlFile = HostingEnvironment.MapPath(@"~/restaurant.xml");

            using (FileStream xs = new FileStream(xmlFile, FileMode.Open))
            {
                XmlSerializer serializor = new XmlSerializer(typeof(Restaurants));
                allRestaurants = ((Restaurants)serializor.Deserialize(xs));
            }
            return allRestaurants;

        }

        public List<RestaurantInfo> GetRestaurantsByRating(int rating)
        {
            List<RestaurantInfo> names = new List<RestaurantInfo>();

            Restaurants allRestaurants = GetRestaurantsFromXML();

            if (allRestaurants != null)
            {
                foreach (RestaurantsRestaurant rest in allRestaurants.Restaurant)
                {
                    if (rest.review.rating >= rating)
                    //  if (rating == rest.review.rating)
                    {
                        RestaurantInfo restInfo = new RestaurantInfo();
                        Address addressInfo = new Address();
                        restInfo.Name = rest.name;
                        restInfo.Summary = rest.summary;
                        addressInfo.Street = rest.address.street.name;
                        addressInfo.City = rest.address.city;
                        addressInfo.Province = rest.address.province.ToString();
                        addressInfo.PostalCode = rest.address.postalCode;
                        restInfo.Location = addressInfo;
                        restInfo.Rating = rest.review.rating;
                        names.Add(restInfo);
                    }
                }
            }
            return names;
        }

        public bool SaveRestaurant(RestaurantInfo restInfo)
        {

            Restaurants allRestaurants = GetRestaurantsFromXML();
            if (allRestaurants != null)
            {
                foreach (RestaurantsRestaurant rest in allRestaurants.Restaurant)
                {
                    if (restInfo.Name == rest.name)
                    {
                        if (restInfo.Summary != null)
                        {
                            rest.summary = restInfo.Summary;
                        }
                        else
                        {
                            rest.summary = rest.summary;
                        }
                        //int rating = restInfo.Rating;
                        if (String.IsNullOrEmpty(restInfo.Rating.ToString()) || restInfo.Rating == 0)
                        {
                            rest.review.rating = rest.review.rating;
                        }
                        else
                        {
                            rest.review.rating = restInfo.Rating;
                        }
                        rest.address.street.name = restInfo.Location.Street;
                        rest.address.city = restInfo.Location.City;
                        //rest.address.province = restInfo.Location.Province;
                        ProvinceType provinceType = (ProvinceType)Enum.Parse(typeof(ProvinceType), restInfo.Location.Province);
                        rest.address.province = provinceType;
                        rest.address.postalCode = restInfo.Location.PostalCode;
                        //rest.summary = restInfo.Summary;
                        //rest.review.rating = restInfo.Rating;
                        string xmlFile = HostingEnvironment.MapPath(@"~/restaurant.xml");
                        using (FileStream xs = new FileStream(xmlFile, FileMode.Create))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(Restaurants));
                            serializer.Serialize(xs, allRestaurants);
                        }
                        break;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    }
