﻿//using System.Collections.Generic;
//using System.Linq;
//using GraduationProject.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace GraduationProject.Controllers
//{
//    [Route("api/Beverage")]
//    [ApiController]
//    public class BeverageController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public BeverageController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public ActionResult<IEnumerable<Beverage>> GetBeverages()
//        {
//            return _context.Beverages.ToList();
//        }

//        [HttpGet("{id}")]
//        public ActionResult<Beverage> GetBeverage(int id)
//        {
//            Beverage? beverage = _context.Beverages.Find(id);

//            if (beverage == null)
//            {
//                return NotFound();
//            }

//            return beverage;
//        }

//        [HttpPut("{id}")]
//        public IActionResult PutBeverage(int id, Beverage beverage)
//        {
//            if (id != beverage.BeverageId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(beverage).State = EntityState.Modified;
//            _context.SaveChanges();

//            return NoContent();
//        }

//        [HttpPost]
//        public ActionResult<Beverage> PostBeverage(Beverage beverage)
//        {
//            _context.Beverages.Add(beverage);
//            _context.SaveChanges();

//            return CreatedAtAction(nameof(GetBeverage), new { id = beverage.BeverageId }, beverage);
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeleteBeverage(int id)
//        {
//            Beverage? beverage = _context.Beverages.Find(id);
//            if (beverage == null)
//            {
//                return NotFound();
//            }

//            _context.Beverages.Remove(beverage);
//            _context.SaveChanges();

//            return NoContent();
//        }
//    }
//}

