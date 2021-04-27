using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using kursa4.Models;
using Microsoft.EntityFrameworkCore;

namespace kursa4.Controllers
{
    public class HomeController : Controller
    {
        StudContext db;

        public HomeController(StudContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Students.ToList());
        }
        [HttpGet]
        public IActionResult Append()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if(student.Surname == null)
            {
                ViewBag.Oshibka = "Введите фамилию";
                return View("Append");
            }
            else if (student.Name == null)
            {
                ViewBag.Oshibka = "Введите имя";
                return View("Append");
            }
            else if (student.Patronymic == null)
            {
                ViewBag.Oshibka = "Введите отчество";
                return View("Append");
            }
            else if (student.Course == null)
            {
                ViewBag.Oshibka = "Введите курс";
                return View("Append");
            }
            else if (student.State == null)
            {
                ViewBag.Oshibka = "Введите положение";
                return View("Append");
            }
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Editing(int? id)
        {
            if (id == null) RedirectToAction("Index");
            var student = await db.Students.FirstOrDefaultAsync(p => p.id == id);
            ViewBag.Student = student;
            ViewBag.StudentId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Editing(Student student)
        {
            try
            {
                db.Students.Update(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Error = "Ошибка";
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Student student = await db.Students.FirstOrDefaultAsync(p => p.id == id);
                if (student != null)
                {
                    db.Students.Remove(student);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }

}