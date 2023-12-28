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
    public class ProfessionRepository:GenericRepository<Profession>,IProfessionRepository
    {
        public ProfessionRepository(AppDbContext context):base(context)
        {
            
        }
    }
}
