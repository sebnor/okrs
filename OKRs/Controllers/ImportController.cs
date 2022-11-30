using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OKRs.Models;
using OKRs.Repositories;

namespace OKRs.Controllers
{
    public class ImportController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IObjectivesRepository _objectivesRepository;

        public ImportController(IUserRepository userRepository, IObjectivesRepository objectivesRepository)
        {
            _userRepository = userRepository;
            _objectivesRepository = objectivesRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();

            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    stream.Position = 0;

                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   

                    var usersLookup = _userRepository.GetAllUsers().ToDictionary(x => x.UserName);
                    //var objectiveLookup = (await _objectivesRepository.GetAllObjectives()).ToDictionary(x => x.Title);
                    var objectiveLookup = await GetObjectiveLookup();

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        var email = row.GetCell(0).ToString();
                        var objective = row.GetCell(1).ToString();
                        var keyResult = row.GetCell(2).ToString();

                        var user = await CreateOrGetUser(usersLookup, email);
                        var obj = await CreateOrGetObjective(objectiveLookup, user, objective);
                        await CreateOrGetKeyResult(obj, user, keyResult);
                    }

                }
            }
            return RedirectToAction("Index", "Objective");
        }

        private async Task<Dictionary<string, Objective>> GetObjectiveLookup()
        {
            var dic = new Dictionary<string, Objective>();
            var objectives = await _objectivesRepository.GetAllObjectives();
            foreach (var objective in objectives)
            {
                dic.TryAdd(objective.Title, objective);
            }
            return dic;
        }

        private async Task CreateOrGetKeyResult(Objective objective, ApplicationUser user, string k)
        {
            var description = k.Trim();
            if (objective.KeyResults.Any(x => x.Description == description))
                return;

            objective.AddKeyResult(new KeyResult { Description = description });
            await _objectivesRepository.SaveObjective(objective);
        }

        private async Task<Objective> CreateOrGetObjective(Dictionary<string, Objective> objectiveLookup, ApplicationUser user, string o)
        {
            var title = o.Trim();
            if (objectiveLookup.ContainsKey(title))
                return objectiveLookup[title];

            var newObjective = new Objective(title, user.UserId);
            await _objectivesRepository.CreateObjective(newObjective);
            objectiveLookup.Add(title, newObjective);

            return newObjective;
        }

        private async Task<ApplicationUser> CreateOrGetUser(IDictionary<string, ApplicationUser> usersLookup, string e)
        {
            var email = e.Trim();
            if (usersLookup.ContainsKey(email))
                return usersLookup[email];

            var name = email.Split('@').FirstOrDefault()?.Replace(".", " ");
            var newUser = new ApplicationUser { UserName = email, Email = email, Name = name };
            var result = await _userRepository.CreateUser(newUser);
            if (!result.Succeeded)
            {
                throw new Exception($"Could not create user {e}");
            }
            usersLookup.Add(email, newUser);
            return newUser;
        }
    }
}