using System.Web.Mvc;
using System.Linq;
using ScottsJewels.Datasource;
using ScottsJewels.Models;

namespace WebAPI.Controllers
{
    public class CarController : Controller
    {
        static private Cars CarDatasource = new Cars();

        [HttpGet]
        public JsonResult SearchCars(string search, int pageSize=20, int pageIndex=0)
        {
            int total = 0;
            Car[] cars = CarDatasource.SearchCars( search, pageSize, pageIndex, out total );
            
            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {                    
                    data =  from car 
                            in cars 
                            select new { 
                                value = car.Id, 
                                text = string.Format("{0} {1} {2}", car.Year, car.Make, car.Model)
                            },
                    total = total
                }
            };
        }    
    }


    // Restful doesn't work well for pagination.
    //public class CarController : ApiController
    //{
    //    static private Cars CarDatasource = new Datasource.Cars();

    //    [HttpGet]
    //    public IEnumerable<Car> Get()
    //    {
    //        return CarDatasource.GetCars();
    //    }

    //    [HttpGet]
    //    public Car Get(int id)
    //    {
    //        return CarDatasource.GetCarById(id);
    //    }

    //    //[HttpPost]
    //    //public void Post(Person value)
    //    //{
    //    //}

    //    //// PUT api/values/5
    //    //public void Put(int id, [FromBody]Person value)
    //    //{
    //    //}

    //    //// DELETE api/values/5
    //    //public void Delete(int id)
    //    //{
    //    //}
    //}
}