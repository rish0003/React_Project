using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReactJs.RestaurantServiceOnline;
namespace ReactJs.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reviews()
        {

            ResturantReviewServiceClient reviewer = new ResturantReviewServiceClient();
           RestaurantInfo[] restInfos = reviewer.GetRestaurantsByRating(0);

            Models.RestaurantInfo[] restaurantInfos = new
                                    Models.RestaurantInfo[restInfos.Length];

            for (int i = 0; i < restInfos.Length; i++)
            {
                restaurantInfos[i] = new Models.RestaurantInfo();
                restaurantInfos[i].Name = restInfos[i].Name;
                restaurantInfos[i].Summary = restInfos[i].Summary;
                restaurantInfos[i].Rating = restInfos[i].Rating;

                restaurantInfos[i].Location = new Models.Address();
                restaurantInfos[i].Location.Street = restInfos[i].Location.Street;
                restaurantInfos[i].Location.City = restInfos[i].Location.City;
                restaurantInfos[i].Location.Province = restInfos[i].Location.Province;
                restaurantInfos[i].Location.PostalCode = restInfos[i].Location.PostalCode;
            }
            return Json(restaurantInfos, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateReview(string restInfo)
        {
            if (!string.IsNullOrWhiteSpace(restInfo))
            {
                Models.RestaurantInfo restaurantInfo =
                System.Web.Helpers.Json.Decode<Models.RestaurantInfo>(restInfo);
               
               RestaurantInfo info = new RestaurantInfo();
                info.Name = restaurantInfo.Name;
                info.Summary = restaurantInfo.Summary;
                info.Rating = restaurantInfo.Rating;

                info.Location = new Address();
                info.Location.Street = restaurantInfo.Location.Street;
                info.Location.City = restaurantInfo.Location.City;
                info.Location.Province = restaurantInfo.Location.Province;
                info.Location.PostalCode = restaurantInfo.Location.PostalCode;

                ResturantReviewServiceClient reviewer = new ResturantReviewServiceClient();
                if (reviewer.SaveRestaurant(info))
                {
                    return Json("Updated restaurant review has been saved!");
                }
            }
            return Json("No data received!");
         }


    }
}