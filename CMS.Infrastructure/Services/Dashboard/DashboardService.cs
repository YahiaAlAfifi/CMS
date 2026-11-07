using CMS.Core.Enums;
using CMS.Core.ViewModels;
using CMS.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Infrastructure.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly CMSDbContext _db;
        private readonly Random _random = new Random();

        public DashboardService(CMSDbContext db)
        {
            _db = db;
        }

        public async Task<DashboardViewModel> GetData()
        {
            return new DashboardViewModel
            {
                NumberOfPosts = await _db.Posts.CountAsync(x => !x.IsDelete),
                NumberOfUsers = await _db.Users.CountAsync(x => !x.IsDelete),
                NumberOfTracks = await _db.Tracks.CountAsync(x => !x.IsDelete),
                NumberOfAdvertisements = await _db.Advertisements.CountAsync(x => !x.IsDelete)
            };
        }

        public async Task<List<PieChartViewModel>> GetUserTypeChart()
        {
            var users = await _db.Users
                .Where(x => !x.IsDelete)
                .GroupBy(x => x.UserType)
                .Select(x => new { Type = x.Key, Count = x.Count() })
                .ToListAsync();

            return users.Select(x => new PieChartViewModel
            {
                Key = x.Type.ToString(),
                Value = x.Count,
                color = GenerateColor()
            }).ToList();
        }

        public async Task<List<PieChartViewModel>> GetContentTypeChart()
        {
            return new List<PieChartViewModel>
            {
                new PieChartViewModel
                {
                    Key = ContentType.Post.ToString(),
                    Value = await _db.Posts.CountAsync(x => !x.IsDelete),
                    color = GenerateColor()
                },
                new PieChartViewModel
                {
                    Key = ContentType.Track.ToString(),
                    Value = await _db.Tracks.CountAsync(x => !x.IsDelete),
                    color = GenerateColor()
                }
            };
        }

        public async Task<List<PieChartViewModel>> GetContentByMonthChart()
        {
            var monthNames = new[]
            {
                "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
            };

            var data = new List<PieChartViewModel>();
            for (var month = 1; month <= 12; month++)
            {
                data.Add(new PieChartViewModel
                {
                    Key = monthNames[month - 1],
                    Value = await _db.Posts.CountAsync(x => !x.IsDelete && x.CreatedAt.Month == month),
                    color = GenerateColor()
                });
            }

            return data;
        }

        private string GenerateColor()
        {
            return string.Format("#{0:X6}", _random.Next(0x1000000));
        }
    }
}
