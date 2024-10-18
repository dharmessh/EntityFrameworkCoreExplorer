using Explorer.Data;
using Explorer.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Transactions;


var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Combine(path, "Football.db");
var optionBuilder = new DbContextOptionsBuilder<FootballDbContext>();
optionBuilder.UseSqlite($"Data Source={dbPath}");

using var _context = new FootballDbContext(optionBuilder.Options);

/*************************TEMPORAL TABLES*************************************/
//using var _mssqlContext = new FootballSqlServerContext();

//var teamHistory = _mssqlContext.Teams.TemporalAll().Where(w => w.Id == 1)
//    .Select(t => new
//    {
//        Name = t.Name,
//        ValueFrom = EF.Property<DateTime>(t,"PeriodStart"),
//        ValueTo = EF.Property<DateTime>(t, "PeriodEnd"),
//    }).ToList();

//foreach (var record in teamHistory)
//{
//    Console.WriteLine(record.Name, record.ValueFrom, record.ValueTo);
//}

//// Migrations at RunTime
///
//await _context.Database.MigrateAsync();

/*******************************TRANSACTION AND ROLLBACKS*******************************/

//var transaction = _context.Database.BeginTransaction();

// Add Coach
// Add Team
// Add League

//try
//{
//    transaction.Commit();
//}
//catch (Exception)
//{
//    transaction.Rollback();
//	throw;
//}

/*******************************  CONCURRENCY CHECK  *******************************/

//var team = await _context.Teams.FindAsync(1);
//team.Name = "COncurrency Update CHeck INDIA";
//team.Version = Guid.NewGuid();

//try
//{
//    await _context.SaveChangesAsync();
//}
//catch (DbUpdateConcurrencyException ex)
//{
//    Console.WriteLine(ex.Message);
//    throw;
//}

/*----------------------------------------GLOBAL QUERY FILTERS-----------------------------------*/

//var league = _context.Leagues.Find(1);
//league.IsDeleted = true;

//_context.SaveChanges();

//// Instead of doing this we can add query filter in configuration, which will add it by default
//var getLeague = _context.Leagues.Where(q => !q.IsDeleted).ToList();

//// This will ignore all the query filters
//var leagueIgnore = _context.Leagues.IgnoreQueryFilters().ToList();

/*-----------------------------------------DATA SEEDING-----------------------------------*/
// Adding Match
//var match = new Match
//{
//    AwayTeamId = 1,
//    HomeTeamId = 2,
//    HomeTeamScore = 0,
//    AwayTeamScore = 0,
//    Date = new DateTime(2024,10,10),
//    TicketPrice = 25
//};

//await _context.AddAsync(match);
//await _context.SaveChangesAsync();

// Inserting Parent and Child at the same time
// Adding Coach and Team
// One Way
//var coach = new Coach
//{
//    Name = "Kapil Dev",  
//};

//var team = new Team
//{
//    Name = "Australia",
//    Coach = coach,
//};

//await _context.AddAsync(team);
//await _context.SaveChangesAsync();

// Second Way
//var team2 = new Team
//{
//    Name = "NewzeaLand",
//    Coach = new Coach
//    {
//        Name = "Faf Du Plesssis",
//    },
//};

//await _context.AddAsync(team2);
//await _context.SaveChangesAsync();

//var league = new League
//{
//    Name = "IPL",
//    Teams = new List<Team>
//    {
//        new Team
//        {
//            Name = "West Indies",
//            Coach = new Coach
//            {
//                Name = "Chris Gayle",
//            },
//        },
//        new Team
//        {
//            Name = "Afghanistan",
//            Coach = new Coach
//            {
//                Name = "Lius Pillip",
//            },
//        },
//        new Team
//        {
//            Name = "Argentina",
//            Coach = new Coach
//            {
//                Name = "Messi",
//            },
//        },
//        new Team
//        {
//            Name = "France",
//            Coach = new Coach
//            {
//                Name = "Morgan",
//            },
//        }
//    }
//};

//await _context.AddAsync(league);
//await _context.SaveChangesAsync();

/*-------------------------------------------EAGER LOADING------------------------------*/

//var leagues = await _context.Leagues.Include(a => a.Teams).ThenInclude(a => a.Coach).ToListAsync();

//foreach (var league in leagues)
//{
//    Console.WriteLine(league.Name);
//    foreach (var team in league.Teams)
//    {
//        Console.WriteLine(team.Name);
//        Console.WriteLine(team.Coach.Name);
//    }
//}

/*-------------------------------------------EXPLICIT LOADING------------------------------*/

//var leagueExp = await _context.FindAsync<League>(1);

//if (!leagueExp.Teams.Any())
//{
//    Console.WriteLine("Teams Not Loaded!");
//}

//await _context.Entry(leagueExp).Collection(a => a.Teams).LoadAsync();

//if (leagueExp.Teams.Any())
//{
//    foreach(var team in leagueExp.Teams)
//    {
//        Console.WriteLine(team.Name);
//        //Console.WriteLine(team.Coach.Name);
//    }
//}

/*-------------------------------------------LAZZY LOADING------------------------------*/

//var lazyTeam = await _context.FindAsync<League>(1);
//foreach (var team in lazyTeam.Teams)
//{
//    Console.WriteLine(team.Name);
//}

//foreach(var league in  _context.Leagues)
//{
//    foreach(var team in league.Teams)
//    {
//        Console.WriteLine($"{team.Name} and {team.Coach.Name}");
//    }
//}

/*-------------------------------------------DATA FILTERING------------------------------*/

//var teams = await _context.Teams.Include("Coach").Include(q => q.HomeMatches.Where(q => q.HomeTeamScore > 0)).ToListAsync();

//foreach (var team in teams)
//{
//    Console.WriteLine($"{team.Name} and {team.Coach.Name}");
//    foreach (var match in team.HomeMatches)
//    {
//        Console.WriteLine($"Score : {match.HomeTeamScore}");
//    }
//}

/*-------------------------------------------PROJECTIONS IMTO DTOS------------------------------*/

//var teamProject = await _context.Teams
//                                  .Select(p => new TeamDetails
//                                  {
//                                      TeamId = p.Id,
//                                      TeamName = p.Name,
//                                      CoachName = p.Coach.Name,
//                                      TotalHomeTeamScore = p.HomeMatches.Sum(s => s.HomeTeamScore),
//                                      TotalAwayTeamScore = p.AwayMatches.Sum(s => s.AwayTeamScore)
//                                  })
//                                  .ToListAsync();

//foreach (var team in teamProject)
//{
//    Console.WriteLine($"{team.TeamName}, {team.CoachName}, {team.TotalHomeTeamScore}, {team.TotalAwayTeamScore}, {team.TeamId}");
//}

/*-------------------------------------------VIEW------------------------------*/

//var view = _context.TeamsAndLeaguesView.ToListAsync();

/*-------------------------------------------RAW SQL QUERY------------------------------*/

//Console.WriteLine("Enter Team Name :");
//var inputTeam = Console.ReadLine();

//// FromRawSQL
//var inputTeamParam = new SqliteParameter("teamParam", inputTeam);
//var teams = _context.Teams.FromSqlRaw($"SELECT * FROM Teams WHERE name = @teamParam", inputTeamParam);

//foreach (var team in teams)
//{
//    Console.WriteLine(team.Name);
//    Console.WriteLine(team.Id);
//    Console.WriteLine(team.Coach.Name);  // Run By EFC Automatically
//}

//// SQL - Automatically Paramterized
//teams = _context.Teams.FromSql($"SELECT * FROM Teams WHERE name = {inputTeam}");
//foreach (var team in teams)
//{
//    Console.WriteLine(team.Name);
//    Console.WriteLine(team.Id);
//}

//// FromSQLinterpolated - Automatically Paramterized
//teams = _context.Teams.FromSqlInterpolated($"SELECT * FROM Teams WHERE name = {inputTeam}");
//foreach (var team in teams)
//{
//    Console.WriteLine(team.Name);
//    Console.WriteLine(team.Id);
//}

//// Mixing RawSQL with LINQ
//var teamList = _context.Teams.FromSql($"SELECT * FROM Teams").Where(w => w.Id == 1).OrderBy(w => w.Id).Include("League").ToList();
//foreach (var team in teamList)
//{
//    Console.WriteLine(team.Name);
//    Console.WriteLine(team.League.Name);
//}

//// Executing Stored Proc
//int leagueId = 1;
//var league = _context.Leagues.FromSqlInterpolated($"EXEC dbo.getLeagueNames {leagueId}");

//// Executing Non Query Statements
//var newTeamName = "INDIA";
//var success = _context.Database.ExecuteSqlInterpolated($"UPDATE Team SET Name = {newTeamName}");

//int teamToDelete = 10;
//var teamDltSucces = _context.Database.ExecuteSqlInterpolated($"EXEC dbo.DeleteTEam {teamToDelete}");

//// Scalar or Non Entity Types
//var scalar = _context.Database.SqlQuery<int>($"SELECT Id FROM Teams").ToList();

//// User Defined Function
//var fromFunction = _context.GetEarliestTeamMatch(1);

/* ---------------------- DATA QUERYING------------------------*/
//// Select *
//var team1 = _context.Teams.ToList();
//var team2 = await _context.Teams.ToListAsync();

//// First
//var team3 = await _context.Teams.FirstAsync();
//var team4 = await _context.Teams.FirstAsync(a => a.Id == 1);

//// FirstOrDefault
//var team5 = await _context.Teams.FirstOrDefaultAsync();
//var team6 = await _context.Teams.FirstOrDefaultAsync(a => a.Id == 1);

//// Find
//var Id = await _context.Teams.FindAsync(1);

//// Single
//var team7 = await _context.Teams.SingleAsync();
//var team8 = await _context.Teams.SingleOrDefaultAsync(a => a.Id == 1);

//// Where
//var teamFilter = await _context.Teams.Where(a => a.Name == "INDIA").ToListAsync();

//// One Way
//var teamFilterConatins = await _context.Teams.Where(a => a.Name.Contains("IND")).ToListAsync();

//// Second Way
//var searchTerm = "IND";
//var teamFilterEfFunction = await _context.Teams.Where(a => EF.Functions.Like(a.Name,$"%{searchTerm}%")).ToListAsync();

//// Query Syntax
//var teamQuery = await (from team in _context.Teams where EF.Functions.Like(team.Name, $"%{searchTerm}%") select team).ToListAsync();

//// Aggregate Functions
//var teamCount = await _context.Teams.CountAsync();
//var teamCountFilter = await _context.Teams.CountAsync(q => q.Id == 1);

//// Max
//var teamMax = await _context.Teams.MaxAsync(q => q.Id);

//// Min
//var teamMin = await _context.Teams.MinAsync(q => q.Id);

//// Average
//var teamAvg = await _context.Teams.AverageAsync(q => q.Id);

//// Sum
//var teamSum = await _context.Teams.SumAsync(q => q.Id);

//// Group By
//var teamGroup = await _context.Teams.GroupBy(a => a.Name).ToListAsync();    

//// Group By for more than one column
//var teamGroupColumns = await _context.Teams.GroupBy(a => new {a.Name, a.CreationDate, a.Id}).ToListAsync();

//// Group By With WHERE clause
//var groupByWhere = await _context.Teams.Where(a => a.Id == 1).GroupBy(a => a.CreationDate).ToListAsync();

//// Group By with HAVING clause
//var groupByHaving = await _context.Teams.GroupBy(a => a.CreationDate).Where(a => a.Count() > 1).ToListAsync();

//// Order By Asc
//var teamOrder = await _context.Teams.OrderBy(a => a.Name).ToListAsync();

//// Order By Desc
//var teamOrderDesc = await _context.Teams.OrderByDescending(a => a.Name).ToListAsync();

//// Max BY
//var teamMaxBy = _context.Teams.MaxBy(a => a.Id);

//// Min By
//var teamMinBy = _context.Teams.MinBy(a => a.Id);

//// Skip and Take
//var records = 2;
//var page = 1;

//var teamRecords = await _context.Teams.Skip(records * page).Take(records).ToListAsync();

//// Select and Projections
//var projections = await _context.Teams.Select(a => a.Name).ToListAsync();

//// Projections : On the fly with the help of anonymous objects
//var projection = await _context.Teams.Select(a => new { a.Id, a.Name }).ToListAsync();

//// Projection in DTO Class
//var projectionDto = await _context.Teams.Select(a => new TeamInfo{ TeamNo = a.Id, Name = a.Name }).ToListAsync();


//// Tracking vs Non TRacking
//// Good for SELECT queries
//var data = await _context.Teams.AsNoTracking().ToListAsync();

//// IQueryable 
//var extendQuery =  _context.Teams.AsQueryable();
//extendQuery = extendQuery.Where(a => a.Id == 1);                      // Building the query statement
//extendQuery = extendQuery.Where(a => a.Name.Contains("Ind"));             // Building the query statement

//var finalTeam = await extendQuery.ToListAsync();  // final execution

//foreach (var group in groupByWhere)
//{
//    // Key Property : CreationDate here
//    Console.WriteLine(group.Key);
//}


//foreach (var item in team2)
//{
//    Console.WriteLine(item.Name);
//}

///*---------------------DATA INSERTION------------------------*/

//var coaches = new Coach
//{ 
//    Name  = "Ravi Sastri",
//    CreationDate = DateTime.Now,
//};

//await _context.Coaches.AddAsync(coaches);

//Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

//await _context.SaveChangesAsync();

//Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

//Console.WriteLine(coaches.Id);

//// Batch Insert
//await _context.Coaches.AddRangeAsync(coaches);
//await _context.SaveChangesAsync();  


///*---------------------------UPDATE-----------------------------*/

//// With Tracking
//var updateCoach = await _context.Coaches.FindAsync(1);
//updateCoach.Name = "Gautam Gambhir";
//await _context.SaveChangesAsync();

//// With No Tracking
//var anotherCoach = await _context.Coaches.AsNoTracking().FirstOrDefaultAsync(a => a.Id == 2);
//anotherCoach.Name = "Testing EF Core Change Tracking";

//Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

//// One Way
////_context.Update(anotherCoach);

//// Second Way
//_context.Entry(anotherCoach).State = EntityState.Modified;

//Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
//await _context.SaveChangesAsync();

//Console.WriteLine(_context.ChangeTracker.DebugView.LongView);

///*--------------------------DELETE------------------------------*/

//// With Tracking
//var deleteCoach = await _context.Coaches.FindAsync(1);
//_context.Remove(deleteCoach);
//await _context.SaveChangesAsync();

//// With No Tracking
//var dltCoach = await _context.Coaches.FindAsync(1);
//_context.Entry(dltCoach).State = EntityState.Deleted;
//await _context.SaveChangesAsync();

///*-------------------------------SPECIAL METHODS in EFC 7.0------------------------*/

//// ExecuteDelete
//await _context.Coaches.Where(a => a.Id == 1).ExecuteDeleteAsync();

//// ExecuteUpdate
//await _context.Coaches.Where(a => a.Name == "Ravi Sastri").ExecuteUpdateAsync(set => set
//    .SetProperty(prop => prop.Name,"Virat Kohli")
//    .SetProperty(prop => prop.CreationDate, DateTime.Now)
//     );

///*----------------------DTOS----------------------------------*/
//public class TeamInfo
//{
//    public string Name { get; set; }
//    public int TeamNo { get; set; }
//}


public class TeamDetails
{
    public int TeamId { get;set; }
    public string TeamName { get; set; } 
    public string CoachName { get; set; } 
    public int TotalHomeTeamScore { get; set; }
    public int TotalAwayTeamScore { get; set; }
}
