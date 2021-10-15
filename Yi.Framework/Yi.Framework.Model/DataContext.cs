﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.IOCOptions;
using Yi.Framework.Model.Models;

namespace Yi.Framework.Model
{
	//Add-Migration yi-1
	//Update-Database yi-1
   public partial class DataContext : DbContext
    {
		private readonly IOptionsMonitor<SqliteOptions> _optionsMonitor;
		private readonly string _connStr;

		public DataContext(IOptionsMonitor<SqliteOptions> optionsMonitor)
		{
			_optionsMonitor = optionsMonitor;
			_connStr = _optionsMonitor.CurrentValue.Url;
		}

		public DataContext(string connstr)
		{
			_connStr = connstr;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlite(_connStr);
			}
		}
        public  DbSet<user> user { get; set; }
		public DbSet<role> role { get; set; }
    }
}
