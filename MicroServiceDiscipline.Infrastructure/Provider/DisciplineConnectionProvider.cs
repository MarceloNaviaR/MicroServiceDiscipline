using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MicroServiceDiscipline.Infrastructure.Provider
{
    public class DisciplineConnectionProvider : IDisciplineConnectionProvider
    {
        private readonly IConfiguration _configuration;

        public DisciplineConnectionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}