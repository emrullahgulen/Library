using Autofac;
using Business.Absract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookService>().As<IBookService>();
            builder.RegisterType<EFBookDAL>().As<IBookDAL>();

            builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<EFAuthorDAL>().As<IAuthorDAL>();

            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<EFMemberDAL>().As<IMemberDAL>();

            builder.RegisterType<BookTransactionService>().As<IBookTransactionService>();
            builder.RegisterType<EFBookTransactionDAL>().As<IBookTransactionDAL>();
        }
    }
}
