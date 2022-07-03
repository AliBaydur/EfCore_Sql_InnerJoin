// See https://aka.ms/new-console-template for more information

using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using UdemyEFCore.CodeFirst;
using UdemyEFCore.CodeFirst.DAL;
using UdemyEFCore.CodeFirst.DTOs;
using UdemyEFCore.CodeFirst.Mappers;
using UdemyEFCore.CodeFirst.Models;


using (var _context = new AppDbContext())
{
    var result = _context.Categories.Join(_context.Products, x => x.Id, y => y.CategoryId, (c, p) => p).ToList();

    var result2 = (from c in _context.Categories
                   join p in _context.Products on c.Id equals p.CategoryId
                   select p).ToList();

    var result3 = (from c in _context.Categories
                   join p in _context.Products on c.Id equals p.CategoryId
                   select new
                   {
                       CategoryName = c.Name,
                       ProductName = p.Name,
                       ProductPrice = p.Price,
                   }).ToList();


    var result4 = _context.Categories.Join(_context.Products, c => c.Id, p => p.CategoryId, (c, p) => new { c, p })
                                     .Join(_context.productFeatures, x => x.p, y => y.Id, (c, pf) => new
                                     {
                                         CategoryName = c.c.Name,
                                         ProductName = c.p.Name,
                                         ProductFeature = pf.Color
                                     });


    var result5 = (from c in _context.Categories
                   join p in _context.Products on c.Id equals p.CategoryId
                   join pf in _context.productFeatures on p.Id equals pf.Id
                   select new { c, p, pf }).ToList();


    var rightjoinResult = await (from pf in _context.productFeatures
                                 join p in _context.Products on pf.Id equals p.Id into pList
                                 from p in pList.DefaultIfEmpty()
                                 select new
                                 {
                                     ProductName = p.Name,
                                     ProductPrice = (decimal?)p.Price,
                                     ProductColor = pf.Color,
                                     ProductWidth = pf.Width,
                                 }).ToListAsync();
    Console.WriteLine("");
}














