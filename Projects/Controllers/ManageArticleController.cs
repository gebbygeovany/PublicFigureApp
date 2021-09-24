﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Projects.Data;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.Controllers
{
    public class ManageArticleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ManageArticleController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? id)
        {
            ViewData["PeopleId"] = id;
            var data = _db.Articles.Where(x => x.PeopleId == id);
            return View(data);
        }

        //GET-Create
        public IActionResult Create(int? id)
        {
            ViewData["PeopleId"] = id;
            return View();
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Article obj)
        {
            if (ModelState.IsValid)
            {
                Article article = new Article();
                Random rnd = new Random();
                article.Id = rnd.Next(1, 999);
                article.Title = obj.Title;
                article.Link = obj.Link;
                article.PeopleId = obj.PeopleId;
                _db.Articles.Add(article);
                _db.SaveChanges();
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "ManageArticle", action = "Index", Id = obj.PeopleId }));
            }
            return View(obj);
        }

        //GET-Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Articles.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Articles.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Articles.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "ManageArticle", action = "Index", Id = obj.PeopleId }));

        }
        //GET-Update
        public IActionResult Update(int? id, int? people)
        {
            ViewData["PeopleId"] = people;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Articles.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Article obj)
        {
            if (ModelState.IsValid)
            {
                _db.Articles.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", new RouteValueDictionary(new { controller = "ManageArticle", action = "Index", Id = obj.PeopleId }));
            }
            return View(obj);
        }
    }
}
