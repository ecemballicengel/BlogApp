using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlogApp.Controllers
{
    
    public class PostsController : Controller
    {
        private readonly IPostRepository _postrepository;
        private readonly ITagRepository _tagRepository;
       
        public PostsController(IPostRepository postrepository, ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
            _postrepository = postrepository;  
        }
       
        public IActionResult Index(string tag)
        {
            //var model = _context.Posts.ToList();
           var posts = _postrepository.Posts;
           if(!string.IsNullOrEmpty(tag)){
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
                return View(posts.ToList());
            }
            else{
                // new PostViewModel
                // {
                //     Posts = _postrepository.Posts.ToList(),
                // Tags = _tagRepository.Tags.ToList()
                // };
               return View(posts.ToList());
            }
            
        }

        public async Task<IActionResult> Details(string? url){
            return View(await _postrepository.Posts.FirstOrDefaultAsync(p=>p.Url == url));
        }
    }
}