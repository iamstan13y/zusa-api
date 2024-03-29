﻿using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Services;

namespace ZUSA.API.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ISchoolRepository School { get; private set; }
        public ISportRepository Sport { get; private set; }
        public ISubscriptionRepository Subscription { get; private set; }
        public ITeamMemberRepository TeamMember { get; private set; }

        public UnitOfWork(AppDbContext context, IExcelService excelService)
        {
            School = new SchoolRepository(context);
            Sport = new SportRepository(context);
            Subscription = new SubscriptionRepository(context);
            TeamMember = new TeamMemberRepository(context, excelService);
            _context = context;
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}