using Mamba.Core.Repository.Interfaces;
using MambaManyToManyCrud.DAL;
using MambaManyToManyCrud.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.Repository.Implementations
{
    public class MemberRepository:GenericRepository<Member>,IMemberRepository
    {
        public MemberRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
