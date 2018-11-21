using CsvHelper;
using ScottsJewels.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ScottsJewels.Datasource
{
    public class Cars
    {
        private List<Car> _cars;

        public Cars ()
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ScottsJewels.Datasource.Resources.CarData.csv"))
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    _cars = csvReader.GetRecords<Car>().ToList();

                    _cars = _cars.GroupBy(x => x.Make + x.Model + x.Year).Select(y => y.First()).ToList();

                    for (int carIndex = 0; carIndex < _cars.Count(); carIndex++)
                    {
                        _cars[carIndex].Id = carIndex+1;
                    }
                }
            }            
        }

        public Car[] GetCars()
        {
            return this._cars.ToArray();
        }

        public Car GetCarById( int id )
        {
            return this._cars.Find(x => x.Id.Equals(id));
        }

        public Car[] SearchCars(string search, int pageSize, int pageIndex, out int total)
        {
            Car [] results = { };
            IQueryable<Car> query = this._cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Year.ToUpper().Contains(search.ToUpper().Trim()) ||
                    x.Make.ToUpper().Contains(search.ToUpper().Trim()) ||
                    x.Model.ToUpper().Contains(search.ToUpper().Trim()));
            }

            total = query.Count();

            query = query
                    .OrderBy(x => x.Year)
                    .ThenBy(x => x.Make)
                    .ThenBy(x => x.Model)        
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize);

            return query.ToArray();
        }
    }
}
