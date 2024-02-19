using Business.Absract;
using Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class MemberController : ControllerBase
    {

        private readonly IMemberService _memberservice;

        public MemberController(IMemberService memberservice)
        {
            _memberservice = memberservice;
        }

        [HttpGet]
        public IActionResult GetMemberList()
        {
            var result = _memberservice.GetMemberList();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpPost]
        public IActionResult AddMember(Member member)
        {
            var result = _memberservice.AddMember(member);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public IActionResult UpdateMember(Member member)
        {
            var result = _memberservice.UpdateMember(member);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet]
        public IActionResult DeleteMemberbyId(int Id)
        {
            var result = _memberservice.RemoveMemberById(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpGet]
        public IActionResult GetMemberById(int Id)
        {
            var result = _memberservice.GetMemberById(Id);
            return Ok(result.Data);
        }
    }


}
