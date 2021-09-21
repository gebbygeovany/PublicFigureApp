﻿using Microsoft.AspNetCore.Mvc;
using Projects.Data;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Controllers
{

    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ArticleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Article> objList = _db.Articles;
            return View(objList);
        }

        //GET-Create
        public IActionResult Create()
        {
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article obj)
        {
            if (ModelState.IsValid)
            {
                _db.Articles.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
