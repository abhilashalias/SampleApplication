using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApplication.Extensions;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SampleApplication.Data.Entities;
using SampleApplication.Data;

namespace SampleApplication.Controllers
{
    public class ChartsController : Controller
    {
        private readonly ILogger<ChartsController> _logger;
        private readonly ApplicationDbContext _context;

        public ChartsController(ILogger<ChartsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult DashBoard()
        {
            var statistics = GetStatistics();
            ViewData["PieChart"] = GeneratePieChart(statistics);
            ViewData["BarChart"] = GenerateBarChart(statistics);
            ViewData["LineChart"] = GenerateLineChart(statistics);


            return View();
        }


        private Chart GenerateLineChart(List<double?> statistics)
        {
            Chart chart = new Chart();

            chart.Type = Enums.ChartType.Line;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>() { "Not Applicable", "Not Started", "In Progress", "Completed" };

            var dataset = new LineDataset()
            {
                Label = "Audit Issue Status",
                Data = statistics,
                Fill = "false",
                Tension = 0.1,
                BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(75, 192, 192, 0.4)},
                BorderColor = new List<ChartColor> { ChartColor.FromRgb(75,192,192)},
                BorderCapStyle = "butt",
                BorderDash = new List<int> { },
                BorderDashOffset = 0.0,
                BorderJoinStyle = "miter",
                PointBorderColor = new List<ChartColor> { ChartColor.FromRgb(75,192,192) },
                PointBackgroundColor = new List<ChartColor> { ChartColor.FromHexString("#ffffff") },
                PointBorderWidth = new List<int> { 1 },
                PointHoverRadius = new List<int> { 5 },
                PointHoverBackgroundColor = new List<ChartColor> { ChartColor.FromRgb(75,192,192) },
                PointHoverBorderColor = new List<ChartColor> { ChartColor.FromRgb(220,220,220) },
                PointHoverBorderWidth = new List<int> { 2 },
                PointRadius = new List<int> { 1 },
                PointHitRadius = new List<int> { 10 },
                SpanGaps = false
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;
            return chart;
        }

        private Chart GenerateBarChart(List<double?> statistics)
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>() { "Not Applicable", "Not Started", "In Progress", "Completed" };

            BarDataset dataset = new BarDataset()
            {
                Label = "Audit Issue Status",
                Data = statistics,
                BackgroundColor = new List<ChartColor>
                {
                    ChartColor.FromRgba(255, 99, 132, 0.2),
                    ChartColor.FromRgba(54, 162, 235, 0.2),
                    ChartColor.FromRgba(255, 206, 86, 0.2),
                    ChartColor.FromRgba(75, 192, 192, 0.2),
                    ChartColor.FromRgba(153, 102, 255, 0.2),
                    ChartColor.FromRgba(255, 159, 64, 0.2)
                },
                BorderColor = new List<ChartColor>
                {
                    ChartColor.FromRgb(255, 99, 132),
                    ChartColor.FromRgb(54, 162, 235),
                    ChartColor.FromRgb(255, 206, 86),
                    ChartColor.FromRgb(75, 192, 192),
                    ChartColor.FromRgb(153, 102, 255),
                    ChartColor.FromRgb(255, 159, 64)
                },
                BorderWidth = new List<int>() { 1 }
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            return chart;
        }
    
        private Chart GeneratePieChart(List<double?> statistics)
        {
            Chart chart = new Chart();
            chart.Type = Enums.ChartType.Pie;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>() { "Not Applicable", "Not Started", "In Progress", "Completed" };

            PieDataset dataset = new PieDataset()
            {
                 Label = "Audit Issue Status",
                BackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#FF6384"),
                    ChartColor.FromHexString("#36A2EB"),
                    ChartColor.FromHexString("#FFCE56")
                },
                HoverBackgroundColor = new List<ChartColor>() {
                    ChartColor.FromHexString("#FF6384"),
                    ChartColor.FromHexString("#36A2EB"),
                    ChartColor.FromHexString("#FFCE56")
                },
                Data = statistics
            };

           data.Datasets = new List<Dataset>();
           data.Datasets.Add(dataset);
           chart.Data = data;

           return chart;
        }

        private List<double?> GetStatistics()
        {
            List<double?> statistics = new List<double?>();
            var audits = _context.Audits.ToList();
            ViewData["Audits"] = audits;
            statistics.Add(audits.Where(x=>x.Status.ToUpper() == "NOT APPLICABLE").Count());
            statistics.Add(audits.Where(x=>x.Status.ToUpper() == "NOT STARTED").Count());
            statistics.Add(audits.Where(x=>x.Status.ToUpper() == "IN PROGRESS").Count());
            statistics.Add(audits.Where(x=>x.Status.ToUpper() == "COMPLETED").Count());
            return statistics;
        }
    }
}