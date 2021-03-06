﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InlämningsUppgift_Asp.Net_MVC.Models;
using InlämningsUppgift_Asp.Net_MVC.Data;
using InlämningsUppgift_Asp.Net_MVC.Data.Repository;

namespace InlämningsUppgift_Asp.Net_MVC.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.GetPost(id);

            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());
            else
            {
                var post = _repo.GetPost((int)id);
                return View(post);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.PostId > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("index");
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
