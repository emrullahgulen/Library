using Business.Absract;
using Core;
using Core.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Entities;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MemberService:IMemberService
    {
        public IMemberDAL _memberDal; 
        private readonly ILogger<BookService> _logger;
        public MemberService(IMemberDAL memberDal, ILogger<BookService> logger)
        {
            _memberDal = memberDal;
            _logger = logger;
        }

        public IResult AddMember(Member member)
        {
            try
            {
                _memberDal.Insert(member);
                return new SuccessDataResult<Member>(Messages.MemberInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Member>(ex.Message);
            }
        }

        public IResult RemoveMember(Member member)
        {
            try
            {
                _memberDal.Delete(member);
                return new SuccessDataResult<Member>(Messages.MemberDelete);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Member>(ex.Message);
            }
        }

        public IResult UpdateMember(Member member)
        {
            try
            {
                member.UpdateDate = DateTime.Now;
                _memberDal.Update(member);
                return new SuccessDataResult<Member>(Messages.MemberUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Member>(ex.Message);
            }
        }

        public IDataResult<Member> GetMemberById(int Id)
        {
            try
            {
                var result = _memberDal.Get(x => x.Id == Id);
                return new SuccessDataResult<Member>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Member>(ex.Message);
            }
        }

        public IDataResult<List<Member>> GetMemberList()
        {
            try
            {
                var List = _memberDal.GetList();
                return new SuccessDataResult<List<Member>>((List<Member>)List);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<List<Member>>(ex.Message);
            }
        }

        public IResult RemoveMemberById(int Id)
        {
            try
            {
                using (var _context = new LibraryDbContext())
                {
                    Member member = _context.Members.FirstOrDefault(x => x.Id == Id);
                    RemoveMember(member);
                    return new SuccessDataResult<Member>(Messages.MemberDelete);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ErrorDataResult<Book>(ex.Message);
            }
        }
    }
}
