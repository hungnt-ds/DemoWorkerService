namespace DemoWorkerService;

public class Worker : BackgroundService
{
    const int ThreadDelay = 5000;
    ILogger<Worker> log;
    string FileName = @"D:\DeletedKoiFish.txt";

    public Worker(ILogger<Worker> logger)
    {
        log = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        List<KoiFish> koiFishes = new List<KoiFish>
        {
            new KoiFish
            {
                FishId = 1,
                FishName = "KoiFish1",
                UserId = 101,
                PondId = 201,
                BodyShape = "Oval",
                Age = 3,
                Size = 15.5m,
                Weight = 2.3m,
                Gender = "Male",
                Breed = "Kohaku",
                Origin = "Japan",
                Price = 200.50m,
                IsDeleted = false,
                CreatedAt = DateTime.Now.AddMonths(-6),
                UpdatedAt = DateTime.Now
            },
            new KoiFish
            {
                FishId = 2,
                FishName = "KoiFish2",
                UserId = 102,
                PondId = 202,
                BodyShape = "Round",
                Age = 4,
                Size = 20.3m,
                Weight = 3.0m,
                Gender = "Female",
                Breed = "Sanke",
                Origin = "Japan",
                Price = 300.75m,
                IsDeleted = true,
                CreatedAt = DateTime.Now.AddYears(-1),
                UpdatedAt = DateTime.Now.AddMonths(-3)
            },
            new KoiFish
            {
                FishId = 3,
                FishName = "KoiFish3",
                UserId = 103,
                PondId = 203,
                BodyShape = "Long",
                Age = 2,
                Size = 12.1m,
                Weight = 1.9m,
                Gender = "Male",
                Breed = "Showa",
                Origin = "China",
                Price = 150.00m,
                IsDeleted = false,
                CreatedAt = DateTime.Now.AddMonths(-8),
                UpdatedAt = DateTime.Now.AddMonths(-1)
            },
            new KoiFish
            {
                FishId = 4,
                FishName = "KoiFish4",
                UserId = 104,
                PondId = 204,
                BodyShape = "Slim",
                Age = 5,
                Size = 22.8m,
                Weight = 3.5m,
                Gender = "Female",
                Breed = "Utsuri",
                Origin = "Vietnam",
                Price = 400.99m,
                IsDeleted = true,
                CreatedAt = DateTime.Now.AddYears(-2),
                UpdatedAt = DateTime.Now.AddMonths(-6)
            },
            new KoiFish
            {
                FishId = 5,
                FishName = "KoiFish5",
                UserId = 105,
                PondId = 205,
                BodyShape = "Oval",
                Age = 1,
                Size = 10.5m,
                Weight = 1.5m,
                Gender = "Male",
                Breed = "Asagi",
                Origin = "Thailand",
                Price = 100.00m,
                IsDeleted = false,
                CreatedAt = DateTime.Now.AddMonths(-4),
                UpdatedAt = DateTime.Now.AddDays(-10)
            }
        };

        //sc create "My Service" BinPath = "D:\Code\ASP.NET\PRN221\DemoWorkerService1\DemoWorkerService03\bin\Release\net8.0\DemoWorkerService03.exe"
        //dotnet publish  -c  Release

        while (!stoppingToken.IsCancellationRequested)
        {
            log.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            try
            {
                foreach (var koiFish in koiFishes)
                {
                    if (koiFish.IsDeleted)
                    {
                        string content = $"FishId: {koiFish.FishId}, FishName: {koiFish.FishName}, IsDeleted: {koiFish.IsDeleted}" + Environment.NewLine;
                        await File.AppendAllTextAsync(FileName, content);
                    }
                }
                await File.AppendAllTextAsync(FileName, new string('*', 30) + Environment.NewLine);
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Error occurred while writing to file.");
            }

            await Task.Delay(ThreadDelay, stoppingToken);
        }
    }
}

public class KoiFish
{
    public int FishId { get; set; }
    public string FishName { get; set; } = null!;
    public int UserId { get; set; }
    public int? PondId { get; set; }
    public string BodyShape { get; set; } = null!;
    public int? Age { get; set; }
    public decimal? Size { get; set; }
    public decimal? Weight { get; set; }
    public string Gender { get; set; } = null!;
    public string Breed { get; set; } = null!;
    public string Origin { get; set; } = null!;
    public decimal? Price { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
