using Core.Results;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Absract
{
    public interface IMemberService
    {
        IResult AddMember(Member member);
        IResult RemoveMember(Member member);
        IResult UpdateMember(Member member);
        IDataResult<Member> GetMemberById(int Id);
        IDataResult<List<Member>> GetMemberList();
        IResult RemoveMemberById(int Id);
    }
}
