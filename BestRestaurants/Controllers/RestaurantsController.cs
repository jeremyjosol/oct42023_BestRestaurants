using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BestRestaurants.Models;

namespace BestRestaurants.Controllers
{
  public class RestaurantsController : Controller
  {
    private readonly BestRestaurantsContext _db;
    public RestaurantsController(BestRestaurantsContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Restaurant> model = _db.Restaurants
                                  .Include(restaurant => restaurant.Cuisine)
                                  .ToList();
      return View(model);
    }
    public ActionResult Create()
    {
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Type");
      return View();
    }
    [HttpPost]
    public ActionResult Create(Restaurant restaurant)
    {
      if (restaurant.CuisineId == 0)
      {
        return RedirectToAction("Create");
      }
      _db.Restaurants.Add(restaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Restaurant thisRestaurant = _db.Restaurants
                                     .Include(restaurant => restaurant.Cuisine)
                                     .FirstOrDefault(restaurant => restaurant.RestaurantId == id);
      return View(thisRestaurant);
    }
  }
}