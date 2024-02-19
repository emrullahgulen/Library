using Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebMvc.Controllers
{
    public class MembersController : MyBaseController
    {
        private readonly ILogger<MembersController> _logger;
        public MembersController(ILogger<MembersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMembers()
        {
            try
            {
                var result = NewhttpClinetGet("Member/GetMemberList");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    List<Member> ListModels = JsonConvert.DeserializeObject<List<Member>>(data);

                    return View(ListModels);
                }
                else
                {
                    ViewBag.LoginError = result.StatusCode;
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

        [HttpGet]
        public async Task<IActionResult> AddMember()
        {
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> UpdateMember(int Id)
        {
            try
            {
                var resultBook= NewhttpClinetGet($"Member/GetMemberById?Id={Id}");
                if (resultBook.IsSuccessStatusCode)
                {
                    var data = await resultBook.Content.ReadAsStringAsync().ConfigureAwait(true);
                    Member model = JsonConvert.DeserializeObject<Member>(data);
                    return View(model);
                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateMember(Member model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }

                var result = NewhttpClinetPost("Member/UpdateMember", model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetMembers", "Members");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddMember(Member model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }

                var result = NewhttpClinetPost("Member/AddMember",model);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetMembers", "Members");
                }
                else
                {
                    ViewBag.ErrorMessage = "Hata Oluştu ";
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

        [HttpGet]
        public async Task<IActionResult> DeleteMember(int Id)
        {
            try
            { 
                var result = NewhttpClinetGet($"Member/DeleteMemberbyId?Id={Id}");
                if (result.IsSuccessStatusCode)
                {
                    var data = await result.Content.ReadAsStringAsync().ConfigureAwait(true);
                    // Burda mesaj gösterilebilir
                    return RedirectToAction("GetMembers");
                }

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }
    }
}
