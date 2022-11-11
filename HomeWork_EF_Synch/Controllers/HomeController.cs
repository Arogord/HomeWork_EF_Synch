using HomeWorCore_MVC_DataBase.Models;
using HomeWork_EF_Synch.Context;
using HomeWork_EF_Synch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Text;

namespace HomeWork_EF_Synch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string GetSensorReadings()
        {
            StringBuilder result = new StringBuilder();
            using (ApplicationContext db = new ApplicationContext())
            {
                var sensor = db.sensors.ToList().;
                foreach(var val in sensor)
                {
                     result.Append($"{val.ID} \t{val.Temp_Sensor} \t\t{val.Humid_Sensor} \t\t{val.Motion_Sensor} \t\t{val.Light_Sensor} \t\t{val.CO_Sensor} \t\t{val.Data_Time}\n");
                }            }
            return result.ToString();
        }

        [HttpGet]
        public IActionResult AddSensorData()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddSensorData(Sensors data)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.sensors.Add(data);
                db.SaveChanges();
            }

            return View();
            
        }

        public string UpdateFirstSensorData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Sensors? sens = db.sensors.FirstOrDefault();
                if(sens != null)
                {
                    sens.Temp_Sensor = 33;
                    sens.Humid_Sensor = 33;
                    sens.Motion_Sensor = Convert.ToBoolean(1);
                    sens.CO_Sensor = 33;
                    sens.Light_Sensor = 33;
                    sens.Data_Time = new DateTime(2022, 11, 11, 0, 0, 0);
                    db.SaveChanges();
                }
            }

            return "Ready";
        }
        public string DeleteFirstSensorData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Sensors? sens = db.sensors.FirstOrDefault();
                if (sens != null)
                {
                    db.sensors.Remove(sens);
                    db.SaveChanges();
                }
            }

            return "Deleted";
        }

    }
}